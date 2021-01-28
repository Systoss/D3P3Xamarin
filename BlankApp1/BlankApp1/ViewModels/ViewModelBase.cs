using BlankApp1.ApiCognitos;
using BlankApp1.Util;
using BlankApp1.Views;
using MvvmHelpers.Commands;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        public ISessionStore SessionStore { get; set; }
        public IApiCognito AuthApi { get; set; }
        public IPageDialogService DialogService { get; set; }
        public ICommand CmdSignOut { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy = false;

        /// <summary>
        /// Indicates when the cognito API is busy. Can be bound for activity indicators, etc.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public bool IsAdmin
        {
            get { return SessionStore.IsAdmin; }
        }

        public ViewModelBase(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            SessionStore = sessionStore;
            AuthApi = apiCognito;
            DialogService = dialogService;
            this.CmdSignOut = new Command(SignOut);
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public virtual async void SignOut()
        {
            this.IsBusy = true;
            this.SessionStore.Logout();

            await NavigationService.NavigateAsync("SignIn");
        }

        public async void NavigateToMainPage()
        {
            var parameters = new NavigationParameters();

            if (SessionStore.IsAdmin)
            {
                parameters.Add(KnownNavigationParameters.CreateTab, nameof(Administration));
            }
            else
            {
                parameters.Add(KnownNavigationParameters.CreateTab, nameof(User));
            }

            parameters.Add(KnownNavigationParameters.CreateTab, nameof(Recherche));
            parameters.Add(KnownNavigationParameters.CreateTab, nameof(Availability));

            if (!string.IsNullOrEmpty(SessionStore.LastTab))
            {
                parameters.Add(KnownNavigationParameters.SelectedTab, SessionStore.LastTab);
            }

            await NavigationService.NavigateAsync("MainPage" + parameters);
        }

        public async Task OnCognitoEvent(CognitoEvent ce)
        {
            switch (ce)
            {
                case CognitoEvent.Authenticated:
                    this.NavigateToMainPage();
                    break;
                case CognitoEvent.BadPassword:
                    await this.DialogService.DisplayAlertAsync(DefaultStrings.SignInTitle, DefaultStrings.BadPassMessage, DefaultStrings.OkButton);
                    break;
                case CognitoEvent.UserNotFound:
                    await this.DialogService.DisplayAlertAsync(DefaultStrings.SignInTitle, DefaultStrings.UserNotFoundMessage, DefaultStrings.OkButton);
                    break;
                case CognitoEvent.PasswordChangedRequired:
                    await NavigationService.NavigateAsync("UpdatePassword");
                    break;
                case CognitoEvent.PasswordUpdated:
                    await NavigationService.NavigateAsync("SignIn");
                    break;
                case CognitoEvent.PasswordUpdateFailed:
                    await this.DialogService.DisplayAlertAsync(DefaultStrings.PassUpdateFailedTitle, DefaultStrings.PassUpdateFailedMessage, DefaultStrings.OkButton);
                    break;
                default:
                    break;
            }
        }
    }
}
