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
	public partial class AddConsultantPage : ContentPage
	{
		public AddConsultantPage ()
		{
			InitializeComponent ();
		}

        private void AddConsultant_Clicked(object sender, EventArgs e)
        {
            //Checken of deze consultant bestaat en deze dan adden aan het project
        }

        private void BackToEditProject_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new EditProjectPage(1); //ID nakijken
        }
    }
}