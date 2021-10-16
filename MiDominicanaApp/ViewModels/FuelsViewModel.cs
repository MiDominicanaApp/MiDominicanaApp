using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using MiDominicanaApp.Models;
using MiDominicanaApp.Services;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace MiDominicanaApp.ViewModels
{
    public class FuelsViewModel : BaseViewModel
    {

        IMiDominicanaApiService _miDominicanaApiService;
        IPageDialogService _pageDialog;
        public string Loading { get; set; }
        public string Date { get; set; }
        public string UpdateMessage { get; set; }
        public ObservableCollection<Fuel> Fuels { get; set; } = new ObservableCollection<Fuel>() { };
        public FuelsViewModel(IMiDominicanaApiService miDominicanaApiService, IPageDialogService pageDialog)
        {
            _miDominicanaApiService = miDominicanaApiService;
            _pageDialog = pageDialog;
            Loading = "Loading...";
            Task.Run(async () => {
                await LoadFuelsAsync();
                Loading = "";
            });
        }

        public async Task LoadFuelsAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var response = await _miDominicanaApiService.GetFuelsAsync();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var fuels = JsonSerializer.Deserialize<FuelResponse>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    var format = "MMMM dd, yyyy";
                    var input = DateTime.Today.ToString(format);
                    var dt = DateTime.ParseExact(input, format, new CultureInfo("en-US"));
                    Date = dt.ToString("dd 'de' MMMM, yyyy", new CultureInfo("es-ES"));
                    UpdateMessage = DateTime.Now.ToString("'Actualizado por última vez el' dddd dd 'de' MMMM 'a las' HH:mm", new CultureInfo("es-ES"));

                    // Save Fuels
                    Fuels.Add(new Fuel()
                    {
                        Name = "Gasolina Premium",
                        Price = Convert.ToDouble(fuels.PremiumGasoline)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "Gasolina Regular",
                        Price = Convert.ToDouble(fuels.RegularGasoline)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "Gasoil Optimo",
                        Price = Convert.ToDouble(fuels.OptimalDiesel)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "Gasoil Regular",
                        Price = Convert.ToDouble(fuels.RegularDiesel)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "Kerosene",
                        Price = Convert.ToDouble(fuels.Kerosene)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "Gas Licuado Petroleo (GLP)",
                        Price = Convert.ToDouble(fuels.PetroleumLiquidGas)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "Gas Natural Vehicular (GNV)",
                        Price = Convert.ToDouble(fuels.NaturalGas)
                    });
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _pageDialog.DisplayAlertAsync("Alerta", Alert.NoInternetConnection, "OK");
                });
            }
        }

    }
}
