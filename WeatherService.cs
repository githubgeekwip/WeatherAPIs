using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherAPI.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace WeatherAPI
{
    //Serialize and deserialize response and wraps in a custom wrapper object
    public class WeatherService
    {
        private static string _weatherresponse = null;
        public static string SerializeDeserialize(string weatherresp)
        { 
            string jsonresponse = null;
            try
            {
                _weatherresponse = weatherresp;
                var content = JsonConvert.DeserializeObject<CustomWeatherWrapper>(_weatherresponse).WeatherForecast;
                jsonresponse = JsonConvert.SerializeObject(content).ToString();
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error in SerializeDeserialize response,Error {0}",ex.Message));

            }
            return jsonresponse;
        }

    }
}