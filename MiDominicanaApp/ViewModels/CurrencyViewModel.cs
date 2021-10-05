using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public string UpdateMessage { get; set; }

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


                    var format = "MMMM dd, yyyy";
                    var input = DateTime.Today.ToString(format);
                    var dt = DateTime.ParseExact(input, format, new CultureInfo("en-US"));
                    UpdateMessage = DateTime.Now.ToString("'Actualizado por última vez el' dddd dd 'de' MMMM 'a las' HH:mm", new CultureInfo("es-ES"));

                    string[] flags = new string[] { 
                    "usa.png",
                    "canada.png",
                    "euro.png"
                    };

                    int count = 0;
                    foreach (var currency in currencyList)
                    {
                        CurrenciesList.Add(new Currency()
                        {
                            Name = currency.Name,
                            Purchase = Convert.ToDouble(currency.Purchase),
                            Sale = Convert.ToDouble(currency.Sale),
                            ImagePath = flags[count]
                        });
                        count++;
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
