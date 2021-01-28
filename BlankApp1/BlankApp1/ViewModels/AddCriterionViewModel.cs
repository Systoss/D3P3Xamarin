using BlankApp1.Util;
using BlankApp1.ApiCognitos;
using Prism.Services;
using Prism.Navigation;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using BlankApp1.Models;
using System.Collections.Generic;
using BlankApp1.Entities;
using System.Linq;

namespace BlankApp1.ViewModels
{
    public class AddCriterionViewModel : ViewModelBase
    {
        public ICommand CmdAddCriterion { get; set; }

        private bool _isCreate;

        public bool IsCreate
        {
            get { return _isCreate; }
            set { SetProperty(ref _isCreate, value); }
        }

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


        public AddCriterionViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Ajouter un critère";
            this.CmdAddCriterion = new MvvmHelpers.Commands.Command(ValidCriterion);
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            // Implement your implementation logic here...

            if (parameters.ContainsKey(AppNavigationParameters.EditToCriterion))
            {
                this.IsCreate = false;
                Criterion criterion = (Criterion)parameters[AppNavigationParameters.EditToCriterion];
                this.SelectedTool = this.Tools.FirstOrDefault(t => t.Id == criterion.ToolId);
                this.Quantity = criterion.Quantity;
            }
            else
            {
                this.IsCreate = true;
            }
        }

        protected virtual async void ValidCriterion()
        {
            this.IsBusy = true;
            if (this.SelectedTool != null && this.Quantity > 0)
            {
                var parameters = new NavigationParameters();

                if (this.IsCreate)
                {
                    parameters.Add(AppNavigationParameters.AddCriterion, new Criterion { ToolId = this.SelectedTool.Id, ToolLabel = this.SelectedTool.Label, Quantity = this.Quantity });
                }
                else
                {
                    parameters.Add(AppNavigationParameters.EditCriterion, new Criterion { ToolId = this.SelectedTool.Id, ToolLabel = this.SelectedTool.Label, Quantity = this.Quantity });
                }

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
