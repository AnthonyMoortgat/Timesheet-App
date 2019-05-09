using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Log;

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
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMiIsImVtYWlsIjoibWljaGFlbEBnbWFpbC5jb20iLCJuYmYiOjE1NTQyMjU1ODAsImV4cCI6MTU1NDMxMTk4MCwiaWF0IjoxNTU0MjI1NTgwfQ.Qe8R3CvFyQkZvu-bLdOT4yrrYjSEGK5aeVvmqMIPkpH-qlYGx2cBrcsoIJ4TL-KjrEuudWCIgOWDrD2364cq_w");
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

        public static async Task<List<LogDto>> GetAllUserLogsAsync(int id)
        {
            GetClient();
            string getAllUserLogs = null;
            List<LogDto> LogList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Users/{id}/Logs");
            if (response.IsSuccessStatusCode)
            {
                getAllUserLogs = await response.Content.ReadAsStringAsync();
                LogList = JsonConvert.DeserializeObject<List<LogDto>>(getAllUserLogs);
            }
            return LogList;
        }
    }
}