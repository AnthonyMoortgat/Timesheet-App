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
        ProjectServices projectServices = new ProjectServices();

		public EditConsultantPage (int userid)
		{
			InitializeComponent ();
            userId = userid;
		}

        private void BackToConsultantList_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ConsultantListProjectPage();
        }

        private void RemoveConsultantFromProject_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Hello",$"{userId}","Ok");
        }
    }
}