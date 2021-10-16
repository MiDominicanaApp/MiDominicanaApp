using System;
using System.ComponentModel;
using Prism.Navigation;

namespace MiDominicanaApp.ViewModels
{
    public abstract class BaseViewModel: INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;

        protected INavigationService _navigationService;
    }
}
