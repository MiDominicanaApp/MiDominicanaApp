using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MiDominicanaApp.Models
{
    public class CurrencyResponse
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("purchase")]
        public double Purchase { get; set; }

        [JsonPropertyName("sale")]
        public double Sale { get; set; }

        [JsonPropertyName("image")]
        public string ImagePath { get; set; }

    }
}
