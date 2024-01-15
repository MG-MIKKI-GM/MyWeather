using Avalonia.Controls;
using Avalonia.Media;
using MyWeather.Class;
using System;
using System.Linq;
using System.Threading;

namespace MyWeather.Views
{
    public partial class SettingsPage : UserControl
    {
        public event Action ClickBackBtn;

        public SettingsPage()
        {
            InitializeComponent();
            BackBtn.PointerPressed += BackBtn_PointerPressed;
            TempBtn1.Click += Btn_Click;
            TempBtn2.Click += Btn_Click;

            SpeedBtn1.Click += Btn_Click;
            SpeedBtn2.Click += Btn_Click;

            PressureBtn1.Click += Btn_Click;
            PressureBtn2.Click += Btn_Click;

            UpdateBtn();
        }

        private void Btn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    if (Save.Settings == null)
                        Save.Settings = new Settings(true,true,true);
                    if (btn.Name.Last() == '2')
                    {
                        if (btn.Name.Contains("Temp"))
                            Save.Settings.Temp = false;
                        else if (btn.Name.Contains("Speed"))
                            Save.Settings.Speed = false;
                        else
                            Save.Settings.Pressure = false;
                    }
                    else
                    {
                        if (btn.Name.Contains("Temp"))
                            Save.Settings.Temp = true;
                        else if (btn.Name.Contains("Speed"))
                            Save.Settings.Speed = true;
                        else
                            Save.Settings.Pressure = true;
                    }
                    
                    Save.SaveData<Settings>(Save.Settings, Save.PathFileSettings);
                    Thread.Sleep(100);
                    Save.Settings = Save.LoadData<Settings>(Save.PathFileSettings);

                    UpdateBtn();
                }
            }
            catch
            {
            }
        }

        private void UpdateBtn()
        {
            if (Save.Settings == null) return;

            if (Save.Settings.Temp == true)
            {
                SelectBtn(TempBtn1);
                DeselectBtn(TempBtn2);
            }
            else
            {
                SelectBtn(TempBtn2);
                DeselectBtn(TempBtn1);
            }

            if (Save.Settings.Speed == true)
            {
                SelectBtn(SpeedBtn1);
                DeselectBtn(SpeedBtn2);
            }
            else
            {
                SelectBtn(SpeedBtn2);
                DeselectBtn(SpeedBtn1);
            }

            if (Save.Settings.Pressure == true)
            {
                SelectBtn(PressureBtn1);
                DeselectBtn(PressureBtn2);
            }
            else
            {
                SelectBtn(PressureBtn2);
                DeselectBtn(PressureBtn1);
            }
        }

        private void SelectBtn(Button button)
        {
            button.Background = new SolidColorBrush(Color.FromRgb(227, 227, 227));
        }

        private void DeselectBtn(Button button)
        {
            button.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void BackBtn_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            ClickBackBtn?.Invoke();
        }
    }
}
