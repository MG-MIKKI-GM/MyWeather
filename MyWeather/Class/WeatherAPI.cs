using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace MyWeather.Class
{
    public static class WeatherAPI
    {
        static HttpClient client = new HttpClient();
        static string Key = "ec6bdb6f-e0c8-4d91-9960-e2d2043b71e3";

        public async static Task<(string, string, string)> GetLatAndLonbyCityName(string city)
        {
            string uri = "https://catalog.api.2gis.com/3.0/items/geocode?q=" + city + "&fields=items.point&key=fec99ecb-b38c-4630-a81b-9444e0bf0c1c";

            string result = await (await client.GetAsync(uri)).Content.ReadAsStringAsync();
            string lat = "", lon = "", cityName = "";
            lat = JsonController.JsonToText(result, "point.lat");
            lon = JsonController.JsonToText(result, "point.lon");
            cityName = JsonController.JsonToText(result, "items.name");
            return (lat, lon, cityName);
        }

        public async static Task<WeatherContent> GetWeather(string lat, string lon)
        {
            try
            {
                string url = "https://api.weather.yandex.ru/v2/forecast?lat=" + lat + "&lon=" + lon + "&lang=ru_RU&limit=7&hours=true&extra=true";
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("X-Yandex-API-Key", Key);

                string result = await (await client.SendAsync(requestMessage)).Content.ReadAsStringAsync();

                WeatherContent weatherContent = new WeatherContent();
                Fact fact = new Fact();
                List<Forecast> forecasts = new List<Forecast>();

                weatherContent.fact = fact;
                weatherContent.forecasts = forecasts;

                weatherContent.Lat = lat;
                weatherContent.Lon = lon;

                try { fact.temp =  int.Parse(JsonController.JsonToText(result, nameof(fact.temp))); } catch { }
                try { fact.condition =  (JsonController.JsonToText(result, nameof(fact.condition))); } catch { }
                try { fact.wind_speed =  double.Parse(JsonController.JsonToText(result, nameof(fact.wind_speed))); } catch { }
                try { fact.pressure_mm =  int.Parse(JsonController.JsonToText(result, nameof(fact.pressure_mm))); } catch { }
                try { fact.pressure_pa =  int.Parse(JsonController.JsonToText(result, nameof(fact.pressure_pa))); } catch { }
                try { fact.humidity =  int.Parse(JsonController.JsonToText(result, nameof(fact.humidity))); } catch { }
                try { fact.daytime =  (JsonController.JsonToText(result, nameof(fact.daytime))); } catch { }
                try { fact.uv_index =  (JsonController.JsonToText(result, nameof(fact.uv_index))); } catch { }
                
                for (int i = 0; i < 7; i++)
                {
                    Forecast forecast = new Forecast();
                    forecast.parts = new Parts();
                    forecast.parts.day = new Day();
                    try { forecast.date = JsonController.JsonToText(result, $"forecasts[{i}].date"); } catch { }
                    try { forecast.sunset = JsonController.JsonToText(result, $"forecasts[{i}].sunset"); } catch { }
                    try { forecast.rise_begin = JsonController.JsonToText(result, $"forecasts[{i}].rise_begin"); } catch { }
                    try { forecast.parts.day.temp_min = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.day.temp_min")); } catch { }
                    try { forecast.parts.day.temp_max = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.day.temp_max")); } catch { }
                    try { forecast.parts.day.temp_avg = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.day.temp_avg")); } catch { }
                    try { forecast.parts.day.wind_speed = double.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.day.wind_speed")); } catch { }
                    try { forecast.parts.day.wind_dir = (JsonController.JsonToText(result, $"forecasts[{i}].parts.day.wind_dir")); } catch { }
                    try { forecast.parts.day.pressure_mm = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.day.pressure_mm")); } catch { }
                    try { forecast.parts.day.pressure_pa = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.day.pressure_pa")); } catch { }
                    try { forecast.parts.day.humidity = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.day.humidity")); } catch { }
                    try { forecast.parts.day.condition = (JsonController.JsonToText(result, $"forecasts[{i}].parts.day.condition")); } catch { }
                    try { forecast.parts.day.daytime = (JsonController.JsonToText(result, $"forecasts[{i}].parts.day.daytime")); } catch { }
                    try { forecast.parts.day.uv_index = (JsonController.JsonToText(result, $"forecasts[{i}].parts.day.uv_index")); } catch { }

                    forecast.parts.night = new Night();
                    try { forecast.parts.night.temp_min = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.night.temp_min")); } catch { }
                    try { forecast.parts.night.temp_max = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.night.temp_max")); } catch { }
                    try { forecast.parts.night.temp_avg = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.night.temp_avg")); } catch { }
                    try { forecast.parts.night.wind_speed = double.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.night.wind_speed")); } catch { }
                    try { forecast.parts.night.wind_dir = (JsonController.JsonToText(result, $"forecasts[{i}].parts.night.wind_dir")); } catch { }
                    try { forecast.parts.night.pressure_mm = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.night.pressure_mm")); } catch { }
                    try { forecast.parts.night.pressure_pa = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.night.pressure_pa")); } catch { }
                    try { forecast.parts.night.humidity = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.night.humidity")); } catch { }
                    try { forecast.parts.night.condition = (JsonController.JsonToText(result, $"forecasts[{i}].parts.night.condition")); } catch { }
                    try { forecast.parts.night.daytime = (JsonController.JsonToText(result, $"forecasts[{i}].parts.night.daytime")); } catch { }
                    try { forecast.parts.night.uv_index = (JsonController.JsonToText(result, $"forecasts[{i}].parts.night.uv_index")); } catch { }

                    forecast.parts.evening = new Evening();
                    try { forecast.parts.evening.temp_min = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.temp_min")); } catch { }
                    try { forecast.parts.evening.temp_max = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.temp_max")); } catch { }
                    try { forecast.parts.evening.temp_avg = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.temp_avg")); } catch { }
                    try { forecast.parts.evening.wind_speed = double.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.wind_speed")); } catch { }
                    try { forecast.parts.evening.wind_dir = (JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.wind_dir")); } catch { }
                    try { forecast.parts.evening.pressure_mm = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.pressure_mm")); } catch { }
                    try { forecast.parts.evening.pressure_pa = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.pressure_pa")); } catch { }
                    try { forecast.parts.evening.humidity = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.humidity")); } catch { }
                    try { forecast.parts.evening.condition = (JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.condition")); } catch { }
                    try { forecast.parts.evening.daytime = (JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.daytime")); } catch { }
                    try { forecast.parts.evening.uv_index = (JsonController.JsonToText(result, $"forecasts[{i}].parts.evening.uv_index")); } catch { }

                    forecast.parts.morning = new Morning();
                    try { forecast.parts.morning.temp_min = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.temp_min")); } catch { }
                    try { forecast.parts.morning.temp_max = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.temp_max")); } catch { }
                    try { forecast.parts.morning.temp_avg = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.temp_avg")); } catch { }
                    try { forecast.parts.morning.wind_speed = double.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.wind_speed")); } catch { }
                    try { forecast.parts.morning.wind_dir = (JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.wind_dir")); } catch { }
                    try { forecast.parts.morning.pressure_mm = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.pressure_mm")); } catch { }
                    try { forecast.parts.morning.pressure_pa = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.pressure_pa")); } catch { }
                    try { forecast.parts.morning.humidity = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.humidity")); } catch { }
                    try { forecast.parts.morning.condition = (JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.condition")); } catch { }
                    try { forecast.parts.morning.daytime = (JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.daytime")); } catch { }
                    try { forecast.parts.morning.uv_index = (JsonController.JsonToText(result, $"forecasts[{i}].parts.morning.uv_index")); } catch { }

                    List<Hour> hours = new List<Hour>();
                    forecast.hours = hours;

                    for(int j = 0; j < 24; j++)
                    {
                        Hour hour = new Hour();
                        try { hour.hour = JsonController.JsonToText(result, $"forecasts[{i}].hours[{j}].hour"); } catch { }
                        try { hour.temp = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].hours[{j}].temp")); } catch { }
                        try { hour.condition = JsonController.JsonToText(result, $"forecasts[{i}].hours[{j}].condition"); } catch { }
                        try { hour.wind_speed = double.Parse(JsonController.JsonToText(result, $"forecasts[{i}].hours[{j}].wind_speed")); } catch { }
                        try { hour.pressure_mm = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].hours[{j}].pressure_mm")); } catch { }
                        try { hour.pressure_pa = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].hours[{j}].pressure_pa")); } catch { }
                        try { hour.humidity = int.Parse(JsonController.JsonToText(result, $"forecasts[{i}].hours[{j}].humidity")); } catch { }

                        hours.Add(hour);
                    }

                    forecasts.Add(forecast);
                }

                return weatherContent;
            }
            catch (Exception)
            {
                return new WeatherContent();
            }
        }
    }
}
