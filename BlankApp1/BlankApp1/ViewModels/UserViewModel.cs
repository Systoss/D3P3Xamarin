using BlankApp1.ApiCognitos;
using BlankApp1.Util;
using BlankApp1.Views;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlankApp1.ViewModels
{
    public class UserViewModel : TabViewModelBase
    {
        public UserViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService)
            : base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "User";

            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            // Implement your implementation logic here...
        }

        private void HandleIsActiveTrue(object sender, EventArgs args)
        {
            if (IsActive == false) return;

            // Handle Logic Here
            this.SessionStore.LastTab = nameof(User);
        }

        private void HandleIsActiveFalse(object sender, EventArgs args)
        {
            if (IsActive == true) return;

            // Handle Logic Here
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActiveTrue;
            IsActiveChanged -= HandleIsActiveFalse;
        }
    }
}
