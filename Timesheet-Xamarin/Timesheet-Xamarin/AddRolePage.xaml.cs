using System;
using System.Collections.Generic;
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
	public partial class AddRolePage : ContentPage
	{
        private bool isDefault = true, manageCompanies = false, manageRoles = false, manageUsers = false, manageProjects = false; //booleans voor status checkbox op te slaan
        private CompanyRoleServices companyRoleServices = new CompanyRoleServices();
        private List<CompanyRoleDto> companyRoles = new List<CompanyRoleDto>(); //companyRoles moeten hierin geplaatst worden
        //Company ID

		public AddRolePage ()
		{
			InitializeComponent ();
		}

        //Alle companies ophalen
        protected async override void OnAppearing()
        {
            companyRoles = await companyRoleServices.GetAllCompanyRolesAsync(1); //companyRoles worden in deze array gezet
        }

        //Role toevoegen
        private async void AddRoleButton_Clicked(object sender, EventArgs e)
        {
            if (CheckDescriptionAndName() == true)
            {
                CompanyRoleToCreateDto companyRole = new CompanyRoleToCreateDto {
                    Name = EntryName.Text,
                    Description = EntryDescription.Text,
                    IsDefault = isDefault,
                    ManageCompany = manageCompanies,
                    ManageUsers = manageUsers,
                    ManageProjects = manageProjects,
                    ManageRoles = manageRoles
                };
                AddRoleButton.IsEnabled = false;
                CompanyRoleDto companyRole1 = await companyRoleServices.CreateCompanyRoleAsync(companyRole, 1);
                Application.Current.MainPage = new Roles();
            }
        }

        //Check of de description en de nama leeg is
        private bool CheckDescriptionAndName()
        {
            if (EntryDescription.Text == "" && EntryName.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a name and description!", "Ok");
                return false;
            }
            else if (EntryName.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a name!", "Ok");
                return false;
            }
            else if (EntryDescription.Text == "")
            {
                DisplayAlert("Warning", $"You have not added a description!", "Ok");
                return false;
            }
            else
            {
                if (CheckRoleExists() == false)
                {
                    DisplayAlert("Warning", "Name of the Role already exists!", "Ok");
                    return false;
                }
                return true;
            }
        }

        //Kijken of de role al bestaat
        private bool CheckRoleExists()
        {
            foreach (var companyRole in companyRoles)
            {
                if (companyRole.Name == EntryName.Text)
                {
                    return false;
                }
            }
            return true;
        }

        //Checkbox isDefault
        private void CheckBoxIsDefault_IsCheckedChanged(object sender, TappedEventArgs e)
        {
            if(CheckBoxIsDefault.IsChecked == true)
            {
                CheckBoxManageCompanies.IsChecked = false;
                CheckBoxManageRoles.IsChecked = false;
                CheckBoxManageUsers.IsChecked = false;
                CheckBoxManageProjects.IsChecked = false;
                isDefault = true;
            }
            else
            {
                isDefault = false;
            }
        }

        //Checkbox manage Companies
        private void CheckBoxManageCompanies_IsCheckedChanged(object sender, TappedEventArgs e)
        {
            if (CheckBoxManageCompanies.IsChecked == true)
            {
                CheckBoxIsDefault.IsChecked = false;
                isDefault = false;
                manageCompanies = true;
            }
            else
            {
                manageCompanies = false;
            }
        }

        //Checkbox manage Roles
        private void CheckBoxManageRoles_IsCheckedChanged(object sender, TappedEventArgs e)
        {
            if (CheckBoxManageRoles.IsChecked == true)
            {
                CheckBoxIsDefault.IsChecked = false;
                isDefault = false;
                manageRoles = true;
            }
            else
            {
                manageRoles = false;
            }
        }

        //Checkbox manage Users
        private void CheckBoxManageUsers_IsCheckedChanged(object sender, TappedEventArgs e)
        {
            if (CheckBoxManageUsers.IsChecked == true)
            {
                CheckBoxIsDefault.IsChecked = false;
                isDefault = false;
                manageUsers = true;
            }
            else
            {
                manageUsers = false;
            }
        }

        //Checkbox manage Projects
        private void CheckBoxManageProjects_IsCheckedChanged(object sender, TappedEventArgs e)
        {
            if (CheckBoxManageProjects.IsChecked == true)
            {
                CheckBoxIsDefault.IsChecked = false;
                isDefault = false;
                manageProjects = true;
            }
            else
            {
                manageProjects = false;
            }
        }

        //Terug naar de Rolespage
        private void BackToRoles(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Roles();
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

// Intelliabb. Checkbox for Xamarin Forms
// https://intelliabb.com/2018/09/20/checkbox-for-xamarin-forms/
// Geraadpleegd op 13 mei 2019