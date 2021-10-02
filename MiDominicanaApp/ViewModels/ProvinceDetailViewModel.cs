using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiDominicanaApp.ViewModels
{
    class ProvinceDetailViewModel : BaseViewModel
    {
        IMiDominicanaApiService _miDominicanaApiService;
        public Province province { get; set; } = new Province();
        public ProvinceDetailViewModel(IMiDominicanaApiService miDominicanaApiService)
        {
            _miDominicanaApiService = miDominicanaApiService;
            GetProvince();
        }

        private async void OnLoad()
        {
            await GetProvince();
        }

        private async Task GetProvince()
        {
            var rnd = new Random();
            var provinceId = rnd.Next(1, 32);
            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var provinceResponse = await _miDominicanaApiService.GetProvinceDetailAsync(provinceId);
                if (provinceResponse != null)
                {
                    var responseContent = await provinceResponse.Content.ReadAsStringAsync();
                    province = JsonSerializer.Deserialize<Province>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "No hay conexión a internet.", "OK");
                });
            }


            //await _alertService.Alert("Error", "NO HAY CONEXION A INTERNET", "OK");
        }
    }
}
