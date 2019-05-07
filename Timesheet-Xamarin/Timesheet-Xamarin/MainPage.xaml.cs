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
        LogServices logServices = new LogServices();
        UserServices userServices = new UserServices();
        ProjectServices projectServices = new ProjectServices();
        string idUser = Application.Current.Properties["IdUser"].ToString();

        public ObservableCollection<string> LogsCollection = null;

        //Logs met Key
        Dictionary<int, string> logsWithKey = new Dictionary<int, string>();
        //Projecten met Key
        Dictionary<int, string> projectsWithKey = new Dictionary<int, string>();

        public MainPage()
        {
            InitializeComponent();
            StartTime.Time = new TimeSpan(8, 0, 0);
            EndTime.Time = new TimeSpan(16, 0, 0); 
        }

        protected async override void OnAppearing()
        {
            //Haalt alle projecten op
            List<ProjectDto> projects = await projectServices.GetAllProjectsAsync();
            List<LogDto> logs = await userServices.GetAllUserLogsAsync(int.Parse(idUser));
            
            //Steekt alle projecten in ProjectList (Picker)
            AddProjectsToProjectList(projects, projectsWithKey);
            AddLogsToLogList(LogsCollection, logs);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            string logToEdit = LogList1.SelectedItem.ToString();
            int logid = 0;

            //Zoekt id van de log
            foreach (var l in logsWithKey)
            {
                if (l.Value == logToEdit)
                {
                    logid = l.Key;
                }
            }

            LogList1.SelectedItem = null;
            await Navigation.PushModalAsync(new MainPage2(logid, logToEdit));
        }

        private async void LogTime_Clicked(object sender, EventArgs e)
        {
            //Startuur
            string StartHour = StartTime.Time.Hours.ToString();
            string StartMin = StartTime.Time.Minutes.ToString();
            string StartT = $"{StartHour}:{StartMin}";

            //Einduur
            string EndHour = EndTime.Time.Hours.ToString();
            string EndMin = EndTime.Time.Minutes.ToString();
            string EndT = $"{EndHour}:{EndMin}";


            string value = ProjectList.SelectedItem.ToString();
            int projectid = 0;

            //Zoekt id van project
            foreach (var project in projectsWithKey)
            {
                if (project.Value == value)
                {
                    projectid = project.Key;
                }
            }

            //LOgs toevoegen na validatie uren en description
            if (CheckTimePicker() == true && CheckDescription() == true)
            {
                //Alle gegevens in een log object steken
                LogToCreateDto log = new LogToCreateDto()
                {
                    UserID = int.Parse(idUser),
                    ProjectID = projectid,
                    StartTime = DateTime.Parse(StartT),
                    StopTime = DateTime.Parse(EndT),
                    Description = DescriptionEntry.Text
                };
               
                LogDto logDto = await logServices.CreateLogAsync(log);
                Application.Current.MainPage = new MainPage();
            }
        }

        //Validatie start- en einduur
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

        //Validatie dat Startuur < Einduur
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

        //Check of de description leeg is
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

        //Logs toevoegen aan LogList (ListView)
        private void AddLogsToLogList(ObservableCollection<string> LogsCollection, List<LogDto> logsDto = null)
        {
            LogsCollection = new ObservableCollection<string>();
            //Logs(naam en id) in Dictionary steken
            foreach (var log in logsDto)
            {
                logsWithKey.Add(log.ID, $"{log.StartTime.ToString("dd/MM/yyyy")} | {log.StartTime.ToString("HH:mm")} - {log.StopTime.ToString("HH:mm")}: {log.Description} - Total: {log.StopTime - log.StartTime}");
            }
            
            //Dictionary in LogList(ListView) steken
            foreach (var log in logsWithKey)
            {
                LogsCollection.Add(log.Value.ToString());
            }

            //Dictionary in LogList(ListView) steken
            LogList1.ItemsSource = LogsCollection;
        }

        //Projecten toevoegen aan ProjectList (Picker)
        private void AddProjectsToProjectList(List<ProjectDto> projects, Dictionary<int, string> projectsWithKey)
        {
            //Projecten(naam en id) in Dictionary steken
            foreach (var project in projects)
            {
                projectsWithKey.Add(project.ID, project.Name);
            }

            //Dictionary in ProjectList(Picker) steken
            foreach (var project in projectsWithKey)
            {
                ProjectList.Items.Add(project.Value);
            }
            ProjectList.SelectedIndex = 0;
        }

        private void LogList1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void LogList1_Focused(object sender, FocusEventArgs e)
        {

        }
    }
}
