using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class CompanyUsers : ContentPage
	{
        ObservableCollection<UserDto> userCollection = new ObservableCollection<UserDto>();
        public ObservableCollection<UserDto> UserCollection { get { return userCollection; } }
        CompanyServices companyServices = new CompanyServices();
        string idCompany = Application.Current.Properties["IdCompany"].ToString();
        
        public CompanyUsers()
		{
			InitializeComponent();
            Start();
            CompanyUserList.ItemsSource = UserCollection;
        }
        private async void Start()
        {
            List<UserDto> tempUsers = await companyServices.GetUsersFromCompanyByIdAsync(int.Parse(idCompany));
            foreach (var user in tempUsers)
            {
                userCollection.Add(user);
            }
        }
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (UserDto)e.SelectedItem;
                CompanyUserList.SelectedItem = null;
                Application.Current.MainPage = (new EditUserPage(item));
            }
        }
        private void GoBack(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CompanyOptionPage();
        }
    }
}