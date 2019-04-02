using System;
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
                Password = "hallo"
            };

            //Test
            UserDto user1 = new UserDto();

            //user1 = UserServices.GetUserByIdAsync(16).GetAwaiter().GetResult();
            //List<UserDto> userlist = UserServices.GetAllUsersAsync().GetAwaiter().GetResult();
            //user1 = UserServices.CreateUserAsync(createdUser).GetAwaiter().GetResult();
            //user1 = UserServices.UpdateUserByIdAsync(updatedUser, 16).GetAwaiter().GetResult();
            //user1 = UserServices.DeleteUserByIdAsync(14).GetAwaiter().GetResult();
            string str = SessionServices.CreateSessionAsync(login).GetAwaiter().GetResult();
            //string str = SessionServices.DeleteSessionAsync().GetAwaiter().GetResult();

            //Console.WriteLine(user1.ID);

            /*
            for (int i = 0; i < userlist.Count; i++)
            {
                Console.WriteLine(userlist[i].ID);
            }
            */

            Console.WriteLine(str);

            Console.ReadLine();
        }
    }
}
