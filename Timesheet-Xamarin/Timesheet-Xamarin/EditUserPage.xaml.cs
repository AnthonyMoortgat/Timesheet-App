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

        public EditUserPage (UserDto user)
		{
			InitializeComponent ();
            Start(user);
        }
        private async void Start(UserDto user)
        {
            Email.Text = user.Email;
            Name.Text = user.LastName + " " + user.FirstName;
            List<CompanyRoleDto> tempUserRoles = await companyRoleServices.GetUsersCompanyRolesAsync(int.Parse(idCompany), int.Parse(idUser));
            List<CompanyRoleDto> tempAllRoles = await companyRoleServices.GetAllCompanyRolesAsync(int.Parse(idCompany));

            foreach (var role in tempAllRoles)
            {
                bool hasRole = false;
                foreach (var userRole in tempUserRoles)
                {                 
                    if (role.ID == userRole.ID)
                    {
                        MakeCheckbox(true, role.Name);
                        hasRole = true;
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
        public void ClickCancel(object sender, EventArgs args)
        {
            Application.Current.MainPage = new CompanyUsers();
        }
    }
}