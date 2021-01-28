using Amazon;
using Amazon.CognitoIdentityProvider;
using System;
using System.Threading.Tasks;

namespace BlankApp1.ApiCognitos
{
	public enum CognitoResult
	{
		Unknown,
		Ok,
		PasswordChangeRequred,
		SignupOk,
		NotAuthorized,
		Error,
		UserNotFound,
		UserNameAlreadyUsed,
		PasswordRequirementsFailed,
		NotConfirmed
	}

	public class CognitoContext
	{
		public CognitoContext(CognitoResult res = CognitoResult.Unknown)
		{
			Result = res;
		}
		public CognitoResult Result { get; set; }
	}

	public class SignInContext : CognitoContext
	{
		public SignInContext(CognitoResult res = CognitoResult.Unknown) : base(res)
		{
		}
		//public CognitoUser User { get; set; }
		public String IdToken { get; set; }
		public String AccessToken { get; set; }
		public String RefreshToken { get; set; }
		public String SessionId { get; set; }
		public DateTime TokenIssued { get; set; }
		public DateTime Expires { get; set; }
        public bool IsAdmin { get; set; }
    }

	public class CredentialsContext : CognitoContext
	{
		public CredentialsContext(CognitoResult res = CognitoResult.Unknown) : base(res)
		{
		}
		//public CognitoUser User { get; set; }
		public string AccessKeyId { get; set; }
		public DateTime Expiration { get; set; }
		public string SecretKey { get; set; }
		public string SessionToken { get; set; }
	}


	public interface IApiCognito 
	{
		string ClientId { get; set; }
		string PoolId { get; set; }
		RegionEndpoint RegionEndpoint { get; set; }
		AmazonCognitoIdentityProviderConfig ClientHttpConfig { get; set; }

		/// <summary>
		/// Sign in using the username and password
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		Task<SignInContext> SignIn(string userName, string password);

		/// <summary>
		/// Refresh the token
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="idToken"></param>
		/// <param name="accessToken"></param>
		/// <param name="refreshToken"></param>
		/// <param name="issued"></param>
		/// <param name="expires"></param>
		/// <returns></returns>
		Task<SignInContext> RefreshToken(string userName, string idToken, string accessToken, String refreshToken, DateTime issued, DateTime expires);

		/// <summary>
		/// Send a forgot password link to user
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		Task<CognitoContext> ForgotPassword(string userName, string code);

		/// <summary>
		/// Update the password. 
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="newPassword"></param>
		/// <param name="sessionId"></param>
		/// <returns></returns>
		Task<CognitoContext> UpdatePassword(string userName, string newPassword, string sessionId);
	}
}


