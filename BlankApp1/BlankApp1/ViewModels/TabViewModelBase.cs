using BlankApp1.ApiCognitos;
using BlankApp1.Util;
using Prism;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlankApp1.ViewModels
{
    public class TabViewModelBase : ViewModelBase, IActiveAware
    {
        protected bool HasInitialized { get; set; }

        public event EventHandler IsActiveChanged;

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        public TabViewModelBase(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) :
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
        }
    }
}
