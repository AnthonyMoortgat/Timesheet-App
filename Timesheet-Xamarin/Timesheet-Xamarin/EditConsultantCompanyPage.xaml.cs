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
	public partial class EditConsultantCompanyPage : ContentPage
	{
        private int userId;
        private CompanyServices companyServices = new CompanyServices();
        private string idCompany = Application.Current.Properties["IdCompany"].ToString();

        public EditConsultantCompanyPage(int userid)
        {
            InitializeComponent();
            userId = userid;
        }

        private void BackToConsultantList_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CompanyUserListPage();
        }

        private async void RemoveConsultantFromCompany_Clicked(object sender, EventArgs e)
        {
            bool b = await companyServices.DeleteUserFromCompanyByIdAsync(int.Parse(idCompany), userId);
            if (b == true)
            {
                await DisplayAlert("Warning", $"The consultant is removed from the company.", "Ok");
            }
            else
            {
                await DisplayAlert("Warning", $"Could not remove the consultant from the company.", "Ok");
            }
            Application.Current.MainPage = new CompanyUserListPage();
        }
    }
}