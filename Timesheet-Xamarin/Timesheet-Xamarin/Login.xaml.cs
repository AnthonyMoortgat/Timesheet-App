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
            UserToLoginDto user = new UserToLoginDto();
            user.Email = email.Text;
            user.Password = password.Text;

            string x = await SessionServices.CreateSessionAsync(user);

            if (x != "")
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                error.IsVisible = true;
            }
        }


        private void GoToRegisterPage(object sender, EventArgs args)
        {
            Application.Current.MainPage = new Register();
        }
    }
}