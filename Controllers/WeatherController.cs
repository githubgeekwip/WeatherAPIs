using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WeatherAPI.Models;


namespace WeatherAPI.Controllers
{
    //Controller class for API calls and enabling cross origin resource processing
    [RoutePrefix("api/Weather")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class WeatherController : ApiController
    {
        private readonly string APIKEY = ConfigurationManager.AppSettings["API_KEY"].ToString();
        private string _weatherApiURL = null;



        //Forecast Accepts city parameter
        //and returns the response from api
        [Route("Forecast/{city}")]
        [HttpGet]
        public HttpResponseMessage Forecast(string city)
        {
            HttpResponseMessage apiresponse = new HttpResponseMessage();
            try
            {
                SetApiURL(city);
                var response = CallWeatherAPI();
                apiresponse.Content = new StringContent(response, System.Text.Encoding.UTF8, "application/json");
                return apiresponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
           
        }



        #region Private Methods
        //Sets the actual api url by city name
        private void SetApiURL(string cityname)
        {
            _weatherApiURL = "https://api.openweathermap.org/data/2.5/forecast?q=" + cityname.Trim() + ",DE" + "&appid=" + APIKEY;
        }

        //Makes the call to open weather api and returns response string
        private string CallWeatherAPI()
        {
            string _response = null;
            try
            {
                HttpWebRequest apiRequest = WebRequest.Create(_weatherApiURL) as HttpWebRequest;

                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        var _apiResponse = reader.ReadToEnd();
                        _response = WeatherService.SerializeDeserialize(_apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WebException(ex.Message);
            }

            return _response;
        }

        #endregion 
    }
}