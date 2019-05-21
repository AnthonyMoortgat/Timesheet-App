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
        string idCompany = Application.Current.Properties["IdCompany"].ToString();
        public CompanyOptionPage()
        {
            InitializeComponent();
            CompanyName.Text = idCompany;
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
            Application.Current.MainPage = new Roles();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage();
            page.CurrentPage = page.Children[1];
            Application.Current.MainPage = page;
        }
    }
}