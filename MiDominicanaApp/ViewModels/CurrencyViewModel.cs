using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiDominicanaApp.ViewModels
{
    public class CurrencyViewModel : BaseViewModel
    {

        IMiDominicanaApiService _currencyApiService;
        public Currency currency { get; set; } = new Currency();
        public ObservableCollection<Currency> CurrenciesList { get; set; } = new ObservableCollection<Currency>();
        public CurrencyViewModel(IMiDominicanaApiService currencyApiService)
        {
            _currencyApiService = currencyApiService;
            LoadCurrenciesAsync();
        }
       
        private async Task LoadCurrenciesAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var currencyResponse = await _currencyApiService.GetCurrenciesAsync();
                if (currencyResponse != null)
                {
                    var responseString = await currencyResponse.Content.ReadAsStringAsync();
                    currency = JsonSerializer.Deserialize<Currency>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                   
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "No hay conexión a internet.", "OK");
                });
            }
        }
    }
}
