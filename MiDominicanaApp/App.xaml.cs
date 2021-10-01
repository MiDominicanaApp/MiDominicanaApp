using System;
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
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>("Home");
            //containerRegistry.RegisterForNavigation<FuelsPage, FuelsViewModel>("Fuels");
            containerRegistry.RegisterForNavigation<NavigationPage>();
        }
    }
}
