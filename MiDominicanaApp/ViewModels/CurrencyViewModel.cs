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

        public string Loading { get; set; }
        public ObservableCollection<Currency> CurrenciesList { get; set; } = new ObservableCollection<Currency>() { };
        public CurrencyViewModel(IMiDominicanaApiService currencyApiService, IPageDialogService pageDialog)
        {
            _currencyApiService = currencyApiService;
            _pageDialog = pageDialog;
            Loading = "Loading...";
            Task.Run(async () => {
                await LoadCurrenciesAsync();
                Loading = "";
            });
        }        

        private async Task LoadCurrenciesAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var currencyResponse = await _currencyApiService.GetCurrenciesAsync();
                if (currencyResponse != null)
                {
                    var responseString = await currencyResponse.Content.ReadAsStringAsync();
                    var currencyList = JsonSerializer.Deserialize<List<CurrencyResponse>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


                    foreach (var currency in currencyList)
                    {
                        CurrenciesList.Add(new Currency()
                        {
                            Name = currency.Name,
                            Purchase = Convert.ToDouble(currency.Purchase),
                            Sale = Convert.ToDouble(currency.Sale)
                        });
                    }
                    
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
