using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using Prism.Services;
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
        IPageDialogService _pageDialog;
        public string Loading { get; set; }

        public Province Province { get; set; } = new Province();

        public ProvinceDetailViewModel(IMiDominicanaApiService miDominicanaApiService, IPageDialogService pageDialog)
        {
            _miDominicanaApiService = miDominicanaApiService;
            _pageDialog = pageDialog;
            Loading = "Loading...";
            Task.Run(
                async () => { 
                    await LoadProvince(); 
                });
        }

        private async Task LoadProvince()
        {
            var rnd = new Random();
            var provinceId = rnd.Next(1, 32);
            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var provinceResponse = await _miDominicanaApiService.GetProvinceDetailAsync(provinceId);
                if (provinceResponse != null)
                {
                    var responseContent = await provinceResponse.Content.ReadAsStringAsync();
                    Province = JsonSerializer.Deserialize<Province>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
