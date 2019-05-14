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
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage2 : ContentPage
    {
        private LogServices logServices = new LogServices();
        private UserServices userServices = new UserServices();
        private ProjectServices projectServices = new ProjectServices();

        private LogDto log = new LogDto();
        private DateTime date;

        //Projecten met Key
        private Dictionary<int, string> projectsWithKey = new Dictionary<int, string>();
        private int logId = 0;

        public MainPage2(int id)
        {
            InitializeComponent();
            logId = id;
        }
        
        protected async override void OnAppearing()
        {
            //Haalt alle projecten op
            List<ProjectDto> projects = await projectServices.GetAllProjectsAsync();
            ProjectDto projectDto = null;

            //Steekt alle projecten in ProjectList (Picker)
            AddProjectsToProjectList(projects, projectsWithKey);

            if (logId != 0) {
                log = await logServices.GetLogByIdAsync(logId);
                StartTime.Time = new TimeSpan(log.StartTime.Hour, log.StopTime.Minute, 0);
                EndTime.Time = new TimeSpan(log.StopTime.Hour, log.StopTime.Minute, 0);

                projectDto = await projectServices.GetProjectByIdAsync(log.ProjectID);
                LabelProject.Text = $"Project [Before: {projectDto.Name}]";

                DescriptionEntry.Text = log.Description;

                date = log.StartTime.Date; ;
            }
        }

        public async void ClickCancel(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

        private async void ClickEdit(object sender, EventArgs e)
        {
            //Startuur
            string StartHour = StartTime.Time.Hours.ToString();
            string StartMin = StartTime.Time.Minutes.ToString();
            string StartT = $"{StartHour}:{StartMin}";
            DateTime start = new DateTime(date.Year, date.Month, date.Day, int.Parse(StartHour), int.Parse(StartMin), 0);

            //Einduur
            string EndHour = EndTime.Time.Hours.ToString();
            string EndMin = EndTime.Time.Minutes.ToString();
            string EndT = $"{EndHour}:{EndMin}";
            DateTime end = new DateTime(date.Year, date.Month, date.Day, int.Parse(EndHour), int.Parse(EndMin), 0);


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
                LogToUpdateDto log = new LogToUpdateDto()
                {
                    ProjectID = projectid,
                    StartTime = start,
                    StopTime = end,
                    Description = DescriptionEntry.Text
                };

                LogDto logDto = await logServices.UpdateLogByIdAsync(log, logId);
                Application.Current.MainPage = new MainPage();
            }
        }

        public async void ClickDelete(object sender, EventArgs args)
        {
            bool action = await DisplayAlert("Warning", "Do you want to delete this log?", "Yes", "No");

            if (action == true)
            {
                bool deleted = await logServices.DeleteLogByIdAsync(logId);
                if (deleted == true)
                {
                    Application.Current.MainPage = new MainPage();
                }  
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
    }
}

// Stackoverflow. How to set value to picker in xamarin forms?
// https://stackoverflow.com/questions/51272706/how-to-set-value-to-picker-in-xamarin-forms
// Geraadpleegd op 4 mei 2019

// Formum xamarin. How to bind value as id and text as string of the list in Picker control in xamarin forms?
// https://forums.xamarin.com/discussion/87760/how-to-bind-value-as-id-and-text-as-string-of-the-list-in-picker-control-in-xamarin-forms
// Geraadpleegd op 4 mei 2019