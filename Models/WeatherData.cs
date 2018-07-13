using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAPI.Models
{
   //Custom Model for wrapping API response and can be refractored for future reponse schema changes.
   public class CustomWeatherWrapper
    {
        [JsonProperty("list")]
        public IEnumerable<WeatherWrapper> WeatherForecast { get; set; }
    }
   public class WeatherWrapper
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }
        [JsonProperty("main")]
        public Main CustomData1 { get; set; }
        [JsonProperty("wind")]
        public Wind CustomData2 { get; set; }
        [JsonProperty("dt_txt")]
        public DateTime Dt_txt { get; set; }
    }
  
    public class Main
    {
        [JsonProperty("temp")]
        public double Temparature { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }     
    }
}