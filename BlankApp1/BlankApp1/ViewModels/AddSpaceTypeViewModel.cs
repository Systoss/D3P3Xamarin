using BlankApp1.ApiCognitos;
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
    public class AddSpaceTypeViewModel : ViewModelBase
    {
        public ICommand CmdAddSpaceType { get; set; }

        private string _label;

        public string Label
        {
            get { return this._label; }
            set { SetProperty(ref _label, value); }
        }

        public AddSpaceTypeViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Ajouter un type d'espace";

            this.CmdAddSpaceType = new Command(AddSpaceType);

        }

        protected virtual async void AddSpaceType()
        {
            this.IsBusy = true;

            if (!string.IsNullOrEmpty(Label))
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
