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
    public class AddParticipantViewModel : ViewModelBase
    {
        public ICommand CmdAddParticipant { get; set; }

        private List<User> _tempListUser;
        private List<User> _listParticipant;
        private List<User> _listUsers;

        public List<User> ListUsers
        {
            get { return this._listUsers; }
            set { SetProperty(ref _listUsers, value); }
        }

        private User _selectedUser;

        public User SelectedUser
        {
            get { return this._selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        private string _searchText;

        public string SearchText
        {
            get { return this._searchText; }
            set 
            {
                SetProperty(ref _searchText, value);

                if (string.IsNullOrEmpty(value))
                {
                    ListUsers = new List<User>();
                    ListViewVisible = false;
                }
                else
                {
                    ListUsers = _tempListUser.Where(u => !this._listParticipant.Any(p => p.Id == u.Id) && u.Email.Contains(value) && SessionStore.UserName != u.Email).ToList();
                    ListViewVisible = true;
                }
            }
        }

        private bool _listViewVisible;

        public bool ListViewVisible
        {
            get { return this._listViewVisible; }
            set { SetProperty(ref _listViewVisible, value); }
        }

        public AddParticipantViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
            this.Title = "Ajouter un participant";
            this.ListViewVisible = false;

            this.CmdAddParticipant = new Command(ValidParticipant);

            this._tempListUser = new List<User>
            {
                new User{ Id = 1, Email = "hyacinthe.briand@diiage.org"},
                new User{ Id = 2, Email = "gael.peltey@diiage.org"},
                new User{ Id = 3, Email = "johann.kazal@diiage.org"},
                new User{ Id = 4, Email = "thomas.carayon@diiage.org"},
                new User{ Id = 5, Email = "nicolas.wolski@diiage.org"},
                new User{ Id = 6, Email = "alexis.dupont@diiage.org"},
                new User{ Id = 7, Email = "florent.seurre@diiage.org"}
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(AppNavigationParameters.ParticipantAddParticipant))
            {
                this._listParticipant = (List<User>)parameters[AppNavigationParameters.ParticipantAddParticipant];
            }
        }

        protected virtual async void ValidParticipant()
        {
            this.IsBusy = true;
            if (this.SelectedUser != null)
            {
                this.IsBusy = true;

                var parameters = new NavigationParameters();
                parameters.Add(AppNavigationParameters.AddParticipant, this.SelectedUser);

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
