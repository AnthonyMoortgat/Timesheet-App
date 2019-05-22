using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto;
using Timesheet_Library.Dto.Log;
using Timesheet_Library.Dto.Project;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;

namespace Timesheet_Xamarin
{
    public partial class MainPage : CarouselPage
    {
        ObservableCollection<LogDto> logsCollection = new ObservableCollection<LogDto>();
        public ObservableCollection<LogDto> LogsCollection { get { return logsCollection; } }
        private LogServices logServices = new LogServices();
        private UserServices userServices = new UserServices();
        private ProjectServices projectServices = new ProjectServices();
        private string idUser = Application.Current.Properties["IdUser"].ToString();


        //Logs met Key
        private Dictionary<int, string> logsWithKey = new Dictionary<int, string>();
        //Projecten met Key
        private Dictionary<int, string> projectsWithKey = new Dictionary<int, string>();

        List<ProjectDto> projects;
        Dictionary<string, int> projectByName;
        List<CompanyDto> companies;
        Dictionary<string, int> companyByName;

        public MainPage()
        {
            InitializeComponent();
            Start();
            StartTime.Time = new TimeSpan(8, 0, 0);
            EndTime.Time = new TimeSpan(16, 0, 0);
        }
        private async void Start()
        {
            //Haalt alle projecten, companie en logs op
            try
            {
                projects = await userServices.GetAllUserProjectsAsync(int.Parse(idUser));
                companies = await userServices.GetAllUserCompaniesAsync(int.Parse(idUser));
                List<LogDto> tempLogs = await userServices.GetAllUserLogsAsync(int.Parse(idUser));
                foreach (var log in tempLogs)
                {
                    logsCollection.Add(log);                    
                }

                //Steekt alle projecten in ProjectList (Picker)
                AddProjectsToProjectList();
                //Genereer buttons voor elk project
                InitializeProjects();
                //Genereer buttons voor elke company
                InitializeCompanies();
                //steek alle logs in scrollList
                AddLogsToLogList();
            }
            catch(Exception)
            {
            }                                 
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (LogDto)e.SelectedItem;
                LogList1.SelectedItem = null;
                Application.Current.Properties["IdProject"] = item.ProjectID;
                ProjectDto tempProject = await projectServices.GetProjectByIdAsync(item.ProjectID);
                
                Application.Current.Properties["NameProject"] = tempProject.Name;
                await Navigation.PushModalAsync(new MainPage2(item.ID));
            }
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
        private void AddLogsToLogList()
        {
            LogList1.ItemsSource = LogsCollection;
        }

        //Projecten toevoegen aan ProjectList (Picker)
        private void AddProjectsToProjectList()
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

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["IdUser"] = "";
            Application.Current.Properties["Token"] = "";

            Application.Current.MainPage = new Login();
        }

        private void InitializeProjects()
        {
            bool emptyList = true;
            projectByName = new Dictionary<string, int>();

            foreach (ProjectDto project in projects)
            {
                if (project.Name != "No project")
                {
                    var button = new Button() { Text = project.Name };
                    button.Clicked += ToProjectInfo;
                    projectByName.Add(project.Name, project.ID);
                    ProjectOverview.Children.Add(button);
                    emptyList = false;
                }
                else
                {
                    var button = new Button()
                    {
                        Text = project.Name,
                        Scale = 0.5
                    };
                    button.Clicked += ToProjectInfo;
                    projectByName.Add(project.Name, project.ID);
                    ProjectOverview.Children.Add(button);
                    emptyList = false;
                }
            }
            if (emptyList)
            {                             
                var label = new Label()
                {
                    Text = "No projects yet.",
                    FontSize =  10
                };
                ProjectOverview.Children.Add(label);               
            }
        }
        private void InitializeCompanies()
        {
            bool emptyList = true;
            companyByName = new Dictionary<string, int>();
            foreach (CompanyDto company in companies)
            {
                if (company.Name != "DefaultCompany")
                {
                    var button = new Button() { Text = company.Name };
                    button.Clicked += ToCompanyOptions;
                    companyByName.Add(company.Name, company.ID);
                    CompanyOverview.Children.Add(button);
                    emptyList = false;
                }
            }
            if (emptyList)
            {
                var label = new Label()
                {
                    Text = "No company yet.",
                    FontSize = 10
                };
                CompanyOverview.Children.Add(label);
            }
        }

        private async void ToProjectInfo(object sender, EventArgs args)
        {
            string name = ((Button)sender).Text;
            var id = projectByName[name];
            Application.Current.Properties["NameProject"] = name;

            await Navigation.PushModalAsync(new ProjectInfo(id), true);
        }
        private async void ToCompanyOptions(object sender, EventArgs args)
        {
            string name = ((Button)sender).Text;
            var id = companyByName[name];
            Application.Current.Properties["IdCompany"] = id;
            Application.Current.Properties["NameCompany"] = name;

            await Navigation.PushModalAsync(new CompanyOptionPage(), true);
        }
    }
}

// Stackoverflow. How to set value to picker in xamarin forms?
// https://stackoverflow.com/questions/51272706/how-to-set-value-to-picker-in-xamarin-forms
// Geraadpleegd op 4 mei 2019

// Formum xamarin. How to bind value as id and text as string of the list in Picker control in xamarin forms?
// https://forums.xamarin.com/discussion/87760/how-to-bind-value-as-id-and-text-as-string-of-the-list-in-picker-control-in-xamarin-forms
// Geraadpleegd op 4 mei 2019

// Mobile Wits. How to pass data in pages using xamarin forms
// http://www.mobilewits.com/blog/how-to-pass-data-in-pages-using-xamarin-forms/
// Geraadpleegd op 7 mei 2019

// Stackoverflow. How do you switch pages in Xamarin Forms
// https://stackoverflow.com/questions/25165106/how-do-you-switch-pages-in-xamarin-forms
// Geraadpleegd op 7 mei 2019

// Stackoverflow. Xamarin forms listview itemselected functionality
// https://stackoverflow.com/questions/33376462/xamarin-forms-listview-itemselected-functionality
// Geraadpleegd op 7 mei 2019