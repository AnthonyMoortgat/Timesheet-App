using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Project;

namespace Timesheet_Library.Dto.Services
{
    public class ProjectServices
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
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZW1haWwiOiJtaWNoYWVsQGdtYWlsLmNvbSIsIm5iZiI6MTU1Njg4NTAzNCwiZXhwIjoxNTU2OTcxNDM0LCJpYXQiOjE1NTY4ODUwMzR9.GSdOjI0UVAM1owMRNyfCdJ5rqBm4tTPvr_quKXS4B5SN1yMOwT-GCLcuKL1qyDDxCTq_xTu7ncV14LqQtEoHHA");
            }
        }

        public async Task<List<ProjectDto>> GetAllProjectsAsync()
        {
            GetClient();
            string getAllProjects = null;
            List<ProjectDto> ProjectList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Projects/");
            if (response.IsSuccessStatusCode)
            {
                getAllProjects = await response.Content.ReadAsStringAsync();
                ProjectList = JsonConvert.DeserializeObject<List<ProjectDto>>(getAllProjects);
            }
            return ProjectList;
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int id)
        {
            GetClient();
            ProjectDto getProject = null;
            HttpResponseMessage response = await client.GetAsync($"api/Projects/{id}");
            if (response.IsSuccessStatusCode)
            {
                getProject = await response.Content.ReadAsAsync<ProjectDto>();
            }
            return getProject;
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectToCreateDto Project)
        {
            GetClient();
            ProjectDto createdProject = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Projects/", Project);
            if (response.IsSuccessStatusCode)
            {
                createdProject = await response.Content.ReadAsAsync<ProjectDto>();
            }
            return createdProject;
        }

        public async Task<bool> UpdateProjectByIdAsync(ProjectToUpdateDto Project, int id)
        {
            GetClient();
            ProjectDto updatedProject = null;
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Projects/{id}", Project);
            if (response.IsSuccessStatusCode)
            {
                updatedProject = await response.Content.ReadAsAsync<ProjectDto>();
                return true;
            }
            return false;
        }

        public async Task<ProjectDto> DeleteProjectByIdAsync(int id)
        {
            GetClient();
            ProjectDto deletedProject = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Projects/{id}");
            if (response.IsSuccessStatusCode)
            {
                deletedProject = await response.Content.ReadAsAsync<ProjectDto>();
            }
            return deletedProject;
        }

        public async Task<bool> AddUserToProjectAsync(int id, string email)
        {
            GetClient();
            //ProjectUserDto userProject = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Projects/{id}/Users?email={email}", id);
            if (response.IsSuccessStatusCode)
            {
                //userProject = await response.Content.ReadAsAsync<ProjectUserDto>();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveUserToProjectAsync(int id, int userId)
        {
            GetClient();
            ProjectUserDto userProject = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Projects/{id}/users?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                userProject = await response.Content.ReadAsAsync<ProjectUserDto>();
                return true;
            }
            return false;
        }

        public async Task<List<UserDto>> GetUsersFromProjectByIdAsync(int id)
        {
            GetClient();
            string getAllUsers = null;
            List<UserDto> UserList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Projects/{id}/Users");
            if (response.IsSuccessStatusCode)
            {
                getAllUsers = await response.Content.ReadAsStringAsync();
                UserList = JsonConvert.DeserializeObject<List<UserDto>>(getAllUsers);
            }
            return UserList;
        }
    }
}

// Microsoft. Call a Web API From a .NET Client (C#)
// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
// Geraadpleegd op 29 maart 2019