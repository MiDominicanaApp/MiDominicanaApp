using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Fuel> Fuels { get; set; } = new ObservableCollection<Fuel>() { };
        public FuelsViewModel(IMiDominicanaApiService miDominicanaApiService, IPageDialogService pageDialog)
        {
            _miDominicanaApiService = miDominicanaApiService;
            _pageDialog = pageDialog;
            LoadFuelsAsync();
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

                    // Save Fuels
                    Fuels.Add(new Fuel()
                    {
                        Name = "GasolinaPremium",
                        Price = Convert.ToDouble(fuels.PremiumGasoline)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "GasolinaRegular",
                        Price = Convert.ToDouble(fuels.RegularGasoline)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "GasoilOptimo",
                        Price = Convert.ToDouble(fuels.OptimalDiesel)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "GasoilRegular",
                        Price = Convert.ToDouble(fuels.RegularDiesel)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "Kerosene",
                        Price = Convert.ToDouble(fuels.Kerosene)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "GasLicuadoPetroleoGLP",
                        Price = Convert.ToDouble(fuels.PetroleumLiquidGas)
                    });
                    Fuels.Add(new Fuel()
                    {
                        Name = "GasNaturalVehicularGNV",
                        Price = Convert.ToDouble(fuels.NaturalGas)
                    });
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
