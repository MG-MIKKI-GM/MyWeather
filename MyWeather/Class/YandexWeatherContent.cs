using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Newtonsoft.Json;

namespace MyWeather.Class
{
    public class Day
    {
        public int temp_min { get; set; }
        public int temp_avg { get; set; }
        public int temp_max { get; set; }
        public double wind_speed { get; set; }
        public string wind_dir { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
        public string condition { get; set; }
        public string daytime { get; set; }
        public string uv_index { get; set; }
    }
    public class Evening
    {
        public int temp_min { get; set; }
        public int temp_avg { get; set; }
        public int temp_max { get; set; }
        public double wind_speed { get; set; }
        public string wind_dir { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
        public string condition { get; set; }
        public string daytime { get; set; }
        public string uv_index { get; set; }
    }

    public class Fact
    {
        public int temp { get; set; }
        public string condition { get; set; }
        public double wind_speed { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
        public string daytime { get; set; }
        public string uv_index { get; set; }

    }

    public class Forecast
    {
        public string date { get; set; }
        public string sunset { get; set; }
        public string rise_begin { get; set; }
        public Parts parts { get; set; }
        public List<Hour> hours { get; set; }
    }

    public class Hour
    {
        private string h;
        public string hour { get => h; set { if (value != "") { h = string.Format("{0:d2}", int.Parse(value)); h = h + ":00"; } else { h = "00:00"; } } }
        public int temp { get; set; }
        public string condition { get; set; }
        public double wind_speed { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
    }

    public class Morning
    {
        public int temp_min { get; set; }
        public int temp_avg { get; set; }
        public int temp_max { get; set; }
        public double wind_speed { get; set; }
        public string wind_dir { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
        public string condition { get; set; }
        public string daytime { get; set; }
        public string uv_index { get; set; }
    }

    public class Night
    {
        public int temp_min { get; set; }
        public int temp_avg { get; set; }
        public int temp_max { get; set; }
        public double wind_speed { get; set; }
        public string wind_dir { get; set; }
        public int pressure_mm { get; set; }
        public int pressure_pa { get; set; }
        public int humidity { get; set; }
        public string condition { get; set; }
        public string daytime { get; set; }
        public string uv_index { get; set; }
    }

    public class Parts
    {
        public Night night { get; set; }
        public Day day { get; set; }
        public Evening evening { get; set; }
        public Morning morning { get; set; }
    }
    public class WeatherContent
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
        public Fact fact { get; set; }
        public List<Forecast> forecasts { get; set; }

        public static Bitmap GetIconWeather(string condition, string daytime)
        {
            bool day = daytime == "d" ? true : false;

            Dictionary<string, Bitmap> Icons = new Dictionary<string, Bitmap>()
            {
                { "clear",              new Bitmap(AssetLoader.Open(new Uri(day ? "avares://MyWeather/Assets/sun.png" : "avares://MyWeather/Assets/moon.png"))) },
                { "partly-cloudy",      new Bitmap(AssetLoader.Open(new Uri(day ? "avares://MyWeather/Assets/sunovercast.png" : "avares://MyWeather/Assets/moonovercast.png"))) },
                { "cloudy",             new Bitmap(AssetLoader.Open(new Uri(day ? "avares://MyWeather/Assets/sunovercast.png" : "avares://MyWeather/Assets/moonovercast.png"))) },
                { "overcast",           new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/overcast.png"))) },
                { "light-rain",         new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/lightrain.png"))) },
                { "rain",               new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/rain.png"))) },
                { "heavy-rain",         new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/hardrain.png"))) },
                { "showers",            new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/hardrain.png"))) },
                { "wet-snow",           new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/snowrain.png"))) },
                { "light-snow",         new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/lightsnow.png"))) },//
                { "snow",               new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/lightsnow.png"))) },//
                { "snow-showers",       new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/snow.png"))) },//
                { "hail",               new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/rain.png"))) },
                { "thunderstorm",       new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/lightning.png"))) },
                { "thunderstorm-with-rain", new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/lightningrain.png"))) },
                { "thunderstorm-with-hail", new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/lightningrain.png"))) },
            };
            if(Icons.ContainsKey(condition))
                return Icons[condition];
            return new Bitmap(AssetLoader.Open(new Uri("avares://MyWeather/Assets/overcast.png")));
        }

        public static string GetConditionInRu(string condition)
        {
            Dictionary<string, string> InRus = new Dictionary<string, string>()
            {
                { "clear",                      "Ясно" },
                { "partly-cloudy",              "Малооблачно" },
                { "cloudy",                     "Облачно с прояснениями" },
                { "overcast",                   "Пасмурно" },
                { "light-rain",                 "Небольшой дождь" }, 
                { "rain",                       "Дождь" }, 
                { "heavy-rain",                 "Сильный дождь" }, 
                { "showers",                    "Ливень" }, 
                { "wet-snow",                   "Дождь со снегом" }, 
                { "light-snow",                 "Небольшой снег" }, 
                { "snow",                       "Снег" }, 
                { "snow-showers",               "Снегопад" }, 
                { "hail",                       "Град" },
                { "thunderstorm",               "Гроза" },
                { "thunderstorm-with-rain",     "Дождь с грозой" },
                { "thunderstorm-with-hail",     "Гроза с градом" },
            };
            if (InRus.ContainsKey(condition))
                return InRus[condition];
            return "Mалооблачно";
        }
    }

    /*
        clear — ясно.
        partly-cloudy — малооблачно.
        cloudy — облачно с прояснениями.
        overcast — пасмурно.
        light-rain — небольшой дождь.
        rain — дождь.
        heavy-rain — сильный дождь.
        showers — ливень.
        wet-snow — дождь со снегом.
        light-snow — небольшой снег.
        snow — снег.
        snow-showers — снегопад.
        hail — град.
        thunderstorm — гроза.
        thunderstorm-with-rain — дождь с грозой.
        thunderstorm-with-hail — гроза с градом.
     */
}
