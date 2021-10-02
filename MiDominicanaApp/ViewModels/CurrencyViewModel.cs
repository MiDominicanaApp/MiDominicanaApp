using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using Prism.Services;
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
        IPageDialogService _pageDialog;
        public ObservableCollection<Currency> CurrenciesList { get; set; } = new ObservableCollection<Currency>() { };
        public CurrencyViewModel(IMiDominicanaApiService currencyApiService, IPageDialogService pageDialog)
        {
            _currencyApiService = currencyApiService;
            _pageDialog = pageDialog;
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
                    var currencies = JsonSerializer.Deserialize<Currency>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    CurrenciesList.Add(new Currency()
                    {
                        Name = currencies.Name,
                        Purchase = Convert.ToDouble(currencies.Purchase),
                        Sale = Convert.ToDouble(currencies.Sale)
                    });

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
