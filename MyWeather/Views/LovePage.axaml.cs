using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MyWeather.Class;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWeather.Views
{
    public partial class LovePage : UserControl
    {
        public event Action ClickBackBtn;
        public event Action<WeatherContent, string> SelectNode;
        public event Action LoadedData;

        public LovePage()
        {
            InitializeComponent();

            InitNode();

            BackBtn.PointerPressed += BackBtn_PointerPressed;
        }

        private async void InitNode()
        {
            if (Save.Loves == null || Save.Settings == null) return;
            Save.Loves = Save.LoadData<List<Love>>(Save.PathFileLove);
            await Task.Delay(1000);
            foreach (Love love in Save.Loves)
            {
                EmptyListText.IsVisible = false;
                WeatherContent content = await WeatherAPI.GetWeather(love.Lat, love.Lon);
                string speed = Save.Settings.Speed ? content.fact.wind_speed.ToString() + "ì/ñ" : (string.Format("{0:f0}", content.fact.wind_speed * 3.6) + "êì/÷");
                string temp = Save.Settings.Temp ? content.fact.temp.ToString() : string.Format("{0:f1}", content.fact.temp * 1.8 + 32);
                string tempText = Save.Settings.Temp ? "C°" : "°F";
                Bitmap icon = WeatherContent.GetIconWeather(content.fact.condition, content.fact.daytime);

                NodePanel.Children.Add(CreateNode(speed, love.Name, temp, tempText, icon, content));
            }
            LoadedData?.Invoke();
        }

        private Border CreateNode(string speed, string city, string temp, string tempText, Bitmap icon, WeatherContent content = null)
        {
            Border border = new Border()
            {
                DataContext = content,
                Margin = new Avalonia.Thickness(0,10),
                Background = Brushes.White,
                Name = city,
                Height = 100,
                CornerRadius = new Avalonia.CornerRadius(15),
                BoxShadow = new Avalonia.Media.BoxShadows(new Avalonia.Media.BoxShadow() { Blur = 4, IsInset = false, OffsetX = 0, OffsetY = 4, Color = Color.FromArgb(64,0,0,0) })
            };
            border.PointerPressed += Border_PointerPressed;

            Grid grid = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitions("1.5* * *"),
                Margin = new Avalonia.Thickness(30,0),
            };
            border.Child = grid;

            StackPanel stackPanelCity = new StackPanel()
            {
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            };
            Grid.SetColumn(stackPanelCity, 0);
            grid.Children.Add(stackPanelCity);

            TextBlock textBlockSpeed = new TextBlock()
            {
                FontSize = 12,
                Margin = new Avalonia.Thickness(0,0,0,2),
                Text = speed,
            };
            TextBlock textBlockCity = new TextBlock()
            {
                Text = city,
                Foreground = Brushes.Black,
                FontWeight = FontWeight.Medium
            };
            stackPanelCity.Children.Add(textBlockSpeed);
            stackPanelCity.Children.Add(textBlockCity);

            Image image = new Image()
            {
                Margin = new Avalonia.Thickness(0,30),
                Source = icon,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            };
            Grid.SetColumn(image, 1);
            grid.Children.Add(image);

            StackPanel stackPanelTemp = new StackPanel()
            {
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Orientation = Avalonia.Layout.Orientation.Horizontal
            };
            Grid.SetColumn(stackPanelTemp, 2);
            grid.Children.Add(stackPanelTemp);

            TextBlock textBlockTemp = new TextBlock()
            {
                Foreground = Brushes.Black,
                Text = temp,
                FontWeight = FontWeight.Medium,
            };
            TextBlock textBlockTempText = new TextBlock()
            {
                Foreground = Brushes.Black,
                Text = tempText,
                FontWeight = FontWeight.Medium,
            };
            stackPanelTemp.Children.Add(textBlockTemp);
            stackPanelTemp.Children.Add(textBlockTempText);

            return border;
        }

        private void Border_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if (sender is Border b)
            {
                SelectNode?.Invoke(b.DataContext as WeatherContent, b.Name);
            }
        }

        private void BackBtn_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            ClickBackBtn?.Invoke();
        }
    }
}
