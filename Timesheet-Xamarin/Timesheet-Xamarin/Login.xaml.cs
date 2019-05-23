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

            register.IsEnabled = false;
            login.IsEnabled = false;
            login.Text = "Logging in...";
            string token = await sessionServices.CreateSessionAsync(user);

            if (token != "")
            {
                Application.Current.Properties["IdUser"] = u[0].ID;
                Application.Current.Properties["Token"] = token;
                await Navigation.PushModalAsync(new MainPage(), true);
            }
            else
            { 
                error.IsVisible = true;
                register.IsEnabled = true;
                login.IsEnabled = true;
                login.Text = "Login";
            }
        }

        private async void GoToRegisterPage(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new Register(), true);
        }
    }
}