using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Timesheet_Xamarin
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<string> Logs = null;

        public MainPage()
        {
            InitializeComponent();
            StartTime.Time = new TimeSpan(8, 0, 0);
            EndTime.Time = new TimeSpan(16, 0, 0);
        }

        bool CheckTimePicker()
        {
            string StartHour = StartTime.Time.Hours.ToString();
            string StartMin = StartTime.Time.Minutes.ToString();
            string StartT = $"{StartHour}:{StartMin}";

            string EndHour = EndTime.Time.Hours.ToString();
            string EndMin = EndTime.Time.Minutes.ToString();
            string EndT = $"{EndHour}:{EndMin}";

            if (CheckTime(DateTime.Parse(StartT), DateTime.Parse(EndT)) == false)
            {
                DisplayAlert("Warning", $"The End time needs to past the Start time!", "Ok");
                return false;
            }
            else
            {
                return true;
            }
        }

        bool CheckDescription()
        {
            if (DescriptionEntry.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a description!", "Ok");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LogTime_Clicked(object sender, EventArgs e)
        {
            string StartHour = StartTime.Time.Hours.ToString();
            string StartMin = StartTime.Time.Minutes.ToString();
            string StartT = $"{StartHour}:{StartMin}";

            string EndHour = EndTime.Time.Hours.ToString();
            string EndMin = EndTime.Time.Minutes.ToString();
            string EndT = $"{EndHour}:{EndMin}";

            /*
            if (CheckTimePicker() == true && CheckDescription() == true)
            {
                LogList.Text += $"\n{DateTime.Parse(StartT).ToString("dd/MM/yyyy")} - {DateTime.Parse(StartT).ToString("HH:mm")} - {DateTime.Parse(EndT).ToString("HH:mm")}: {DescriptionEntry.Text}, Total: {DateTime.Parse(EndT) - DateTime.Parse(StartT)}";
            }
            */

            if (CheckTimePicker() == true && CheckDescription() == true)
            {
                if (Logs == null)
                {
                    Logs = new ObservableCollection<string>();
                    Logs.Add($"{DateTime.Parse(StartT).ToString("dd/MM/yyyy")} - {DateTime.Parse(StartT).ToString("HH:mm")} - {DateTime.Parse(EndT).ToString("HH:mm")}: {DescriptionEntry.Text}, Total: {DateTime.Parse(EndT) - DateTime.Parse(StartT)}");
                    LogList1.ItemsSource = Logs;
                }
                else
                {
                    Logs.Add($"{DateTime.Parse(StartT).ToString("dd/MM/yyyy")} | {DateTime.Parse(StartT).ToString("HH:mm")} - {DateTime.Parse(EndT).ToString("HH:mm")}: {DescriptionEntry.Text}, Total: {DateTime.Parse(EndT) - DateTime.Parse(StartT)}");
                    LogList1.ItemsSource = Logs;
                }
            }
        }

        private static bool CheckTime(DateTime StartTime, DateTime EndTime)
        {
            if (StartTime >= EndTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
