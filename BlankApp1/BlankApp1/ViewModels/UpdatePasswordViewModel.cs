using BlankApp1.ApiCognitos;
using BlankApp1.Util;
using MvvmHelpers.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class UpdatePasswordViewModel : ViewModelBase
    {
        public UpdatePasswordViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Mise à jour du mot de passe";
            this.CmdUpdate = new Command(DoVerify);
        }

        public ICommand CmdUpdate { get; set; }

        public string Password { get; set; }
        public string Password2 { get; set; }

        protected virtual async void DoVerify()
        {
            if (Password != Password2)
            {
                await this.OnCognitoEvent(CognitoEvent.PasswordUpdateFailed);
            }
            else
            {
                try
                {
                    var user = SessionStore.UserName.Trim().ToLower();
                    var pass = Password.Trim();

                    IsBusy = true;
                    var result = await AuthApi.UpdatePassword(user, pass, SessionStore.SessionId);
                    IsBusy = false;

                    if (result.Result == CognitoResult.Ok)
                    {
                        await this.OnCognitoEvent(CognitoEvent.PasswordUpdated);
                    }
                    else
                    {
                        await this.OnCognitoEvent(CognitoEvent.PasswordUpdateFailed);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
                }
            }
        }
    }
}
