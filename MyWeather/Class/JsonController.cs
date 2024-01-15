using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyWeather.Class
{
    public static class JsonController
    {
        public static string JsonToText(string text, string data)
        {
            string[] paretns = data.Split('.');
            string t = text;
            try
            {
                if (paretns.Length > 1)
                {
                    for (int i = 0; i < paretns.Length - 1; i++)
                    {
                        int item = -1;
                        if (paretns[i].Contains('['))
                        {
                            item = int.Parse(Regex.Match(Regex.Match(paretns[i], @"[[\d]+\]").Value, @"[\d]+").Value);
                            paretns[i] = Regex.Replace(paretns[i], @"\[[\d]+\]", "");
                        }
                        t = t.Substring(t.IndexOf(paretns[i] + "\":"));
                        t = GetTextInSymbol(t);

                        if (item != -1)
                            t = GetItem(t, item);
                    }
                }

                string result = Regex.Match(t, @$"({paretns[paretns.Length - 1]}"")[^,]*").Value;
                result = result.Split("\":")[1].Trim(' ', '"', ',', ']', '}', '{', '[', '\r', '\n', '\t');

                return result;
            }
            catch
            {
                return "";
            }
        }

        private static string GetItem(string text, int item)
        {
            text = text.Trim('[', ']');
            string t = GetTextInSymbol(text);

            for (int i = 0; i < item; i++)
            {
                text = text.Replace(t, "");
                t = GetTextInSymbol(text);
            }
            return t;
        }

        private static string GetTextInSymbol(string text)
        {
            List<int> indexes = new List<int>();

            int endLength = -1;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '{' || text[i] == '[')
                {
                    indexes.Add(i);
                }
                if (text[i] == '}' || text[i] == ']')
                {
                    endLength = i;

                    int index = indexes[indexes.Count - 1];
                    indexes.RemoveAt(indexes.Count - 1);

                    if (indexes.Count == 0)
                        return text.Substring(index, endLength - index + 1);
                }
            }
            return "";
        }
    }
}
