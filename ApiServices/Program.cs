using System;
using System.Collections.Generic;
using Timesheet_Library.Dto;
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
                Name = "Dell",
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
                ManageProjectRoles = true,
                ManageProjectUsers = true
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
                ManageProjectRoles = true,
                ManageProjectUsers = false
            };

            //Test
            UserDto user1 = new UserDto();
            CompanyDto c = new CompanyDto();
            CompanyRoleDto cr = new CompanyRoleDto();

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
            c = CompanyServices.UpdateCompanyByIdAsync(updatedCompany, 1).GetAwaiter().GetResult();

            Console.WriteLine(c.Name);

            /*
            for (int i = 0; i < companylist.Count; i++)
            {
                Console.WriteLine(companylist[i].Name);
            }
            */

            Console.ReadLine();
        }
    }
}
