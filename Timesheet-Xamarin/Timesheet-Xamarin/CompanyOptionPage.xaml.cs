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
    public partial class CompanyOptionPage : ContentPage
    {
        private string idCompany = Application.Current.Properties["IdCompany"].ToString();
        private string idUser = Application.Current.Properties["IdUser"].ToString();
        private CompanyRoleServices companyRoleServices = new CompanyRoleServices();
        private bool manageCompany = false, manageProjects = false, manageUsers = false, manageRoles = false;

        public CompanyOptionPage()
        {
            InitializeComponent();
            CompanyName.Text = idCompany;
            Start();
        }

        private async void Start()
        {
            try
            {
                List<CompanyRoleDto> companyRoles = await companyRoleServices.GetUsersCompanyRolesAsync(int.Parse(idCompany), int.Parse(idUser));

                for (int i = 0; i < companyRoles.Count; i++)
                {
                    if (companyRoles[i].ManageCompany == true)
                    {
                        manageCompany = true;
                        ManageCompany.IsEnabled = true;
                    }
                    if (companyRoles[i].ManageProjects == true)
                    {
                        manageProjects = true;
                        ManageProjects.IsEnabled = true;
                    }
                    if (companyRoles[i].ManageUsers == true)
                    {
                        manageUsers = true;
                        ManageUsers.IsEnabled = true;
                    }
                    if (companyRoles[i].ManageRoles == true)
                    {
                        manageRoles = true;
                        ManageRoles.IsEnabled = true;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void ManageCompany_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CompanyUserListPage();
        }

        private void ManageProjects_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ProjectListPage();
        }

        private void ManageUsers_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CompanyUsers();
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