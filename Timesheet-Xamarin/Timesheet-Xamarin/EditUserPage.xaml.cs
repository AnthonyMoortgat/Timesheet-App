using IntelliAbb.Xamarin.Controls;
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
	public partial class EditUserPage : ContentPage
	{
        CompanyRoleServices companyRoleServices = new CompanyRoleServices();
        private string idCompany = Application.Current.Properties["IdCompany"].ToString();
        private string idUser = Application.Current.Properties["IdUser"].ToString();

        List<CompanyRoleDto> tempAllRoles;
        UserDto user;
        public EditUserPage (UserDto user)
		{
            this.user = user;
			InitializeComponent ();
            Start();
        }
        private async void Start()
        {
            Email.Text = user.Email;
            Name.Text = user.LastName + " " + user.FirstName;
            List<CompanyRoleDto> tempUserRoles = await companyRoleServices.GetUsersCompanyRolesAsync(int.Parse(idCompany), user.ID);
            tempAllRoles = await companyRoleServices.GetAllCompanyRolesAsync(int.Parse(idCompany));

            foreach (var role in tempAllRoles)
            {
                bool hasRole = false;
                foreach (var userRole in tempUserRoles)
                {                 
                    if (role.ID == userRole.ID)
                    {
                        MakeCheckbox(true, role.Name);
                        hasRole = true;
                        break;
                    }                  
                }
                if (!hasRole)
                {
                    MakeCheckbox(false, role.Name);
                }
            }
        }
        private void MakeCheckbox(bool check, string value)
        {
            Checkbox c = new Checkbox()
            {               
                IsChecked = check               
            };
            Label l = new Label()
            {
                Text = value
            };
            StackLayout s = new StackLayout()
            {              
                Orientation = StackOrientation.Horizontal
            };
            s.Children.Add(c);
            s.Children.Add(l);

            RolesList.Children.Add(s);
        }
        private async void EditUserRoles(object sender, EventArgs e)
        {
            List<bool> checkRoles = new List<bool>();
            foreach (StackLayout stack in RolesList.Children)
            {
                var check = (Checkbox)stack.Children[0];
                var isChecked = check.IsChecked;
                checkRoles.Add(isChecked);
            }
            var index = 0;
            foreach (var role in tempAllRoles)
            {
                if (checkRoles[index])
                {
                    await companyRoleServices.CreateUserCompanyRolesAsync(int.Parse(idCompany), user.ID, role.ID);
                    index++;
                }
                else
                {
                    await companyRoleServices.DeleteCompanyRoleByIdAsync(int.Parse(idCompany), user.ID, role.ID);
                    index++;
                }
                Application.Current.MainPage = new CompanyUsers();
            }
        }
        public void ClickCancel(object sender, EventArgs args)
        {
            Application.Current.MainPage = new CompanyUsers();
        }
    }
}