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
	public partial class Roles : ContentPage
	{
        CompanyRoleServices companyRoleServices = new CompanyRoleServices();
        List<CompanyRoleDto> companyRoles = new List<CompanyRoleDto>();
        Dictionary<int, string> CompanyRolesWithKey = new Dictionary<int, string>();
        ObservableCollection<string> CompanyRolesCollection;


        public Roles ()
		{
            InitializeComponent();
		}

        protected async override void OnAppearing()
        {
            companyRoles = await companyRoleServices.GetAllCompanyRolesAsync(1);
            AddRolesToRoleList(CompanyRolesCollection, companyRoles);
        }

        private void Add_Role(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AddRolePage();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private async void RoleList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            string logToEdit = RoleList.SelectedItem.ToString();
            int logid = 0;

            //Zoekt id van de log
            foreach (var role in CompanyRolesWithKey)
            {
                if (role.Value == logToEdit)
                {
                    logid = role.Key;
                }
            }

            RoleList.SelectedItem = null;
            await Navigation.PushModalAsync(new EditRole(logid));
        }

        //Company Roles toevoegen aan RoleList (ListView)
        private void AddRolesToRoleList(ObservableCollection<string> CompanyRolesCollection, List<CompanyRoleDto> companyRoles = null)
        {
            CompanyRolesCollection = new ObservableCollection<string>();
            //Logs(naam en id) in Dictionary steken
            foreach (var role in companyRoles)
            {
                CompanyRolesWithKey.Add(role.ID, role.Name);
            }

            //Dictionary in LogList(ListView) steken
            foreach (var log in CompanyRolesWithKey)
            {
                CompanyRolesCollection.Add(log.Value.ToString());
            }

            //Dictionary in LogList(ListView) steken
            RoleList.ItemsSource = CompanyRolesCollection;
        }
    }
}