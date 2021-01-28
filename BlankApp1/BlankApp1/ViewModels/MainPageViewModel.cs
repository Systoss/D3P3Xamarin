using BlankApp1.ApiCognitos;
using BlankApp1.Util;
using Prism.Navigation;
using Prism.Services;

namespace BlankApp1.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, ISessionStore sessionStore, IApiCognito apiCognito, IPageDialogService dialogService) : 
            base(navigationService, sessionStore, apiCognito, dialogService)
        {
        }
    }
}
