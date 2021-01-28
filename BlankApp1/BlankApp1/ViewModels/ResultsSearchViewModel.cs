using BlankApp1.ApiCognitos;
using BlankApp1.Entities;
using BlankApp1.Util;
using MvvmHelpers.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class ResultsSearchViewModel : ViewModelBase
    {
        public ICommand CmdNavigateToAddReservation { get; set; }

        private List<Space> _spaces;

        public List<Space> Spaces
        {
            get { return this._spaces; }
            set { SetProperty(ref _spaces, value); }
        }

        private Space _selectedSpace;

        public Space SelectedSpace
        {
            get { return this._selectedSpace; }
            set { SetProperty(ref _selectedSpace, value); }
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

        public ResultsSearchViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Resultat de la recherche";

            this.CmdNavigateToAddReservation = new Command(NavigateToAddReservation);

            this._spaces = new List<Space>()
            {
                new Space()
                {
                    Id = 1,
                    Name = "Salle 304",
                    Capacity = 25
                },
                new Space()
                {
                    Id = 2,
                    Name = "Salle 8",
                    Capacity = 300
                },
                new Space()
                {
                    Id = 3,
                    Name = "Voiture 4",
                    Capacity = 5
                },
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            // Implement your implementation logic here...

            if (parameters.ContainsKey(AppNavigationParameters.ResultatRechercheDateDebut))
            {
                this.DateDebut = (DateTime)parameters[AppNavigationParameters.ResultatRechercheDateDebut];
            }

            if (parameters.ContainsKey(AppNavigationParameters.ResultatRechercheDateFin))
            {
                this.DateFin = (DateTime)parameters[AppNavigationParameters.ResultatRechercheDateFin];
            }

            if (parameters.ContainsKey(AppNavigationParameters.ResultatRechercheTimeDebut))
            {
                this.TimeDebut = (TimeSpan)parameters[AppNavigationParameters.ResultatRechercheTimeDebut];
            }

            if (parameters.ContainsKey(AppNavigationParameters.ResultatRechercheTimeFin))
            {
                this.TimeFin = (TimeSpan)parameters[AppNavigationParameters.ResultatRechercheTimeFin];
            }
        }

        protected virtual async void NavigateToAddReservation()
        {
            if (this.SelectedSpace != null)
            {
                var parameters = new NavigationParameters();

                parameters.Add(AppNavigationParameters.SpaceAddReservation, this.SelectedSpace);
                parameters.Add(AppNavigationParameters.TimeDebutAddReservation, this.TimeDebut);
                parameters.Add(AppNavigationParameters.TimeFinAddReservation, this.TimeFin);
                parameters.Add(AppNavigationParameters.DateDebutAddReservation, this.DateDebut);
                parameters.Add(AppNavigationParameters.DateFinAddReservation, this.DateFin);

                await NavigationService.NavigateAsync("AddReservation", parameters);
            }
            else
            {
                this.IsBusy = false;
                await this.DialogService.DisplayAlertAsync(DefaultStrings.RenseignerInformationTitle, DefaultStrings.SelectionSpace, DefaultStrings.OkButton);
            }
        }
    }
}
