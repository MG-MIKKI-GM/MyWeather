using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyWeather.Class
{
    public static class IPAPI
    {
        static HttpClient client = new HttpClient();
        public async static Task<string> GetTheCityByIP()
        {
            try
            {
                string url = "https://ipwho.is/?lang=ru";
                string result = await (await client.GetAsync(url)).Content.ReadAsStringAsync();
                if(result.Contains("city"))
                {
                    result = JsonController.JsonToText(result, "city");//Regex.Match(Regex.Match(result, @"city\W:\W[\\u\w\d -]+").Value, @":\W[\\u\w\d -]+").Value.Trim(':', '"', ' ');
                    result = Regex.Unescape(result);
                    if (result != "") return result;
                }
                return "Санкт-Петербург";
            }
            catch (Exception)
            {
                return "Москва";
            }
        }

        public async static Task<(string,string)> GetTheLatAndLogByIP()
        {
            try
            {
                string url = "https://ipwho.is/?lang=ru";
                string result = await (await client.GetAsync(url)).Content.ReadAsStringAsync();
                if(result.Contains("longitude"))
                {
                    string lat = JsonController.JsonToText(result, "latitude");//Regex.Match(Regex.Match(result, @"latitude\W:[\d.]+").Value, @"[\d.]+").Value;
                    string log = JsonController.JsonToText(result, "longitude");//Regex.Match(Regex.Match(result, @"longitude\W:[\d.]+").Value, @"[\d.]+").Value;

                    if (lat != "" && log != "") return (lat, log);
                }    

                return ("59.9386", "30.3141");
            }
            catch (Exception)
            {
                return ("55.7522", "37.6156");
            }
        }
    }
}
