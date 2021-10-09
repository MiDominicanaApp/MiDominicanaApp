using System;
using MiDominicanaApp.Services;
using MiDominicanaApp.ViewModels;
using MiDominicanaApp.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiDominicanaApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer): base(platformInitializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("Home");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>(NavigationConstants.Paths.Home);
            containerRegistry.RegisterForNavigation<FuelsPage, FuelsViewModel>(NavigationConstants.Paths.Fuels);
            containerRegistry.RegisterForNavigation<CurrencyPage, CurrencyViewModel>(NavigationConstants.Paths.Currency);
            containerRegistry.RegisterForNavigation<ProvincesPage, ProvincesViewModel>(NavigationConstants.Paths.Provinces);
            containerRegistry.RegisterForNavigation<ProvinceDetailPage, ProvinceDetailViewModel>(NavigationConstants.Paths.ProvinceDetail);
            containerRegistry.Register<IMiDominicanaApiService, MiDominicanaApiService>();
        }
    }
}
