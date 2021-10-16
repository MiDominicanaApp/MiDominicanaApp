using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiDominicanaApp.ViewModels
{
    public class ProvinceDetailViewModel : BaseViewModel, INavigatedAware
    {
        INavigationService _navigationService;

        public ICommand GoBackCommand { get; }
        public Province Province { get; set; } = new Province();

        public ProvinceDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new Command(async () =>
            {
                await _navigationService.GoBackAsync();
            });
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Province = (Province)parameters["province"];
        }
    }
}
