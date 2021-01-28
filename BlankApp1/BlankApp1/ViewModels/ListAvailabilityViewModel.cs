using BlankApp1.ApiCognitos;
using BlankApp1.Entities;
using BlankApp1.Models;
using BlankApp1.Util;
using MvvmHelpers.Commands;
using Prism;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class ListAvailabilityViewModel : ViewModelBase
    {
        public ICommand CmdNavigateToAddReservation { get; set; }

        private List<Booking> _listReservations;

        public List<Booking> ListReservations
        {
            get { return this._listReservations; }
            set { SetProperty(ref _listReservations, value); }
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

        public ListAvailabilityViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService)
            : base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Les réservations";

            this.CmdNavigateToAddReservation = new Command(NavigateToAddReservation);

            this.ListReservations = new List<Booking>
            {
                new Booking{ Id = 1, Name = "Reunion 1", startDate = DateTime.Now, endDate = DateTime.Now },
                new Booking{ Id = 2, Name = "Reunion 2", startDate = DateTime.Now, endDate = DateTime.Now },
                new Booking{ Id = 3, Name = "Reunion 3", startDate = DateTime.Now, endDate = DateTime.Now},
                new Booking{ Id = 4, Name = "Reunion 4", startDate = DateTime.Now, endDate = DateTime.Now},
                new Booking{ Id = 5, Name = "Reunion 5", startDate = DateTime.Now, endDate = DateTime.Now},
                new Booking{ Id = 6, Name = "Reunion 6", startDate = DateTime.Now, endDate = DateTime.Now },
                new Booking{ Id = 7, Name = "Reunion 7", startDate = DateTime.Now, endDate = DateTime.Now },
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(AppNavigationParameters.SpaceAvailibility))
            {
                SelectedSpace = (Space)parameters[AppNavigationParameters.SpaceAvailibility];
            }

            if (parameters.ContainsKey(AppNavigationParameters.DateAvailibility))
            {
                DateAvailability = (DateTime)parameters[AppNavigationParameters.DateAvailibility];
            }
        }

        private List<Space> ListSpaces = new List<Space>
            {
                new Space{ Id = 1, Capacity = 20, Name = "Salle 304" },
                new Space{ Id = 2, Capacity = 20, Name = "Salle 305" },
                new Space{ Id = 3, Capacity = 20, Name = "Salle 306" },
                new Space{ Id = 4, Capacity = 20, Name = "Salle 307" },
                new Space{ Id = 5, Capacity = 20, Name = "Salle 308" },
                new Space{ Id = 6, Capacity = 20, Name = "Salle 309" },
                new Space{ Id = 7, Capacity = 20, Name = "Salle 400" },
            };

        protected virtual async void NavigateToAddReservation()
        {
            var parameters = new NavigationParameters();

            parameters.Add(AppNavigationParameters.SpaceAddReservation, this.SelectedSpace);
            parameters.Add(AppNavigationParameters.DateDebutAddReservation, this.DateAvailability);

            await NavigationService.NavigateAsync("AddReservation", parameters);

        }
    }
}
