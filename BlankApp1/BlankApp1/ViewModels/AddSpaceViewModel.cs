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
    public class AddSpaceViewModel : ViewModelBase
    {
        public ICommand CmdNavigateToAddTool { get; set; }
        public ICommand CmdAddSpace { get; set; }

        private string _name;

        public string Name
        {
            get { return this._name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;

        public string Description
        {
            get { return this._description; }
            set { SetProperty(ref _description, value); }
        }

        private int _capacite;

        public int Capacite
        {
            get { return this._capacite; }
            set { SetProperty(ref _capacite, value); }
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

        private List<Criterion> _criterionList;

        public List<Criterion> CriterionList
        {
            get { return this._criterionList; }
            set { SetProperty(ref _criterionList, value); }
        }

        public AddSpaceViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Ajouter un espace";

            this.CmdNavigateToAddTool = new Command(NavigateToAddTool);
            this.CmdAddSpace = new Command(AddSpace);
            this.CriterionList = new List<Criterion>();
            this.SpaceTypeList = new List<SpaceType>
            {
                new SpaceType{ Id= 1, Label = "Salle"},
                new SpaceType{ Id= 2, Label = "Parking"},
                new SpaceType{ Id= 3, Label = "Voiture"}
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            // Implement your implementation logic here...

            if (parameters.ContainsKey(AppNavigationParameters.AddToolToSpace))
            {
                Criterion criterion = (Criterion)parameters[AppNavigationParameters.AddToolToSpace];
                if (!this.CriterionList.Any(c => c.ToolId == criterion.ToolId))
                {
                    var listCrit = new List<Criterion>();
                    listCrit.AddRange(this.CriterionList);
                    listCrit.Add(criterion);
                    this.CriterionList = listCrit;
                }
            }
        }

        protected virtual async void AddSpace()
        {
            this.IsBusy = true;

            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description) && SpaceTypeList.Count > 0 && SelectedSpaceType != null && Capacite > 0)
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

        protected virtual async void NavigateToAddTool()
        {
            await NavigationService.NavigateAsync("AddToolToSpace");
        }
    }
}
