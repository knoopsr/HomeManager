using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Homepage
{
    public class clsOpenWeatherResponse
    {
        public List<clsWeatherListItem> list { get; set; }
    }
    public class clsWeatherListItem
    {
        public long dt { get; set; }  // Tijdstempel (Unix tijd)
        public clsMainWeatherInfo main { get; set; }
        public List<clsWeatherDescription> weather { get; set; }
    }

    public class clsMainWeatherInfo
    {
        public double temp { get; set; }  // Temperatuur in °C
    }

    public class clsWeatherDescription
    {
        public string description { get; set; }  // Omschrijving zoals "bewolkt"
        public string icon { get; set; }  // Icoon ID voor weergave van afbeelding
    }
}
