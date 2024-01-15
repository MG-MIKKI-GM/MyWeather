using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using MyWeather.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace MyWeather.Views
{
    public partial class MainPageWindows : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action ClickSettingsBtn;
        public event Action ClickLoveBtn;
        public event Action<string> ClickSearchBtn;

        WeatherCity weatherCity;
        WeatherCity WeatherCity { get => weatherCity; set { weatherCity = value; OnPropertyChanged(nameof(WeatherCity)); } }

        WeatherContent weatherContent;
        WeatherContent WeatherContent { get => weatherContent; set { weatherContent = value; OnPropertyChanged(nameof(WeatherContent)); } }

       
        public MainPageWindows()
        {
            InitializeComponent();
            LoveImage.PointerPressed += LoveImage_PointerPressed;
            SettingsImage.PointerPressed += SettingsImage_PointerPressed;
            SearchImage.PointerPressed += SearchImage_PointerPressed;
            CloseImage.PointerPressed += CloseImage_PointerPressed;
            FavouritesText.PointerPressed += FavouritesText_PointerPressed;
            MobileFavouritesText.PointerPressed += FavouritesText_PointerPressed;

            LoveImageM.PointerPressed += LoveImage_PointerPressed;
            SettingsImageM.PointerPressed += SettingsImage_PointerPressed;
            SearchImageM.PointerPressed += SearchImage_PointerPressed;

            MobileBackBtn.PointerPressed += MobileBackBtn_PointerPressed;
            WeekText.PointerPressed += WeekText_PointerPressed;

            Save.OnSave += Save_OnSave;
        }

        private void MobileBackBtn_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            Mobile7Panel.IsVisible = false;
            MobileMainPanel.IsVisible = true;
            MobileBackBtn.IsVisible = false;
        }

        private void WeekText_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            Mobile7Panel.IsVisible = true;
            MobileMainPanel.IsVisible = false;
            MobileBackBtn.IsVisible = true;
        }

        public MainPageWindows(WeatherCity weatherCity, WeatherContent weatherContent) : this()
        {
            WeatherCity = weatherCity;
            WeatherContent = weatherContent;

            UpdateData();
        }

        private void Save_OnSave()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if (WeatherContent == null) return;
            if (WeatherContent.forecasts == null) return;

            string[] times = new string[8];
            Bitmap[] icons = new Bitmap[8];

            string[] days = new string[7];
            string[] months = new string[7];
            string[] weeks = new string[7];
            string[] uFIndex = new string[7];
            string[] temps = new string[7];
            string[] sunrises = new string[7];
            string[] sunsets = new string[7];

            string[] morningSpeeds = new string[7];
            string[] daySpeeds = new string[7];
            string[] eveningSpeeds = new string[7];
            string[] nightSpeeds = new string[7];
            string[] morningHumidities = new string[7];
            string[] dayHumidities = new string[7];
            string[] eveningHumidities = new string[7];
            string[] nightHumidities = new string[7];
            string[] morningPressures = new string[7];
            string[] dayPressures = new string[7];
            string[] eveningPressures = new string[7];
            string[] nightPressures = new string[7];
            string[] condition = new string[7];
            string[] dayTemps = new string[7];
            string[] mightTemps = new string[7];
            Bitmap[] dayicons = new Bitmap[7];

            string[] maxTemps = new string[7];
            string[] minTemps = new string[7];

            for (int i = DateTime.Now.Hour, c = 0, j = 0; c < 8; c++, i++)
            {
                if (i > 23)
                {
                    i -= 24;
                    j = 1;
                }

                times[c] = weatherContent.forecasts[j].hours[i].hour;
                icons[c] = WeatherContent.GetIconWeather(weatherContent.forecasts[j].hours[i].condition, weatherContent.fact.daytime);
            }

            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Parse(weatherContent.forecasts[i].date);
                string month = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetMonthName(date.Month);
                if (month.Last() == 'ü')
                    month = month.Replace('ü', 'ÿ');
                else
                    month += 'a';
                string week = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(date.DayOfWeek);
                if (date.Day == DateTime.Now.Day)
                    week = "ñåãîäíÿ";

                days[i] = string.Format("{0:d2}", date.Day);
                months[i] = month + ",";
                weeks[i] = week;
                uFIndex[i] = weatherContent.forecasts[i].parts.day.uv_index;
                temps[i] = Save.Settings.Temp ? weatherContent.forecasts[i].parts.evening.temp_avg.ToString() : string.Format("{0:f1}", (weatherContent.forecasts[i].parts.evening.temp_avg * 1.8f + 32));
                sunrises[i] = weatherContent.forecasts[i].rise_begin;
                sunsets[i] = weatherContent.forecasts[i].sunset;

                morningSpeeds[i] = Save.Settings.Speed ? weatherContent.forecasts[i].parts.morning.wind_speed.ToString() : string.Format("{0:f0}", weatherContent.forecasts[i].parts.morning.wind_speed * 3.6d);
                morningHumidities[i] = weatherContent.forecasts[i].parts.morning.humidity.ToString();
                morningPressures[i] = Save.Settings.Pressure ? weatherContent.forecasts[i].parts.morning.pressure_mm.ToString() : weatherContent.forecasts[i].parts.morning.pressure_pa.ToString();
                daySpeeds[i] = Save.Settings.Speed ? weatherContent.forecasts[i].parts.day.wind_speed.ToString() : string.Format("{0:f0}", weatherContent.forecasts[i].parts.day.wind_speed * 3.6d);
                dayHumidities[i] = weatherContent.forecasts[i].parts.day.humidity.ToString();
                dayPressures[i] = Save.Settings.Pressure ? weatherContent.forecasts[i].parts.day.pressure_mm.ToString() : weatherContent.forecasts[i].parts.day.pressure_pa.ToString();
                eveningSpeeds[i] = Save.Settings.Speed ? weatherContent.forecasts[i].parts.evening.wind_speed.ToString() : string.Format("{0:f0}", weatherContent.forecasts[i].parts.evening.wind_speed * 3.6d);
                eveningHumidities[i] = weatherContent.forecasts[i].parts.evening.humidity.ToString();
                eveningPressures[i] = Save.Settings.Pressure ? weatherContent.forecasts[i].parts.evening.pressure_mm.ToString() : weatherContent.forecasts[i].parts.evening.pressure_pa.ToString();
                nightSpeeds[i] = Save.Settings.Speed ? weatherContent.forecasts[i].parts.night.wind_speed.ToString() : string.Format("{0:f0}", weatherContent.forecasts[i].parts.night.wind_speed * 3.6d);
                nightHumidities[i] = weatherContent.forecasts[i].parts.night.humidity.ToString();
                nightPressures[i] = Save.Settings.Pressure ? weatherContent.forecasts[i].parts.night.pressure_mm.ToString() : weatherContent.forecasts[i].parts.night.pressure_pa.ToString();
                dayicons[i] = WeatherContent.GetIconWeather(weatherContent.forecasts[i].parts.day.condition, "d");
                condition[i] = WeatherContent.GetConditionInRu(weatherContent.forecasts[i].parts.day.condition);
                maxTemps[i] = Save.Settings.Temp ? weatherContent.forecasts[i].parts.day.temp_max.ToString() : string.Format("{0:f0}", weatherContent.forecasts[i].parts.day.temp_max * 1.8f + 32);
                minTemps[i] = Save.Settings.Temp ? weatherContent.forecasts[i].parts.day.temp_min.ToString() : string.Format("{0:f0}", weatherContent.forecasts[i].parts.day.temp_min * 1.8f + 32);
            }

            WeatherCity.Days = days;
            WeatherCity.Months = months;
            WeatherCity.Weeks = weeks;
            WeatherCity.UFIndex = uFIndex;
            WeatherCity.Temps = temps;
            WeatherCity.Sunrises = sunrises;
            WeatherCity.Sunsets = sunsets;
            WeatherCity.Times = times;
            WeatherCity.Icons = icons;
            WeatherCity.MorningSpeeds = morningSpeeds;
            WeatherCity.MorningHumidities = morningHumidities;
            WeatherCity.MorningPressures = morningPressures;
            WeatherCity.DaySpeeds = daySpeeds;
            WeatherCity.DayHumidities = dayHumidities;
            WeatherCity.DayPressures = dayPressures;
            WeatherCity.EveningSpeeds = eveningSpeeds;
            WeatherCity.EveningHumidities = eveningHumidities;
            WeatherCity.EveningPressures = eveningPressures;
            WeatherCity.NightSpeeds = nightSpeeds;
            WeatherCity.NightHumidities = nightHumidities;
            WeatherCity.NightPressures = nightPressures;
            WeatherCity.Condition = condition;
            WeatherCity.Dayicons = dayicons;
            WeatherCity.MaxTemps = maxTemps;
            WeatherCity.MinTemps = minTemps;
            WeatherCity.SpeedText = Save.Settings.Speed ? "ì/c" : "êì/÷";
            WeatherCity.HumidityText = "%";
            WeatherCity.PressureText = Save.Settings.Pressure ? "ìì ðò. ñò." : "ãÏà";
            WeatherCity.TempText = Save.Settings.Temp ? "°C" : "°F";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void FavouritesText_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            WeatherCity.IsFavourites = !WeatherCity.IsFavourites;
            if(!WeatherCity.IsFavourites)
            {
                Save.Loves = Save.LoadData<List<Love>>(Save.PathFileLove);
                Love love = Save.Loves.Where(l => l.Name == WeatherCity.City).ToList()[0];
                Save.Loves.Remove(love);

                Save.SaveData(Save.Loves, Save.PathFileLove);
            }
            else
            {
                Save.Loves = Save.LoadData<List<Love>>(Save.PathFileLove);
                Save.Loves.Add(new Love(WeatherCity.City, WeatherContent.Lat, WeatherContent.Lon));

                Save.SaveData(Save.Loves, Save.PathFileLove);
            }
            UpdateData();
        }

        private void CloseImage_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SearchImage_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            ClickSearchBtn?.Invoke(SearchTextBox.Text ?? WeatherCity.City);
        }

        private void SettingsImage_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            ClickSettingsBtn?.Invoke();
        }

        private void LoveImage_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            ClickLoveBtn?.Invoke();
        }
    }
}
