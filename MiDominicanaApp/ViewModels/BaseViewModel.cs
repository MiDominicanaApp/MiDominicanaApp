using System;
using System.ComponentModel;

namespace MiDominicanaApp.ViewModels
{
    public abstract class BaseViewModel: INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
