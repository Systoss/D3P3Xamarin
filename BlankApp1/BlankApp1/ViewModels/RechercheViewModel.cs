using BlankApp1.ApiCognitos;
using BlankApp1.Entities;
using BlankApp1.Models;
using BlankApp1.Util;
using BlankApp1.Views;
using MvvmHelpers.Commands;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace BlankApp1.ViewModels
{
    public class RechercheViewModel : TabViewModelBase
    {
        public ICommand CmdNavigateToAddCriterion { get; set; }
        public ICommand CmdNavigateToResultSearch { get; set; }
        public ICommand CmdNavigateToEditCriterion { get; set; }
        public ICommand CmdDeleteCriterion { get; set; }

        private Criterion _selectedcriterion;

        public Criterion Selectedcriterion
        {
            get { return this._selectedcriterion; }
            set { SetProperty(ref _selectedcriterion, value); }
        }

        private List<Criterion> _criterionList;

        public List<Criterion> CriterionList
        {
            get { return this._criterionList; }
            set { SetProperty(ref _criterionList, value); }
        }

        private List<SpaceType> _spaceTypeList;

        public List<SpaceType> SpaceTypeList
        {
            get { return this._spaceTypeList; }
            set { SetProperty(ref _spaceTypeList, value); }
        }

        private SpaceType _selectedSpaceType;

        public SpaceType SelectedSpaceType
        {
            get { return this._selectedSpaceType; }
            set { SetProperty(ref _selectedSpaceType, value); }
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

        private int _capacite;

        public int Capacite
        {
            get { return this._capacite; }
            set { SetProperty(ref _capacite, value); }
        }

        public RechercheViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService)
            : base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Recherche";
            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;

            this.CriterionList = new List<Criterion>();

            this.SpaceTypeList = new List<SpaceType>
            {
                new SpaceType{ Id= 1, Label = "Salle"},
                new SpaceType{ Id= 2, Label = "Parking"},
                new SpaceType{ Id= 3, Label = "Voiture"}
            };

            this.CmdNavigateToResultSearch = new MvvmHelpers.Commands.Command(NavigateToResultSearch);
            this.CmdNavigateToAddCriterion = new MvvmHelpers.Commands.Command(NavigateToAddCriterion);
            this.CmdNavigateToEditCriterion = new MvvmHelpers.Commands.Command(NavigateToEditCriterion);
            this.CmdDeleteCriterion = new MvvmHelpers.Commands.Command(DeleteCriterion);
            Analytics.TrackEvent("TEST EVENT");
            try
            {
                throw new Exception();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            // Implement your implementation logic here...

            if (parameters.ContainsKey(AppNavigationParameters.AddCriterion))
            {
                Criterion criterion = (Criterion)parameters[AppNavigationParameters.AddCriterion];
                if (!this.CriterionList.Any(c => c.ToolId == criterion.ToolId))
                {
                    var listCrit = new List<Criterion>();
                    listCrit.AddRange(this.CriterionList);
                    listCrit.Add(criterion);
                    this.CriterionList = listCrit;
                }
            }

            if (parameters.ContainsKey(AppNavigationParameters.EditCriterion))
            {
                Criterion criterion = (Criterion)parameters[AppNavigationParameters.EditCriterion];
                if (this.CriterionList.Any(c => c.ToolId == criterion.ToolId))
                {
                    var listCrit = new List<Criterion>();
                    listCrit.AddRange(this.CriterionList);
                    listCrit.FirstOrDefault(c => c.ToolId == criterion.ToolId).Quantity = criterion.Quantity;
                    this.CriterionList = listCrit;
                }
            }
        }

        private void HandleIsActiveTrue(object sender, EventArgs args)
        {
            if (IsActive == false) return;

            // Handle Logic Here
            this.SessionStore.LastTab = nameof(Recherche);
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

        protected virtual async void NavigateToAddCriterion()
        {
            await NavigationService.NavigateAsync("AddCriterion");
        }

        protected virtual async void DeleteCriterion()
        {
            var listCrit = new List<Criterion>();
            listCrit.AddRange(this.CriterionList);
            listCrit.Remove(listCrit.FirstOrDefault(c => c.ToolId == Selectedcriterion.ToolId));
            this.CriterionList = listCrit;
        }

        protected virtual async void NavigateToEditCriterion()
        {
            var parameters = new NavigationParameters();
            parameters.Add(AppNavigationParameters.EditToCriterion, this.CriterionList.FirstOrDefault(c => c.ToolId == this.Selectedcriterion.ToolId));
            await NavigationService.NavigateAsync("AddCriterion", parameters);
        }

        protected virtual async void NavigateToResultSearch()
        {
            if (this.SelectedSpaceType != null && this.DateDebut != null && this.DateFin != null && this.TimeDebut != null && this.TimeFin != null)
            {
                var parameters = new NavigationParameters();

                parameters.Add(AppNavigationParameters.ResultatRechercheDateDebut, this.DateDebut);
                parameters.Add(AppNavigationParameters.ResultatRechercheDateFin, this.DateFin);
                parameters.Add(AppNavigationParameters.ResultatRechercheTimeDebut, this.TimeDebut);
                parameters.Add(AppNavigationParameters.ResultatRechercheTimeFin, this.TimeFin);

                await NavigationService.NavigateAsync("ResultsSearch", parameters);
            }
            else
            {
                this.IsBusy = false;
                await this.DialogService.DisplayAlertAsync(DefaultStrings.RenseignerInformationTitle, DefaultStrings.RenseignerInformation, DefaultStrings.OkButton);
            }
        }

    }
}
