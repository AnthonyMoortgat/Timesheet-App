using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

        private async void GoToHomePage(object sender, EventArgs args)
        {
            SessionServices sessionServices = new SessionServices();
            UserServices userServices = new UserServices();

            UserToLoginDto user = new UserToLoginDto()
            {
                Email = email.Text,
                Password = password.Text
            };

            List<UserDto> u = await userServices.GetUserByEmailAsync(email.Text);

            Application.Current.Properties["IdUser"] = u[0].ID;

            string token = await sessionServices.CreateSessionAsync(user);

            if (token != "") //Indien login niet klopt (dus geen token)
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            { 
                Application.Current.Properties["Token"] = token;
                error.IsVisible = true;
            }
        }

        private void GoToRegisterPage(object sender, EventArgs args)
        {
            Application.Current.MainPage = new Register();
        }
    }
}