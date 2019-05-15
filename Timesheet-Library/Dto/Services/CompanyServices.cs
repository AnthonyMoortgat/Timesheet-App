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
    public class CompanyServices
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

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            GetClient();
            string getAllCompanies = null;
            List<CompanyDto> CompanyList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Companies/");
            if (response.IsSuccessStatusCode)
            {
                getAllCompanies = await response.Content.ReadAsStringAsync();
                CompanyList = JsonConvert.DeserializeObject<List<CompanyDto>>(getAllCompanies);
            }
            return CompanyList; 
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            GetClient();
            CompanyDto getCompany = null;
            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}");
            if (response.IsSuccessStatusCode)
            {
                getCompany = await response.Content.ReadAsAsync<CompanyDto>();
            }
            return getCompany;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyToCreateDto company)
        {
            GetClient();
            CompanyDto createdCompany = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Companies/", company);
            if (response.IsSuccessStatusCode)
            {
                createdCompany = await response.Content.ReadAsAsync<CompanyDto>();
            }
            return createdCompany;
        }

        public async Task<CompanyDto> UpdateCompanyByIdAsync(CompanyToUpdateDto company, int id)
        {
            GetClient();
            CompanyDto updatedCompany = null;
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Companies/{id}", company);
            if (response.IsSuccessStatusCode)
            {
                updatedCompany = await response.Content.ReadAsAsync<CompanyDto>();
            }
            return updatedCompany;
        }

        public async Task<CompanyDto> DeleteCompanyByIdAsync(int id)
        {
            GetClient();
            CompanyDto deletedCompany = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Companies/{id}");
            if (response.IsSuccessStatusCode)
            {
                deletedCompany = await response.Content.ReadAsAsync<CompanyDto>();
            }
            return deletedCompany;
        }

        public async Task<List<UserDto>> GetUsersFromCompanyByIdAsync(int id)
        {
            GetClient();
            string getAllCompanyUsers = null;
            List<UserDto> getCompanyUsers = null;

            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}/Users");
            if (response.IsSuccessStatusCode)
            {
                getAllCompanyUsers = await response.Content.ReadAsStringAsync();
                getCompanyUsers = JsonConvert.DeserializeObject<List<UserDto>>(getAllCompanyUsers);
            }
            return getCompanyUsers;
        }

        public async Task<bool> AddUserToCompanyByIdAsync(int id, string email)
        {
            GetClient();
            string getAllCompanies = null;

            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Companies/{id}/Users?email={email}", id);
            if (response.IsSuccessStatusCode)
            {
                getAllCompanies = await response.Content.ReadAsStringAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserFromCompanyByIdAsync(int id, int userid)
        {
            GetClient();
            CompanyDto deletedCompany = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Companies/{id}/Users/{userid}");
            if (response.IsSuccessStatusCode)
            {
                deletedCompany = await response.Content.ReadAsAsync<CompanyDto>();
                return true;
            }
            return false;
        }

        public async Task<List<ProjectDto>> GetAllCompanyProjectsAsync(int id)
        {
            GetClient();
            string getAllCompanyProjects = null;
            List<ProjectDto> ProjectList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}/Projects");
            if (response.IsSuccessStatusCode)
            {
                getAllCompanyProjects = await response.Content.ReadAsStringAsync();
                ProjectList = JsonConvert.DeserializeObject<List<ProjectDto>>(getAllCompanyProjects);
            }
            return ProjectList;
        }
    }
}

// Microsoft. Call a Web API From a .NET Client (C#)
// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
// Geraadpleegd op 29 maart 2019