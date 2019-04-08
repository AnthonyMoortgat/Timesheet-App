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

        private void GoToHomePage(object sender, EventArgs args)
        {
            bool a = false;

            UserToLoginDto user = new UserToLoginDto();
            user.Email = "michael@gmail.com";
            user.Password = "hallo1";

            if (SessionServices.CreateSession(user) != "")
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