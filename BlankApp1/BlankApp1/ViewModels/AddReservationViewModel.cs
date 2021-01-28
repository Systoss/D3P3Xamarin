using BlankApp1.ApiCognitos;
using BlankApp1.Entities;
using BlankApp1.Models;
using BlankApp1.Util;
using MvvmHelpers.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class AddReservationViewModel : ViewModelBase
    {
        public ICommand CmdNavigateToAddParticipant { get; set; }
        public ICommand CmdAddReservation { get; set; }

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

        private string _name;

        public string Name
        {
            get { return this._name; }
            set { SetProperty(ref _name, value); }
        }

        private DateTime _dateDebut;

        public DateTime DateDebut
        {
            get { return this._dateDebut; }
            set { SetProperty(ref _dateDebut, value); }
        }

        private DateTime _dateFin;

        public DateTime DateFin
        {
            get { return this._dateFin; }
            set { SetProperty(ref _dateFin, value); }
        }

        private TimeSpan _timeDebut;

        public TimeSpan TimeDebut
        {
            get { return this._timeDebut; }
            set { SetProperty(ref _timeDebut, value); }
        }

        private TimeSpan _timeFin;

        public TimeSpan TimeFin
        {
            get { return this._timeFin; }
            set { SetProperty(ref _timeFin, value); }
        }

        private List<User> _listUsers;

        public List<User> ListUsers
        {
            get { return this._listUsers; }
            set { SetProperty(ref _listUsers, value); }
        }

        public AddReservationViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Ajouter une réservation";

            this.CmdNavigateToAddParticipant = new Command(NavitageToAddParticipant);
            this.CmdAddReservation = new Command(AddReservation);

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

            this.ListUsers = new List<User>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(AppNavigationParameters.SpaceAddReservation))
            {
                this.SelectedSpace = this.ListSpaces.FirstOrDefault(l => l.Id == ((Space)parameters[AppNavigationParameters.SpaceAddReservation]).Id);
            }

            if (parameters.ContainsKey(AppNavigationParameters.DateDebutAddReservation))
            {
                this.DateDebut = (DateTime)parameters[AppNavigationParameters.DateDebutAddReservation];
            }

            if (parameters.ContainsKey(AppNavigationParameters.DateFinAddReservation))
            {
                this.DateDebut = (DateTime)parameters[AppNavigationParameters.DateDebutAddReservation];
            }
            else
            {
                this.DateFin = this.DateDebut;
            }

            if (parameters.ContainsKey(AppNavigationParameters.TimeDebutAddReservation))
            {
                this.TimeDebut = (TimeSpan)parameters[AppNavigationParameters.TimeDebutAddReservation];
            }

            if (parameters.ContainsKey(AppNavigationParameters.TimeFinAddReservation))
            {
                this.TimeFin = (TimeSpan)parameters[AppNavigationParameters.TimeFinAddReservation];
            }

            if (parameters.ContainsKey(AppNavigationParameters.AddParticipant))
            {
                User user = (User)parameters[AppNavigationParameters.AddParticipant];
                if (!this.ListUsers.Any(c => c.Id == user.Id))
                {
                    var listUser = new List<User>();
                    listUser.AddRange(this.ListUsers);
                    listUser.Add(user);
                    this.ListUsers = listUser;
                }
            }
        }

        protected virtual async void NavitageToAddParticipant()
        {
            var parameters = new NavigationParameters();
            parameters.Add(AppNavigationParameters.ParticipantAddParticipant, this.ListUsers);
            await this.NavigationService.NavigateAsync("AddParticipant", parameters);
        }

        protected virtual async void AddReservation()
        {
            this.IsBusy = true;

            if (DateDebut != null && DateFin != null && TimeDebut != null && TimeFin != null && ListUsers.Count > 0 && SelectedSpace != null && !string.IsNullOrEmpty(Name))
            {
                this.IsBusy = false;
                await this.DialogService.DisplayAlertAsync(DefaultStrings.ValidationCreationTitle, DefaultStrings.ValidationCreationOK, DefaultStrings.OkButton);
                this.NavigateToMainPage();
            }
            else
            {
                this.IsBusy = false;
                await this.DialogService.DisplayAlertAsync(DefaultStrings.RenseignerInformationTitle, DefaultStrings.RenseignerInformation, DefaultStrings.OkButton);
            }
        }
    }
}
