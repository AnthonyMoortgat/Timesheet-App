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
        bool isDefault = true, manageCompanies = false, manageRoles = false, manageUsers = false, manageProjects = false;
        CompanyRoleServices companyRoleServices = new CompanyRoleServices();
        List<CompanyRoleDto> companyRoles = new List<CompanyRoleDto>();

		public AddRolePage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            companyRoles = await companyRoleServices.GetAllCompanyRolesAsync(1);
        }

        private void AddRoleButton_Clicked(object sender, EventArgs e)
        {
            if (CheckDescriptionAndName() == true)
            {
                DisplayAlert("Warning", $"Correct!", "Ok");
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

        private void BackToRoles(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Roles();
        } 
    }
}