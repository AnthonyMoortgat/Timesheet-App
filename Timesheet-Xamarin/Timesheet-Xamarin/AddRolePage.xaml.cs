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
	public partial class AddRolePage : ContentPage
	{
		public AddRolePage ()
		{
			InitializeComponent ();
		}

        private void BackToRoles(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Roles();
        }
    }
}