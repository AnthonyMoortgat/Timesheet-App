﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Log;
using Timesheet_Library.Dto.Project;

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

        public async Task<List<UserDto>> GetAllUsersAsync()
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

        public async Task<UserDto> GetUserByIdAsync(int id)
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

        public async Task<List<UserDto>> GetUserByEmailAsync(string email)
        {
            GetClient();
            string getUser = null;
            List<UserDto> UsersList = null;

            HttpResponseMessage response = await client.GetAsync($"api/users?email={email}");
            if (response.IsSuccessStatusCode)
            {
                getUser = await response.Content.ReadAsStringAsync();
                UsersList = JsonConvert.DeserializeObject<List<UserDto>>(getUser);
            }
            return UsersList;
        }

        public async Task<UserDto> CreateUserAsync(UserToCreateDto user)
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

        public async Task<UserDto> UpdateUserByIdAsync(UserToUpdateDto user, int id)
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

        public async Task<UserDto> DeleteUserByIdAsync(int id)
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

        public async Task<List<LogDto>> GetAllUserLogsAsync(int id)
        {
            GetClient();
            string getAllUserLogs = null;
            List<LogDto> LogList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Users/{id}/Logs");
            if (response.IsSuccessStatusCode)
            {
                getAllUserLogs = await response.Content.ReadAsStringAsync();
                LogList = JsonConvert.DeserializeObject<List<LogDto>>(getAllUserLogs);
                return LogList;
            }
            return null;
        }

        public async Task<List<CompanyDto>> GetAllUserCompaniesAsync(int id)
        {
            GetClient();
            string getAllCompanyProjects = null;
            List<CompanyDto> CompanyList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Users/{id}/Companies");
            if (response.IsSuccessStatusCode)
            {
                getAllCompanyProjects = await response.Content.ReadAsStringAsync();
                CompanyList = JsonConvert.DeserializeObject<List<CompanyDto>>(getAllCompanyProjects);
                return CompanyList;
            }
            return null;
        }

        public async Task<List<ProjectDto>> GetAllUserProjectsAsync(int id)
        {
            GetClient();
            string getAllUserProjects = null;
            List<ProjectDto> ProjectList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Users/{id}/Projects");
            if (response.IsSuccessStatusCode)
            {
                getAllUserProjects = await response.Content.ReadAsStringAsync();
                ProjectList = JsonConvert.DeserializeObject<List<ProjectDto>>(getAllUserProjects);
                return ProjectList;
            }
            return null;
        }
    }
}

// Microsoft. Call a Web API From a .NET Client (C#)
// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
// Geraadpleegd op 29 maart 2019