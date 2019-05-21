using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditConsultantPage : ContentPage
	{
        private int userId;
        private ProjectServices projectServices = new ProjectServices();
        private string projectId = Application.Current.Properties["projectid"].ToString();

        public EditConsultantPage(int userid)
        {
            InitializeComponent();
            userId = userid;
        }

        private void BackToConsultantList_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ConsultantListProjectPage();
        }

        private async void RemoveConsultantFromProject_Clicked(object sender, EventArgs e)
        {
            bool b = await projectServices.RemoveUserToProjectAsync(int.Parse(projectId), userId);
            if (b == true)
            {
                await DisplayAlert("Warning", $"The consultant is removed from the project.", "Ok");
            }
            else
            {
                await DisplayAlert("Warning", $"Could not remove the consultant from the project.", "Ok");
            }
            Application.Current.MainPage = new ConsultantListProjectPage();
        }
    }
}