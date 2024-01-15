using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform;
using MyWeather.Views;
using Newtonsoft.Json;

namespace MyWeather.Class
{
    public static class Save
    {
        public static event Action OnSave;

        public static string PathFileSettings => "SaveSettings.json";
        public static string PathFileLove => "SaveLove.json";
        public static Settings Settings { get; set; } = new Settings(true,true,true);
        public static List<Love> Loves { get; set; } = new List<Love>();

        public static T LoadData<T>(string pathFile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(pathFile))
                {
                    return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                }
            }
            catch 
            {
                return JsonConvert.DeserializeObject<T>("");
            }
            
        }

        public static void SaveData<T>(T obj, string pathFile)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathFile))
                {
                    string json = JsonConvert.SerializeObject(obj);
                    sw.Write(json);
                    OnSave?.Invoke();
                }
            }
            catch (Exception e)
            {
            }
        }

        public static void InitFilesForSave()
        {
            if(!File.Exists(PathFileSettings))
            {
                SaveData(Settings, PathFileSettings);
            }
            if (!File.Exists(PathFileLove))
            {
                SaveData(Loves, PathFileLove);
            }
        }
    }

    public class Settings
    {
        public bool Temp { get; set; }
        public bool Speed { get; set; }
        public bool Pressure { get; set; }

        public Settings(bool temp, bool speed, bool pressure)
        {
            Temp = temp;
            Speed = speed;
            Pressure = pressure;
        }
    }

    public class Love
    {
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

        public Love(string name, string lat, string lon)
        {
            Name = name;
            Lat = lat;
            Lon = lon;
        }
    }
}
