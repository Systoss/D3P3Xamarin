using BlankApp1.ApiCognitos;
using BlankApp1.Entities;
using BlankApp1.Models;
using BlankApp1.Util;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BlankApp1.ViewModels
{
    public class AddToolToSpaceViewModel : ViewModelBase
    {
        public ICommand CmdAddTool { get; set; }

        private List<Tool> _tools;
        public List<Tool> Tools
        {
            get { return _tools; }
            set { SetProperty(ref _tools, value); }
        }

        private Tool _selectedTool;
        public Tool SelectedTool
        {
            get { return _selectedTool; }
            set { SetProperty(ref _selectedTool, value); }
        }

        private double _quantity;
        public double Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        public AddToolToSpaceViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Ajouter un equipement à un espace";
            this.CmdAddTool = new MvvmHelpers.Commands.Command(AddTool);
            this.Tools = new List<Tool>()
            {
                new Tool()
                {
                    Id= 1,
                    Label = "Cafetière"
                },
                new Tool()
                {
                    Id= 2,
                    Label = "Chaise"
                },
                new Tool()
                {
                    Id= 3,
                    Label = "Vidéo projecteur"
                },
                new Tool()
                {
                    Id= 4,
                    Label = "Tableau"
                }
            };
        }

        protected virtual async void AddTool()
        {
            this.IsBusy = true;
            if (this.SelectedTool != null && this.Quantity > 0)
            {
                var parameters = new NavigationParameters();
                parameters.Add(AppNavigationParameters.AddToolToSpace, new Criterion { ToolId = this.SelectedTool.Id, ToolLabel = this.SelectedTool.Label, Quantity = this.Quantity });

                await this.NavigationService.GoBackAsync(parameters);
            }
            else
            {
                this.IsBusy = false;
                await this.DialogService.DisplayAlertAsync(DefaultStrings.RenseignerInformationTitle, DefaultStrings.RenseignerInformation, DefaultStrings.OkButton);
            }
        }
    }
}
