using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (/*User.CheckLogin() == */a == true)
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