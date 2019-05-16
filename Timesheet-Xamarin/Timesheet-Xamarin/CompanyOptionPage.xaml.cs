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
    public partial class CompanyOptionPage : ContentPage
    {
        public CompanyOptionPage()
        {
            InitializeComponent();
        }

        private void ManageProjects_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ProjectListPage();
        }

        private void ManageUsers_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new Users();  Moet nog gemaakt worden
        }

        private void ManageRoles_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new Roles();
        }
    }
}