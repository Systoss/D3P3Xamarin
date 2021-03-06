﻿using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BlankApp1.ApiCognitos
{
    public class ApiCognito : IApiCognito
	{
		public string ClientId { get; set; }
		public string PoolId { get; set; }
		public RegionEndpoint RegionEndpoint { get; set; }
        public AmazonCognitoIdentityProviderConfig ClientHttpConfig { get; set; }

        public async Task<SignInContext> SignIn(string userName, string password)
		{
            try
            {
                var credentials = new AnonymousAWSCredentials();

                using (var client = new AmazonCognitoIdentityProviderClient(credentials, ClientHttpConfig))
                {
                    CognitoUserPool userPool = new CognitoUserPool(PoolId, ClientId, client);
                    CognitoUser user = new CognitoUser(userName, ClientId, userPool, client);

                    AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
                    {
                        Password = password
                    }).ConfigureAwait(false);
					
					

                    // TODO handle other challenges
                    if (context.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
                    {
						return new SignInContext(CognitoResult.PasswordChangeRequred)
						{
							//User = user,
							SessionId = context.SessionID
						};
					}
                    else
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(context.AuthenticationResult?.AccessToken);
						var groups = token.Payload.ToArray().FirstOrDefault(p => p.Key == "cognito:groups").Value;

                        return new SignInContext(CognitoResult.Ok)
                        {
                            //User = user,
                            IdToken = context.AuthenticationResult?.IdToken,
                            RefreshToken = context.AuthenticationResult?.RefreshToken,
                            AccessToken = context.AuthenticationResult?.AccessToken,
                            TokenIssued = user.SessionTokens.IssuedTime,
                            Expires = user.SessionTokens.ExpirationTime,
                            SessionId = context.SessionID,
							IsAdmin = groups != null ? groups.ToString().Contains("Admin") : false
                        };
                    }
                }
            }
            catch (NotAuthorizedException)
            {
                return new SignInContext(CognitoResult.NotAuthorized);
            }
            catch (UserNotFoundException)
            {
                return new SignInContext(CognitoResult.UserNotFound);
            }
            catch (UserNotConfirmedException)
            {
                return new SignInContext(CognitoResult.NotConfirmed);
            }
            catch (Exception e)
            {
                Console.WriteLine($"SignIn() threw an exception {e}");
            }
            return new SignInContext(CognitoResult.Unknown);
        }


        public async Task<SignInContext> RefreshToken(string userName, string idToken, string accessToken, String refreshToken, DateTime issued, DateTime expires)
		{
			try
			{
				var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint);

				CognitoUserPool userPool = new CognitoUserPool(PoolId, ClientId, provider);
				CognitoUser user = new CognitoUser("", ClientId, userPool, provider);

				user.SessionTokens = new CognitoUserSession(idToken, accessToken, refreshToken, issued, expires);

				AuthFlowResponse context = await user.StartWithRefreshTokenAuthAsync(new InitiateRefreshTokenAuthRequest
				{
					 AuthFlowType = AuthFlowType.REFRESH_TOKEN_AUTH
				})
				.ConfigureAwait(false);

				// TODO handle other challenges
				return new SignInContext(CognitoResult.Ok)
				{
					//User = user,
					IdToken = context.AuthenticationResult?.IdToken,
					RefreshToken = context.AuthenticationResult?.RefreshToken,
					AccessToken = context.AuthenticationResult?.AccessToken,
					TokenIssued = user.SessionTokens.IssuedTime,
					Expires = user.SessionTokens.ExpirationTime,
					SessionId = context.SessionID
				};
			}
			catch (NotAuthorizedException)
			{
				return new SignInContext(CognitoResult.NotAuthorized);
			}
			catch (Exception e)
			{
				Console.WriteLine($"RefreshToken() threw an exception {e}");
			}
			return new SignInContext(CognitoResult.Unknown);
		}

		public async Task<CognitoContext> SignUp( string userName, string password)
		{
			try
			{
				var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint);

				var result = await provider.SignUpAsync(new SignUpRequest
				{
					ClientId = ClientId,
					Password = password,
					Username = userName
				});

				Console.WriteLine("Signed in.");

				return new CognitoContext(CognitoResult.SignupOk);
			}
			catch( UsernameExistsException)
			{
				return new CognitoContext(CognitoResult.UserNameAlreadyUsed);
			}
			catch (InvalidParameterException)
			{
				return new CognitoContext(CognitoResult.PasswordRequirementsFailed);
			}
			catch (Exception e)
			{
				Console.WriteLine($"SignUp() threw an exception {e}");
			}
			return new CognitoContext(CognitoResult.Unknown);
		}

		public async Task<CognitoContext> ForgotPassword(string userName, string code)
		{
			try
			{
				var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint);
				CognitoUserPool userPool = new CognitoUserPool(PoolId, ClientId, provider);
				CognitoUser user = new CognitoUser(userName, ClientId, userPool, provider);

				await user.ForgotPasswordAsync();

				return new CognitoContext(CognitoResult.Ok);
			}
			catch (Exception e)
			{
                Console.WriteLine($"ForgotPassword() threw an exception {e}");
            }
			return new CognitoContext(CognitoResult.Unknown);
		}

		public async Task<CognitoContext> VerifyWithCode(string userName, string code)
		{
			try
			{
				var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint);

				var result = await provider.ConfirmSignUpAsync(new ConfirmSignUpRequest
				{
					ClientId = ClientId,
					Username = userName,
					ConfirmationCode = code
				});

				return new CognitoContext(CognitoResult.Ok);
			}
			catch (Exception e)
			{
				Console.WriteLine($"VerifyWithCode() threw an exception {e}");
			}
			return new CognitoContext(CognitoResult.Unknown);
		}

		public async Task<CognitoContext> UpdatePassword(string userName, string newPassword, string sessionId)
		{
			try
			{
				var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint);

				CognitoUserPool userPool = new CognitoUserPool(PoolId, ClientId, provider);
				CognitoUser user = new CognitoUser(userName, ClientId, userPool, provider);

				var res = await user.RespondToNewPasswordRequiredAsync(new RespondToNewPasswordRequiredRequest
				{ 
					SessionID = sessionId,
					NewPassword = newPassword
				});

				return new CognitoContext(CognitoResult.Ok);
			}
			catch (Exception e)
			{
				Console.WriteLine($"UpdatePassword() threw an exception {e}");
			}
			return new CognitoContext(CognitoResult.Unknown);
		}

	}
}
