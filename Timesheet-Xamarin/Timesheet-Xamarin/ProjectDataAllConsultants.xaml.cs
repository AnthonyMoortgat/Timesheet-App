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
	public partial class ProjectDataAllConsultants : ContentPage
	{
		public ProjectDataAllConsultants ()
		{
			InitializeComponent ();
		}

    protected async override void OnAppearing()
    {
        //consultant = await companyRoleServices.GetAllCompanyRolesAsync(1);
        //companyRole = await companyRoleServices.GetCompanyRoleByIdAsync(1, roleId);
        //CheckBoxIsDefault.IsChecked = companyRole.IsDefault;
        //CheckBoxManageCompanies.IsChecked = companyRole.ManageCompany;
        //CheckBoxManageRoles.IsChecked = companyRole.ManageProjectRoles;
        //CheckBoxManageUsers.IsChecked = companyRole.ManageUsers;
        //CheckBoxManageProjects.IsChecked = companyRole.ManageProjects;
        //EntryName.Text = companyRole.Name;
        //RoleName = EntryName.Text;
        //EntryDescription.Text = companyRole.Description;
    }
}
}