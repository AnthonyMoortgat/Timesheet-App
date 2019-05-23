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

            ProjectToCreateDto createdProject = new ProjectToCreateDto
            {
                CompanyID = 1,
                Name = "Tim",
                Description = "desc"
            };

            ProjectToUpdateDto updatedProject = new ProjectToUpdateDto
            {
                CompanyID = 2,
                Name = "Kevin",
                Description = "desc 2"
            };


            //Test
            UserServices userServices = new UserServices();
            CompanyServices companyServices = new CompanyServices();
            ProjectServices projectServices = new ProjectServices();
            CompanyRoleServices companyRoleServices = new CompanyRoleServices();

            UserDto user1 = new UserDto();
            CompanyDto c = new CompanyDto();
            CompanyRoleDto cr = new CompanyRoleDto();
            ProjectDto p = new ProjectDto();

            //user1 = UserServices.GetUserByIdAsync(1).GetAwaiter().GetResult();
            //List<UserDto> userlist = UserServices.GetAllUsersAsync().GetAwaiter().GetResult();
            //user1 = UserServices.CreateUserAsync(createdUser).GetAwaiter().GetResult();
            //user1 = UserServices.UpdateUserByIdAsync(updatedUser, 1).GetAwaiter().GetResult();
            //user1 = UserServices.DeleteUserByIdAsync(3).GetAwaiter().GetResult();
            //string str = SessionServices.CreateSession(login);
            //string str = SessionServices.DeleteSessionAsync().GetAwaiter().GetResult();

            //Console.WriteLine(user1.ID);

            /*
            for (int i = 0; i < userlist.Count; i++)
            {
                Console.WriteLine(userlist[i].FirstName);
            }
            */

            //Console.WriteLine(str);

            //List<CompanyDto> companylist = CompanyServices.GetAllCompaniesAsync().GetAwaiter().GetResult();
            //c = CompanyServices.GetCompanyByIdAsync(1).GetAwaiter().GetResult();
            //c = CompanyServices.CreateCompanyAsync(createdCompany).GetAwaiter().GetResult();
            //c = CompanyServices.UpdateCompanyByIdAsync(updatedCompany, 1).GetAwaiter().GetResult();

            //Console.WriteLine(c.Name);

            /*
            for (int i = 0; i < companylist.Count; i++)
            {
                Console.WriteLine(companylist[i].Name);
            }
            */

            //cr = CompanyRoleServices.CreateCompanyRoleAsync(createdCompanyRole, 1).GetAwaiter().GetResult();
            //List<CompanyRoleDto> crlist = CompanyRoleServices.GetAllCompanyRolesAsync(1).GetAwaiter().GetResult();
            /*for (int i = 0; i < crlist.Count; i++)
            {
                Console.WriteLine(crlist[i].Name);
            }
            */

            ////cr = CompanyRoleServices.GetCompanyRoleByIdAsync(1, 3).GetAwaiter().GetResult();
            //cr = CompanyRoleServices.UpdateCompanyRoleByIdAsync(updatedCompanyRole, 1, 3).GetAwaiter().GetResult();


            //List<UserDto> cl = CompanyServices.GetUsersFromCompanyByIdAsync(1).GetAwaiter().GetResult();

            //Console.WriteLine(cl[0].FirstName);

            //bool b = CompanyServices.AddUserToCompanyByIdAsync(1, 8).GetAwaiter().GetResult();

            //bool bp = ProjectServices.UpdateProjectByIdAsync(updatedProject, 1).GetAwaiter().GetResult();
            //Console.WriteLine(bp);

            //List<LogDto> user = userServices.GetAllUserLogsAsync(1).GetAwaiter().GetResult();
            //List<UserDto> users = userServices.GetUserByEmailAsync("kevin@gmail.com").GetAwaiter().GetResult();
            //Console.WriteLine(user[0].StartTime.ToString("HH:mm"));
            //string s = $"{user[0].StartTime.ToString("dd/MM/yyyy")} | {user[0].StartTime.ToString("HH:mm")} - {user[0].StopTime.ToString("HH:mm")}: {user[0].Description} - Total: {user[0].StopTime - user[0].StartTime}";

            //List<ProjectDto> projects = companyServices.GetAllCompanyProjectsAsync(1).GetAwaiter().GetResult();
            List<ProjectDto> projects = userServices.GetAllUserProjectsAsync(1).GetAwaiter().GetResult();
            //bool pu = projectServices.RemoveUserToProjectAsync(1, 1).GetAwaiter().GetResult();
            //List<LogDto> lk = userServices.GetAllUserLogsAsync(5).GetAwaiter().GetResult();

            //bool u = projectServices.AddUserToProjectAsync(1, "michael@hotmail.com").GetAwaiter().GetResult();
            //bool u = projectServices.RemoveUserToProjectAsync(1, 7).GetAwaiter().GetResult();

            //Console.WriteLine(u.ProjectID);
            //Console.WriteLine(u.UserID);
            Console.WriteLine(projects[0].Name);

            Console.ReadLine();
        }
    }
}