using BlankApp1.ApiCognitos;
using BlankApp1.Util;
using BlankApp1.Views;
using MvvmHelpers.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class AdministrationViewModel : TabViewModelBase
    {
        public ICommand CmdNavigateToAddSpace { get; set; }
        public ICommand CmdNavigateToAddSpaceType { get; set; }
        public ICommand CmdNavigateToAddTool { get; set; }

        public AdministrationViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService)
            : base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Administration";

            this.CmdNavigateToAddSpace = new Command(NavigateToAddSpace);
            this.CmdNavigateToAddSpaceType = new Command(NavigateToAddSpaceType);
            this.CmdNavigateToAddTool = new Command(NavigateToAddTool);

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
            this.SessionStore.LastTab = nameof(Administration);
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

        protected virtual async void NavigateToAddSpace()
        {
            await NavigationService.NavigateAsync("AddSpace");
        }

        protected virtual async void NavigateToAddSpaceType()
        {
            await NavigationService.NavigateAsync("AddSpaceType");
        }

        protected virtual async void NavigateToAddTool()
        {
            await NavigationService.NavigateAsync("AddTool");
        }
    }
}
