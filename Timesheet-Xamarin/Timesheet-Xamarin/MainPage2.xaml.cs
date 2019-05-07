using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Project;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage2 : ContentPage
    {
        LogServices logServices = new LogServices();
        UserServices userServices = new UserServices();
        ProjectServices projectServices = new ProjectServices();

        //Projecten met Key
        Dictionary<int, string> projectsWithKey = new Dictionary<int, string>();
        private int logId;

        public MainPage2(int id, string log)
        {
            InitializeComponent();
            StartTime.Time = new TimeSpan(8, 0, 0);
            EndTime.Time = new TimeSpan(16, 0, 0);

            LogToEdit.Text = $"{log} :: {id}";
            logId = id;
        }
        
        protected async override void OnAppearing()
        {
            //Haalt alle projecten op
            List<ProjectDto> projects = await projectServices.GetAllProjectsAsync();

            //Steekt alle projecten in ProjectList (Picker)
            AddProjectsToProjectList(projects, projectsWithKey);
        }

        public async void ClickCancel(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new MainPage());
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