using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeather.Class
{
    public class WeatherCity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string city = "";
        private bool isFavourites;
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City)); } }

        string favourites = "Добавить в избранное";
        public string Favourites { get => favourites; set { favourites = value; OnPropertyChanged(nameof(Favourites)); } }
        public bool IsFavourites { get => isFavourites; set { isFavourites = value; Favourites = !value ? "Добавить в избранное" : "Удалить из избранного"; OnPropertyChanged(nameof(IsFavourites)); } }
       

        //---------------
        
        string[] _times;
        Bitmap[] _icons;
        public string[] Times { get => _times; set { _times = value; OnPropertyChanged(nameof(Times)); } }
        public Bitmap[] Icons { get => _icons; set { _icons = value; OnPropertyChanged(nameof(Icons)); } }

        string[] _days;
        string[] _months;
        string[] _weeks;
        string[] _uFIndex;
        string[] _sunrises;
        string[] _sunsets;
        string[] _temps;


        public string[] Days { get => _days; set { _days = value; OnPropertyChanged(nameof(Days)); } }
        public string[] Months { get => _months; set { _months = value; OnPropertyChanged(nameof(Months)); } }
        public string[] Weeks { get => _weeks; set { _weeks = value; OnPropertyChanged(nameof(Weeks)); } }
        public string[] UFIndex { get => _uFIndex; set { _uFIndex = value; OnPropertyChanged(nameof(UFIndex)); } }
        public string[] Sunrises { get => _sunrises; set { _sunrises = value; OnPropertyChanged(nameof(Sunrises)); } }
        public string[] Sunsets { get => _sunsets; set { _sunsets = value; OnPropertyChanged(nameof(Sunsets)); } }
        public string[] Temps { get => _temps; set { _temps = value; OnPropertyChanged(nameof(Temps)); } }

        string[] _morningSpeeds;
        string[] _daySpeeds;
        string[] _eveningSpeeds;
        string[] _nightSpeeds;
        string[] _morningHumidities;
        string[] _dayHumidities;
        string[] _eveningHumidities;
        string[] _nightHumidities;
        string[] _morningPressures;
        string[] _dayPressures;
        string[] _eveningPressures;
        string[] _nightPressures;
        string[] _condition;
        string[] _maxTemps;
        string[] _minTemps;
        Bitmap[] _dayicons;
        string _speedText;
        string _pressureText;
        string _humidityText;
        string _tempText;

        public string[] MorningSpeeds { get => _morningSpeeds; set { _morningSpeeds = value; OnPropertyChanged(nameof(MorningSpeeds)); } }
        public string[] DaySpeeds { get => _daySpeeds; set { _daySpeeds = value; OnPropertyChanged(nameof(DaySpeeds)); } }
        public string[] EveningSpeeds { get => _eveningSpeeds; set { _eveningSpeeds = value; OnPropertyChanged(nameof(EveningSpeeds)); } }
        public string[] NightSpeeds { get => _nightSpeeds; set { _nightSpeeds = value; OnPropertyChanged(nameof(NightSpeeds)); } }
        public string[] MorningHumidities { get => _morningHumidities; set { _morningHumidities = value; OnPropertyChanged(nameof(MorningHumidities)); } }
        public string[] DayHumidities { get => _dayHumidities; set { _dayHumidities = value; OnPropertyChanged(nameof(DayHumidities)); } }
        public string[] EveningHumidities { get => _eveningHumidities; set { _eveningHumidities = value; OnPropertyChanged(nameof(EveningHumidities)); } }
        public string[] NightHumidities { get => _nightHumidities; set { _nightHumidities = value; OnPropertyChanged(nameof(NightHumidities)); } }
        public string[] MorningPressures { get => _morningPressures; set { _morningPressures = value; OnPropertyChanged(nameof(MorningPressures)); } }
        public string[] DayPressures { get => _dayPressures; set { _dayPressures = value; OnPropertyChanged(nameof(DayPressures)); } }
        public string[] EveningPressures { get => _eveningPressures; set { _eveningPressures = value; OnPropertyChanged(nameof(EveningPressures)); } }
        public string[] NightPressures { get => _nightPressures; set { _nightPressures = value; OnPropertyChanged(nameof(NightPressures)); } }
        public string[] Condition { get => _condition; set { _condition = value; OnPropertyChanged(nameof(Condition)); } }
        public string[] MaxTemps { get => _maxTemps; set { _maxTemps = value; OnPropertyChanged(nameof(MaxTemps)); } }
        public string[] MinTemps { get => _minTemps; set { _minTemps = value; OnPropertyChanged(nameof(MinTemps)); } }
        public Bitmap[] Dayicons { get => _dayicons; set { _dayicons = value; OnPropertyChanged(nameof(Dayicons)); } }
        public string SpeedText { get => _speedText; set { _speedText = value; OnPropertyChanged(nameof(SpeedText)); } }
        public string HumidityText { get => _humidityText; set { _humidityText = value; OnPropertyChanged(nameof(HumidityText)); } }
        public string PressureText { get => _pressureText; set { _pressureText = value; OnPropertyChanged(nameof(PressureText)); } }
        public string TempText { get => _tempText; set { _tempText = value; OnPropertyChanged(nameof(TempText)); } }

        public WeatherCity() 
        {
            City = "";
            IsFavourites = false;
            SpeedText = "";
            HumidityText = "";
            PressureText = "";
            TempText = "";

            Times = new string[8];
            Icons = new Bitmap[8];
            Days = new string[7];
            Months = new string[7];
            Weeks = new string[7];
            UFIndex = new string[7];
            Sunrises = new string[7];
            Sunsets = new string[7];
            Temps = new string[7];

            MorningSpeeds = new string[7];
            DaySpeeds = new string[7];
            EveningSpeeds = new string[7];
            NightSpeeds = new string[7];
            MorningHumidities = new string[7];
            DayHumidities = new string[7];
            EveningHumidities = new string[7];
            NightHumidities = new string[7];
            MorningPressures = new string[7];
            DayPressures = new string[7];
            EveningPressures = new string[7];
            NightPressures = new string[7];
            Condition = new string[7];
            MaxTemps = new string[7];
            MinTemps = new string[7];
            Dayicons = new Bitmap[7];
        }

        public WeatherCity(string city, bool isFavourites) : this()
        {
            City = city;
            IsFavourites = isFavourites;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
