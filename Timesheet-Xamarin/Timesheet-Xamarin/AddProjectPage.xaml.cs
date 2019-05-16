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
	public partial class AddProjectPage : ContentPage
	{
        private ProjectServices projectServices = new ProjectServices();
        private List<ProjectDto> projects = new List<ProjectDto>();
        //Company ID

		public AddProjectPage ()
		{
			InitializeComponent();
		}

        //Alle Companies ophalen
        protected async override void OnAppearing()
        {
            projects = await projectServices.GetAllProjectsAsync(); //companyRoles worden in deze array gezet
        }

        private async void ClickAddProjectAsync(object sender, EventArgs args)
        {
            if (CheckDescriptionAndName() == true)
            {
                await DisplayAlert("Warning", $"Correct!", "Ok");

                /*
                ProjectToCreateDto project = new ProjectToCreateDto
                {
                    CompanyID = 1, //Company ID 
                    Description = EntryDescription.Text,
                    Name = EntryNameProject.Text
                };
                ProjectDto projectDto = await projectServices.CreateProjectAsync(project);*/
            }
        }

        private bool CheckDescriptionAndName()
        {
            if (EntryDescription.Text == "" && EntryNameProject.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a name and description!", "Ok");
                return false;
            }
            else if (EntryNameProject.Text == "")
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
                    DisplayAlert("Warning", "Name of the project already exists!", "Ok");
                    return false;
                }
                return true;
            }
        }

        //Kijken of het project al bestaat
        private bool CheckProjectExists()
        {
            foreach (var project in projects)
            {
                if (project.Name == EntryNameProject.Text)
                {
                    return false;
                }
            }
            return true;
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ProjectListPage();
        }
    }
}