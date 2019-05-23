using System;
using System.Collections.Generic;
using Timesheet_Library.Dto;
using Timesheet_Library.Dto.Log;
using Timesheet_Library.Dto.Project;
using Timesheet_Library.Dto.Services;

namespace ApiServices
{
    class Program
    {
        static void Main()
        {
            //Create address
            AddressToCreateDto createdAddress = new AddressToCreateDto
            {
                Country = "Belgium",
                City = "Brussel",
                PostalCode = "1000",
                Street = "testStraat",
                HouseNumber = "20"
            };

            //Create user
            UserToCreateDto createdUser = new UserToCreateDto
            {
                FirstName = "Mich",
                LastName = "dg",
                Email = "michael@hotmail.com",
                PhoneNumber = "+3256798487",
                Psw = "hallo",
                Address = createdAddress
            };

            //Update address
            AddressToUpdateDto updatedAddress = new AddressToUpdateDto
            {
                Country = "Belgium",
                City = "Brussel",
                PostalCode = "1000",
                Street = "testStraat1",
                HouseNumber = "35"
            };

            //Update user
            UserToUpdateDto updatedUser = new UserToUpdateDto
            {
                FirstName = "Michael",
                LastName = "DG",
                Email = "michael@hotmail.com",
                PhoneNumber = "+3256798489",
                Address = updatedAddress
            };

            //Login
            UserToLoginDto login = new UserToLoginDto
            {
                Email = "michael@gmail.com",
                Password = "Azerty123"
            };

            //Create company
            CompanyToCreateDto createdCompany = new CompanyToCreateDto
            {
                Name = "Microsoft1",
                Address = createdAddress
            };

            //Update company
            CompanyToUpdateDto updatedCompany = new CompanyToUpdateDto
            {
                Name = "Microsoft 2",
                Address = updatedAddress
            };

            //Create companyrole
            CompanyRoleToCreateDto createdCompanyRole = new CompanyRoleToCreateDto
            {
                Name = "CompanyRole1",
                Description = "It's just a test",
                IsDefault = true,
                ManageCompany = true,
                ManageUsers = false,
                ManageProjects = true,
                ManageRoles = true
            };

            //Update companyrole
            CompanyRoleToUpdateDto updatedCompanyRole = new CompanyRoleToUpdateDto
            {
                Name = "CompanyRole1Updated",
                Description = "It's just a test for something",
                IsDefault = true,
                ManageCompany = true,
                ManageUsers = true,
                ManageProjects = true,
                ManageRoles = true
            };

            //Create project
            ProjectToCreateDto createdProject = new ProjectToCreateDto
            {
                CompanyID = 1,
                Name = "Tim",
                Description = "desc"
            };

            //Update project
            ProjectToUpdateDto updatedProject = new ProjectToUpdateDto
            {
                CompanyID = 2,
                Name = "Kevin",
                Description = "desc 2"
            };


            //Testen API calls
            UserServices userServices = new UserServices();
            CompanyServices companyServices = new CompanyServices();
            ProjectServices projectServices = new ProjectServices();
            CompanyRoleServices companyRoleServices = new CompanyRoleServices();
            LogServices logServices = new LogServices();
            SessionServices sessionServices = new SessionServices();

            UserDto user1 = new UserDto();
            CompanyDto c = new CompanyDto();
            CompanyRoleDto cr = new CompanyRoleDto();
            ProjectDto p = new ProjectDto();


            //Userservices  //OK

            //user1 = userServices.GetUserByIdAsync(1).GetAwaiter().GetResult();
            //List<UserDto> userlist = userServices.GetAllUsersAsync().GetAwaiter().GetResult();
            //user1 = userServices.CreateUserAsync(createdUser).GetAwaiter().GetResult();
            //user1 = userServices.UpdateUserByIdAsync(updatedUser, 1).GetAwaiter().GetResult();
            //user1 = userServices.DeleteUserByIdAsync(3).GetAwaiter().GetResult();
            //List<LogDto> user = userServices.GetAllUserLogsAsync(1).GetAwaiter().GetResult();
            //List<UserDto> users = userServices.GetUserByEmailAsync("kevin@gmail.com").GetAwaiter().GetResult();
            //List<LogDto> lk = userServices.GetAllUserLogsAsync(5).GetAwaiter().GetResult();


            //Sessionservices  //OK

            //string str = sessionServices.CreateSession(login);
            //string str = sessionServices.DeleteSessionAsync().GetAwaiter().GetResult();


            //Companyservices  //OK

            //List<CompanyDto> companylist = companyServices.GetAllCompaniesAsync().GetAwaiter().GetResult();
            //c = companyServices.GetCompanyByIdAsync(1).GetAwaiter().GetResult();
            //c = companyServices.CreateCompanyAsync(createdCompany).GetAwaiter().GetResult();
            //c = companyServices.UpdateCompanyByIdAsync(updatedCompany, 1).GetAwaiter().GetResult();
            //List<UserDto> cl = companyServices.GetUsersFromCompanyByIdAsync(1).GetAwaiter().GetResult();
            //bool b = companyServices.AddUserToCompanyByIdAsync(1, 8).GetAwaiter().GetResult();


            //CompanyRoleservices  //OK

            //cr = companyRoleServices.CreateCompanyRoleAsync(createdCompanyRole, 1).GetAwaiter().GetResult();
            //List<CompanyRoleDto> crlist = companyRoleServices.GetAllCompanyRolesAsync(1).GetAwaiter().GetResult();
            //cr = companyRoleServices.GetCompanyRoleByIdAsync(1, 3).GetAwaiter().GetResult();
            //cr = companyRoleServices.UpdateCompanyRoleByIdAsync(updatedCompanyRole, 1, 3).GetAwaiter().GetResult();


            //Projectservices  //OK

            //bool bp = projectServices.UpdateProjectByIdAsync(updatedProject, 1).GetAwaiter().GetResult();
            //List<ProjectDto> projects = companyServices.GetAllCompanyProjectsAsync(1).GetAwaiter().GetResult();
            //List<ProjectDto> projects = userServices.GetAllUserProjectsAsync(1).GetAwaiter().GetResult();
            //bool pu = projectServices.RemoveUserToProjectAsync(1, 1).GetAwaiter().GetResult();
            //bool u = projectServices.AddUserToProjectAsync(1, "michael@hotmail.com").GetAwaiter().GetResult();
            //bool u = projectServices.RemoveUserToProjectAsync(1, 7).GetAwaiter().GetResult();
        }
    }
}