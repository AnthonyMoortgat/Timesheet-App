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
        private CompanyRoleServices companyRoleServices = new CompanyRoleServices();
        private List<CompanyRoleDto> companyRoles = new List<CompanyRoleDto>(); //alle roles van de company worden hier in geplaatst
        private Dictionary<int, string> CompanyRolesWithKey = new Dictionary<int, string>();//Role name en ID opslaan
        private ObservableCollection<string> CompanyRolesCollection; //List voor in RoleList (Listview)

        public Roles ()
		{
            InitializeComponent();
		}

        //Als het scherm verschijnt
        protected async override void OnAppearing()
        {
            companyRoles = await companyRoleServices.GetAllCompanyRolesAsync(1); //Alle company roles opslaan 
            AddRolesToRoleList(CompanyRolesCollection, companyRoles); //De roles in de Listview(RoleList) opslaan
        }

        //Naar de AddRolePage gaan
        private void Add_Role(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AddRolePage();
        }

        //Terug naar Company Option page
        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage(); //Moet nog verandert worden naar de Options page ipv MainPage
        }

        //Id zoeken van het geselecteerde item (role) en dit id meegeven aan de Edit/Delete rolepage
        private async void RoleList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            string logToEdit = RoleList.SelectedItem.ToString();
            int logid = 0;

            //Zoekt id van de role
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
            //Roles(naam en id) in Dictionary steken
            foreach (var role in companyRoles)
            {
                CompanyRolesWithKey.Add(role.ID, role.Name);
            }

            //Dictionary in RoleList(ListView) steken
            foreach (var log in CompanyRolesWithKey)
            {
                CompanyRolesCollection.Add(log.Value.ToString());
            }

            //Dictionary in RoleList(ListView) steken
            RoleList.ItemsSource = CompanyRolesCollection;
        }
    }
}

// Mobile Wits. How to pass data in pages using xamarin forms
// http://www.mobilewits.com/blog/how-to-pass-data-in-pages-using-xamarin-forms/
// Geraadpleegd op 7 mei 2019

// Stackoverflow. How do you switch pages in Xamarin Forms
// https://stackoverflow.com/questions/25165106/how-do-you-switch-pages-in-xamarin-forms
// Geraadpleegd op 7 mei 2019

// Stackoverflow. Xamarin forms listview itemselected functionality
// https://stackoverflow.com/questions/33376462/xamarin-forms-listview-itemselected-functionality
// Geraadpleegd op 7 mei 2019