using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto;
using Timesheet_Library.Dto.Project;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProjectPage : ContentPage
	{
        private string projectId = Application.Current.Properties["projectid"].ToString();
        private ProjectServices projectServices = new ProjectServices();
        private ProjectDto projectById = new ProjectDto();
        private List<ProjectDto> projects = new List<ProjectDto>();
        private string projectSave = "";

        public EditProjectPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            //Haalt alle projecten op
            projectById = await projectServices.GetProjectByIdAsync(int.Parse(projectId));
            projects = await projectServices.GetAllProjectsAsync(); //Alle projects van de company moet dit worden
            EntryName.Text = projectById.Name;
            EntryDescription.Text = projectById.Description;
            projectSave = EntryName.Text;
        }

        private void EditProjectButton_Clicked(object sender, EventArgs e)
        {
            if (CheckDescriptionAndName() == true)
            {
                DisplayAlert("Hallo", "Correct!", "Ok");
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ProjectListPage();
        }

        //Delete role
        private async void DeleteProjectButton_Clicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("Warning", "Do you want to delete this project?", "Yes", "No");

            if (action == true)
            {
                bool deleted = await projectServices.DeleteProjectByIdAsync(1);
                if (deleted == true)
                {
                    Application.Current.MainPage = new ProjectListPage();
                }
            }
        }

        //Check of de description en de name leeg is
        private bool CheckDescriptionAndName()
        {
            if (EntryDescription.Text == "" && EntryName.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a name and description!", "Ok");
                return false;
            }
            else if (AddConsultantToProjectButton.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a name!", "Ok");
                return false;
            }
            else if (EntryDescription.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a description!", "Ok");
                return false;
            }
            else
            {
                if (CheckProjectExists() == false)
                {
                    DisplayAlert("Warning", "Name of the Project already exists!", "Ok");
                    return false;
                }
                return true;
            }
        }

        //Kijken of de role bestaat
        private bool CheckProjectExists()
        {
            foreach (var project in projects)
            {
                if (project.Name == EntryName.Text)
                {
                    if (project.Name == projectSave)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return true;
        }

        private void AddConsultantToProjectButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AddConsultantPage();
        }

        private void ConsultantListProjectButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ConsultantListProjectPage();
        }
    }
}