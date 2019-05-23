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
	public partial class CompanyUserListPage : ContentPage
	{
        private UserServices userServices = new UserServices();
        private ProjectServices projectServices = new ProjectServices();
        private CompanyServices companyServices = new CompanyServices();
        private string idUser = Application.Current.Properties["IdUser"].ToString();
        private string idCompany = Application.Current.Properties["IdCompany"].ToString();

        public ObservableCollection<string> UserCollection = null;

        //Consultants met Key
        Dictionary<int, string> consultantWithKey = new Dictionary<int, string>();

        public CompanyUserListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            try
            {
                //Haalt alle users op van de company
                List<UserDto> consultants = await companyServices.GetUsersFromCompanyByIdAsync(int.Parse(idCompany));

                //Steekt alle users in ConsultantList (Picker)
                AddUserToConsultantList(UserCollection, consultants);
            }
            catch (Exception)
            {
                LabelNoUsers.IsEnabled = true;
                LabelNoUsers.IsVisible = true;
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            string consultantToEdit = Consultantlist.SelectedItem.ToString();
            int userid = 0;

            //Zoekt id van de user
            foreach (var c in consultantWithKey)
            {
                if (c.Value == consultantToEdit)
                {
                    userid = c.Key;
                }
            }

            Consultantlist.SelectedItem = null;
            await Navigation.PushModalAsync(new EditConsultantCompanyPage(userid));
        }

        //Users toevoegen aan Consultantlist (ListView)
        private void AddUserToConsultantList(ObservableCollection<string> UserCollection, List<UserDto> usersDto = null)
        {
            UserCollection = new ObservableCollection<string>();
            //Users(email en id) in Dictionary steken
            foreach (var user in usersDto)
            {
                consultantWithKey.Add(user.ID, user.Email);
            }

            //Dictionary in Consultantlist(ListView) steken
            foreach (var user in consultantWithKey)
            {
                UserCollection.Add(user.Value.ToString());
            }

            //Dictionary in Consultantlist(ListView) steken
            Consultantlist.ItemsSource = UserCollection;
        }

        private void AddConsultantButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AddConsultantCompanyPage();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CompanyOptionPage();
        }
    }
}