using BlankApp1.ApiCognitos;
using BlankApp1.Util;
using MvvmHelpers.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
		/// <summary>
		/// Command to sign in
		/// </summary>
		public ICommand CmdSignIn { get; set; }

		public string Email { get; set; }
		public string Password { get; set; }

		public SignInViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
			base(navigationService, sessionStore, apiCognito, dialogService)
		{
			CmdSignIn = new Command(DoSignIn);
			this.Title = "Connexion";
			this.Email = "hyacinthe.briand@diiage.org";
			this.Password = "Azerty@123";
		}

		protected virtual async void DoSignIn()
		{
			try
			{
				var user = Email?.Trim().ToLower();
				var pass = Password?.Trim();
				IsBusy = true;
				var result = await AuthApi.SignIn(user, pass);
				IsBusy = false;

				if (result.Result == CognitoResult.Ok)
				{
					SessionStore.UserName = user;
					SessionStore.AccessToken = result.AccessToken;
					SessionStore.IdToken = result.IdToken;
					SessionStore.RefreshToken = result.RefreshToken;
					SessionStore.SessionId = result.SessionId;
					SessionStore.TokenIssuedServer = result.TokenIssued;
					SessionStore.TokenExpiresServer = result.Expires;
					SessionStore.IsAdmin = result.IsAdmin;

					await this.OnCognitoEvent(CognitoEvent.Authenticated);
				}
				else if (result.Result == CognitoResult.NotAuthorized)
				{
					await this.OnCognitoEvent(CognitoEvent.BadPassword);
				}
				else if (result.Result == CognitoResult.UserNotFound)
				{
					await this.OnCognitoEvent(CognitoEvent.UserNotFound);
				}
				else if (result.Result == CognitoResult.PasswordChangeRequred)
				{
					SessionStore.UserName = user;
					SessionStore.AccessToken = result.AccessToken;
					SessionStore.IdToken = result.IdToken;
					SessionStore.RefreshToken = result.RefreshToken;
					SessionStore.SessionId = result.SessionId;
					SessionStore.TokenIssuedServer = result.TokenIssued;
					SessionStore.TokenExpiresServer = result.Expires;
					SessionStore.IsAdmin = result.IsAdmin;

					await this.OnCognitoEvent(CognitoEvent.PasswordChangedRequired);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
			}
		}
	}
}
