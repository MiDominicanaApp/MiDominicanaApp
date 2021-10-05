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
    public class ProvincesViewModel : BaseViewModel
    {
        IMiDominicanaApiService _miDominicanaApiService;
        IPageDialogService _pageDialog;
        public string Loading { get; set; }
        public List<Province> Provinces { get; set; } = new List<Province>();

        public ProvincesViewModel(IMiDominicanaApiService miDominicanaApiService, IPageDialogService pageDialog)
        {
            _miDominicanaApiService = miDominicanaApiService;
            _pageDialog = pageDialog;
            Loading = "Loading...";
            Task.Run(
                async () =>
                {
                    await LoadProvinces();
                    Loading = "";
                });
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
