using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MiDominicanaApp.ViewModels
{
    public class ProvincesViewModel : BaseViewModel
    {
        IMiDominicanaApiService _miDominicanaApiService;
        IPageDialogService _pageDialog;
        INavigationService _navigationService;
        public string Loading { get; set; }
        public List<Province> Provinces { get; set; } = new List<Province>();
        private ICommand SelectedProvinceCommand { get; }

        private Province _province;
        public Province SelectedProvince
        {
            get
            {
                return _province;
            }
            set
            {
                _province = value;
                if (_province != null)
                {
                    SelectedProvinceCommand.Execute(_province);
                }
            }
        }

        public ProvincesViewModel(IMiDominicanaApiService miDominicanaApiService, IPageDialogService pageDialog, INavigationService navigationService)
        {
            _miDominicanaApiService = miDominicanaApiService;
            _pageDialog = pageDialog;
            _navigationService = navigationService;
            SelectedProvinceCommand = new Command<Province>(OnPlaceSelected);
            Loading = "Loading...";
            Task.Run(
                async () =>
                {
                    await LoadProvinces();
                    Loading = "";
                });
        }

        private async void OnPlaceSelected(Province province)
        {
            var navigationParams = new NavigationParameters
            {
                {"province", SelectedProvince }
            };
            await _navigationService.NavigateAsync("ProvinceDetail", navigationParams);
        }

        private async Task LoadProvinces()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var provinceResponse = await _miDominicanaApiService.GetProvincesAsync();
                if (provinceResponse != null)
                {
                    var responseContent = await provinceResponse.Content.ReadAsStringAsync();
                    Provinces = JsonSerializer.Deserialize<List<Province>>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _pageDialog.DisplayAlertAsync("Alerta", "No hay conexión a internet.", "OK");
                });
            }
        }
    }
}
