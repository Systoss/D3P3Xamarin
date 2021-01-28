using BlankApp1.ApiCognitos;
using BlankApp1.Entities;
using BlankApp1.Models;
using BlankApp1.Util;
using BlankApp1.Views;
using MvvmHelpers.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class AvailabilityViewModel : TabViewModelBase
    {
        public ICommand CmdNavigateToListAvailability { get; set; }

        private List<Space> _listSpaces;

        public List<Space> ListSpaces
        {
            get { return this._listSpaces; }
            set { SetProperty(ref _listSpaces, value); }
        }

        private Space _selectedSpace;

        public Space SelectedSpace
        {
            get { return this._selectedSpace; }
            set { SetProperty(ref _selectedSpace, value); }
        }

        private DateTime _dateAvailability;

        public DateTime DateAvailability
        {
            get { return this._dateAvailability; }
            set { SetProperty(ref _dateAvailability, value); }
        }

        public AvailabilityViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService)
            : base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Disponibilite";

            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;

            this.CmdNavigateToListAvailability = new Command(NavigateToListAvailability);
            this.DateAvailability = DateTime.Now;

            this.ListSpaces = new List<Space>
            {
                new Space{ Id = 1, Capacity = 20, Name = "Salle 304" },
                new Space{ Id = 2, Capacity = 20, Name = "Salle 305" },
                new Space{ Id = 3, Capacity = 20, Name = "Salle 306" },
                new Space{ Id = 4, Capacity = 20, Name = "Salle 307" },
                new Space{ Id = 5, Capacity = 20, Name = "Salle 308" },
                new Space{ Id = 6, Capacity = 20, Name = "Salle 309" },
                new Space{ Id = 7, Capacity = 20, Name = "Salle 400" },
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            // Implement your implementation logic here...
        }

        private void HandleIsActiveTrue(object sender, EventArgs args)
        {
            if (IsActive == false) return;

            // Handle Logic Here

            this.SessionStore.LastTab = nameof(Availability);
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

        protected virtual async void NavigateToListAvailability()
        {
            this.IsBusy = true;

            if (this.SelectedSpace != null && this.DateAvailability != null)
            {
                var parameters = new NavigationParameters();

                parameters.Add(AppNavigationParameters.SpaceAvailibility, this.SelectedSpace);
                parameters.Add(AppNavigationParameters.DateAvailibility, this.DateAvailability);

                await NavigationService.NavigateAsync("ListAvailability", parameters);
            }
            else
            {
                this.IsBusy = false;
                await this.DialogService.DisplayAlertAsync(DefaultStrings.RenseignerInformationTitle, DefaultStrings.RenseignerInformation, DefaultStrings.OkButton);
            }
            
        }
    }
}
