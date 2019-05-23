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
	public partial class AddConsultantPage : ContentPage
	{
        private CompanyServices companyServices = new CompanyServices();
        private ProjectServices projectServices = new ProjectServices();
        private string projectId = Application.Current.Properties["projectid"].ToString();

        public AddConsultantPage()
        {
            InitializeComponent();
        }

        private async void AddConsultant_Clicked(object sender, EventArgs e)
        {
            if (Email.Text == "")
            {
                await DisplayAlert("Warning", "You have to fill in an email.", "Ok");
            }
            else
            {
                bool b = await projectServices.AddUserToProjectAsync(int.Parse(projectId), Email.Text);
                if (b == true)
                {
                    await DisplayAlert("Warning", "Email is send to consultant.", "Ok");
                    Application.Current.MainPage = new EditProjectPage();
                }
                else
                {
                    await DisplayAlert("Warning", "Email is not send because the consultant doesn't exist or is already added to this project.", "Ok");
                }
            }
        }

        private void BackToEditProject_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new EditProjectPage();
        }
    }
}