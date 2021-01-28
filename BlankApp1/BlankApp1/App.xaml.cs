using Amazon.CognitoIdentityProvider;
using BlankApp1.Util;
using BlankApp1.ViewModels;
using BlankApp1.Views;
using Prism;
using Prism.Ioc;
using System;
using System.IO;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using BlankApp1.ApiCognitos;
using Amazon;
using System.Threading.Tasks;
using Prism.Navigation;
using BlankApp1.Models;
using System.Collections.Generic;

namespace BlankApp1
{
    public partial class App
    {
        public ISessionStore Session { get; set; }
        public IApiCognito AuthApi { get; set; }
        public AmazonCognitoIdentityProviderConfig ClientHttpFactory { get; set; }

        public App(IPlatformInitializer initializer, AmazonCognitoIdentityProviderConfig _ClientHttpFactory)
            : base(initializer)
        {
            this.ClientHttpFactory = _ClientHttpFactory;

            this.AuthApi.ClientHttpConfig = this.ClientHttpFactory;

            InitializeMainPage();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.AuthApi = new ApiCognito();
            this.Session = new SessionStore { Settings = Plugin.Settings.CrossSettings.Current };

            this.AuthApi.PoolId = "us-east-1_hsCcxO7ni"; // Change to <Your Pool Id>
            this.AuthApi.ClientId = "3v9fnug1vqvesq9kecnmdr91si"; // Change to <Your Client Id>
            this.AuthApi.RegionEndpoint = RegionEndpoint.USEast1; // Change to <Your Region>
        }

        public async void InitializeMainPage()
        {
            var parameters = new NavigationParameters();

            if (Session.IsLoggedIn(DateTime.Now.ToUniversalTime()))
            {
                if (Session.IsAdmin)
                {
                    parameters.Add(KnownNavigationParameters.CreateTab, nameof(Administration));
                }
                else
                {
                    parameters.Add(KnownNavigationParameters.CreateTab, nameof(Views.User));
                }

                parameters.Add(KnownNavigationParameters.CreateTab, nameof(Recherche));
                parameters.Add(KnownNavigationParameters.CreateTab, nameof(Availability));

                await NavigationService.NavigateAsync("NavigationPage/MainPage" + parameters);
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/SignIn");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<ISessionStore>(t => this.Session);
            containerRegistry.RegisterSingleton<IApiCognito>(t => this.AuthApi);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<UpdatePassword, UpdatePasswordViewModel>();
            containerRegistry.RegisterForNavigation<AddCriterion, AddCriterionViewModel>();
            containerRegistry.RegisterForNavigation<ListAvailability, ListAvailabilityViewModel>();
            containerRegistry.RegisterForNavigation<AddReservation, AddReservationViewModel>();
            containerRegistry.RegisterForNavigation<AddParticipant, AddParticipantViewModel>();
            containerRegistry.RegisterForNavigation<AddSpace, AddSpaceViewModel>();
            containerRegistry.RegisterForNavigation<AddTool, AddToolViewModel>();
            containerRegistry.RegisterForNavigation<AddToolToSpace, AddToolToSpaceViewModel>();
            containerRegistry.RegisterForNavigation<AddSpaceType, AddSpaceTypeViewModel>();
            containerRegistry.RegisterForNavigation<ResultsSearch, ResultsSearchViewModel>();

            //Tab
            containerRegistry.RegisterForNavigation<Administration, AdministrationViewModel>();
            containerRegistry.RegisterForNavigation<Views.User, UserViewModel>();
            containerRegistry.RegisterForNavigation<Availability, AvailabilityViewModel>();
            containerRegistry.RegisterForNavigation<Recherche, RechercheViewModel>();
        }
    }
}
