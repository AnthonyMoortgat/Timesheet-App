using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet_Library.Dto.Services
{
    public class UserServices
    {
        private static HttpClient client = null;

        public static void GetClient()
        {
            if (client == null)
            {
                client = new HttpClient();

                //Set up client
                client.BaseAddress = new Uri("https://timesheetappapi20190303094246.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMiIsImVtYWlsIjoibWljaGFlbEBnbWFpbC5jb20iLCJuYmYiOjE1NTQxMzYzMTQsImV4cCI6MTU1NDIyMjcxNCwiaWF0IjoxNTU0MTM2MzE0fQ.5LPuMM1l_z8ToFLqw0X-kdl8Xy1dCDbjWtd_l_sDvaKC6YiXnQsZB2sff7HaPxSsGK6u5sLL49-4n6_CEm8J9A");
            }
        }

        public static async Task<List<UserDto>> GetAllUsersAsync()
        {
            GetClient();
            string getAllUsers = null;
            List<UserDto> UsersList = null;

            HttpResponseMessage response = await client.GetAsync($"api/users/");
            if (response.IsSuccessStatusCode)
            {
                getAllUsers = await response.Content.ReadAsStringAsync();
                UsersList = JsonConvert.DeserializeObject<List<UserDto>>(getAllUsers);
            }
            return UsersList;
        }

        public static async Task<UserDto> GetUserByIdAsync(int id)
        {
            GetClient();
            UserDto getUser = null;
            HttpResponseMessage response = await client.GetAsync($"api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                getUser = await response.Content.ReadAsAsync<UserDto>();
            }
            return getUser;
        }

        public static async Task<UserDto> CreateUserAsync(UserToCreateDto user)
        {
            GetClient();
            UserDto createdUser = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/users/", user);
            if (response.IsSuccessStatusCode)
            {
                createdUser = await response.Content.ReadAsAsync<UserDto>();
            }
            return createdUser;
        }

        public static async Task<UserDto> UpdateUserByIdAsync(UserToUpdateDto user, int id)
        {
            GetClient();
            UserDto updatedUser = null;
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/users/{id}", user);
            if (response.IsSuccessStatusCode)
            {
                updatedUser = await response.Content.ReadAsAsync<UserDto>();
            }
            return updatedUser;
        }

        public static async Task<UserDto> DeleteUserByIdAsync(int id)
        {
            GetClient();
            UserDto deletedUser = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                deletedUser = await response.Content.ReadAsAsync<UserDto>();
            }
            return deletedUser;
        }
    }
}