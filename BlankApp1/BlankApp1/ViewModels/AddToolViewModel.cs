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
    public class AddToolViewModel : ViewModelBase
    {
        public ICommand CmdAddTool { get; set; }

        private string _label;

        public string Label
        {
            get { return this._label; }
            set { SetProperty(ref _label, value); }
        }

        public AddToolViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Ajouter équipement";

            this.CmdAddTool = new Command(AddTool);
        }

        protected virtual async void AddTool()
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
