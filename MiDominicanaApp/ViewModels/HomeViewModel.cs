using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace MiDominicanaApp.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {
        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
