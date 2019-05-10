using System;
using System.Collections.Generic;
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
	public partial class AddProjectPage : ContentPage
	{
        ProjectServices projectServices = new ProjectServices();
		public AddProjectPage ()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {

        }

        private async void ClickAddProjectAsync(object sender, EventArgs args)
        {
            if(EntryNameProject.Text == "" && EntryDescription.Text == "")
            {
                await DisplayAlert("Warning", $"Some fields are not filled in!", "Ok");
            }
            else
            {
                ProjectToCreateDto project = new ProjectToCreateDto
                {
                    CompanyID = 1,
                    Description = EntryDescription.Text,
                    Name = EntryNameProject.Text
                };
                ProjectDto projectDto = await projectServices.CreateProjectAsync(project);
            }
        }
    }
}