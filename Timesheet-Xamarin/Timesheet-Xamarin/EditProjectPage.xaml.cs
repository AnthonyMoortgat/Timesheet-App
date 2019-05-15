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

        private int projectId = 0;
        private ProjectServices projectServices = new ProjectServices();
        public List<ProjectDto> projects = new List<ProjectDto>();
        private ProjectDto project;

        public EditProjectPage(int projectID)
        {
            InitializeComponent();
            projectId = projectID;
            DisplayAlert("Warning", projectId.ToString(), "Ok");
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddProjectPage());
        }

        //Delete role
        private async void DeleteRoleButton_Clicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("Warning", "Do you want to delete this project?", "Yes", "No");

            if (action == true)
            {
                bool deleted = await projectServices.GetProjectByIdAsync(2);
                if (deleted == true)
                {
                    Application.Current.MainPage = new ProjectServices().DeleteProjectByIdAsync(1);
                }
            }
        }

        //Check of de description en de nama leeg is
        private bool CheckDescriptionAndName()
        {
            if (EntryDescription.Text == "" && AddConsultantToProjectButton.Text == "")
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
                    DisplayAlert("Warning", "Name of the Role already exists!", "Ok");
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
                if (project.Name == AddConsultantToProjectButton.Text)
                {
                    if (project.Name == AddConsultantToProjectButton.Text)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return true;
        }
    }
}