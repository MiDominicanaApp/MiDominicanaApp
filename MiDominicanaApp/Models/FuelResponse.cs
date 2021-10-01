using System;
using System.Text.Json.Serialization;

namespace MiDominicanaApp.Models
{
    public class FuelResponse
    {

        [JsonPropertyName("gasolinaPremium")]
        public string PremiumGasoline { get; set; }

        [JsonPropertyName("gasolinaRegular")]
        public string RegularGasoline { get; set; }

        [JsonPropertyName("gasoilOptimo")]
        public string OptimalDiesel { get; set; }

        [JsonPropertyName("gasoilRegular")]
        public string RegularDiesel { get; set; }

        [JsonPropertyName("kerosene")]
        public string Kerosene { get; set; }

        [JsonPropertyName("gasLicuadoPetroleoGLP")]
        public string PetroleumLiquidGas { get; set; }

        [JsonPropertyName("gasNaturalVehicularGNV")]
        public string NaturalGas { get; set; }
    }

}
