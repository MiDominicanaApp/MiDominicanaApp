using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MiDominicanaApp.ViewModels;
using MiDominicanaApp.Services;

namespace MiDominicanaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProvincesPage : ContentPage
    {
        public ProvincesPage()
        {
            InitializeComponent();
        }
    }
}