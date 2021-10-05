using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MiDominicanaApp.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public double Purchase { get; set; }
        public double Sale { get; set; }
        public string ImagePath { get; set; }
    }
}
