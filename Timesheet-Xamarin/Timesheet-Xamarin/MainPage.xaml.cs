using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Log;
using Timesheet_Library.Dto.Project;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;

namespace Timesheet_Xamarin
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<string> Logs = null;
        Dictionary<int, string> projectsWithKey = new Dictionary<int, string>();

        public MainPage()
        {
            InitializeComponent();
            StartTime.Time = new TimeSpan(8, 0, 0);
            EndTime.Time = new TimeSpan(16, 0, 0); 
        }

        protected async override void OnAppearing()
        {
            ProjectServices projectServices = new ProjectServices();
            //Haal alle projecten
            List<ProjectDto> projects = await projectServices.GetAllProjectsAsync();
            //Steekt alle projecten in ProjectList (Picker)
            AddProjectsToProjectList(projects, projectsWithKey);
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
            string idUser = Application.Current.Properties["IdUser"].ToString();

            string StartHour = StartTime.Time.Hours.ToString();
            string StartMin = StartTime.Time.Minutes.ToString();
            string StartT = $"{StartHour}:{StartMin}";

            string EndHour = EndTime.Time.Hours.ToString();
            string EndMin = EndTime.Time.Minutes.ToString();
            string EndT = $"{EndHour}:{EndMin}";

            string value = ProjectList.SelectedItem.ToString();
            int id = 1;

            foreach (var project in projectsWithKey)
            {
                if (project.Value == value)
                {
                    id = project.Key;
                }
            }

            LogToCreateDto log = new LogToCreateDto()
            {
                UserID = int.Parse(idUser),
                ProjectID = id,
                StartTime = DateTime.Parse(StartT),
                StopTime = DateTime.Parse(EndT),
                Description = DescriptionEntry.Text
            };

            if (CheckTimePicker() == true && CheckDescription() == true)
            {
                if (Logs == null)
                {
                    Logs = new ObservableCollection<string>();
                    //LOG TOEVOEGEN AAN API CALL
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

        private void AddProjectsToProjectList(List<ProjectDto> projects, Dictionary<int, string> projectsWithKey)
        {
            foreach (var project in projects)
            {
                projectsWithKey.Add(project.ID, project.Name);
            }

            foreach (var project in projectsWithKey)
            {
                ProjectList.Items.Add(project.Value);
            }
            ProjectList.SelectedIndex = 0;
        }
    }
}
