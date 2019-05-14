using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet_Library.Dto.Services
{
    public class CompanyRoleServices
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

        public async Task<List<CompanyRoleDto>> GetAllCompanyRolesAsync(int id)
        {
            GetClient();
            string getAllCompanyRoles = null;
            List<CompanyRoleDto> CompanyRoleList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}/Roles");
            if (response.IsSuccessStatusCode)
            {
                getAllCompanyRoles = await response.Content.ReadAsStringAsync();
                CompanyRoleList = JsonConvert.DeserializeObject<List<CompanyRoleDto>>(getAllCompanyRoles);
            }
            return CompanyRoleList;
        }

        public async Task<CompanyRoleDto> GetCompanyRoleByIdAsync(int id, int roleId)
        {
            GetClient();
            CompanyRoleDto getCompanyRole = null;
            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}/Roles/{roleId}");
            if (response.IsSuccessStatusCode)
            {
                getCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return getCompanyRole;
        }

        public async Task<CompanyRoleDto> CreateCompanyRoleAsync(CompanyRoleToCreateDto companyRole, int id)
        {
            GetClient();
            CompanyRoleDto createdCompanyRole = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Companies/{id}/Roles", companyRole);
            if (response.IsSuccessStatusCode)
            {
                createdCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return createdCompanyRole;
        }

        public async Task<CompanyRoleDto> UpdateCompanyRoleByIdAsync(CompanyRoleToUpdateDto companyRole, int id, int roleId)
        {
            GetClient();
            CompanyRoleDto updatedCompanyRole = null;
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Companies/{id}/Roles/{roleId}", companyRole);
            if (response.IsSuccessStatusCode)
            {
                updatedCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return updatedCompanyRole;
        }

        public async Task<bool> DeleteCompanyRoleByIdAsync(int id, int roleId)
        {
            GetClient();
            CompanyRoleDto deletedCompanyRole = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Companies/{id}/Roles/{roleId}");
            if (response.IsSuccessStatusCode)
            {
                deletedCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
                return true;
            }
            return false;
        }

        public async Task<CompanyRoleDto> GetUsersCompanyRolesAsync(int id, int userId)
        {
            GetClient();
            CompanyRoleDto getCompanyRole = null;
            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}/Users/{userId}/Roles");

            if (response.IsSuccessStatusCode)
            {
                getCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return getCompanyRole;
        }

        public async Task<bool> CreateUserCompanyRolesAsync(int id, int userId, int roleId)
        {
            GetClient();
            CompanyRoleDto createdCompanyRole = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Companies/{id}/Users/{userId}/Roles?roleId={roleId}", roleId);
            if (response.IsSuccessStatusCode)
            {
                createdCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCompanyRoleByIdAsync(int id, int userId, int roleId)
        {
            GetClient();
            CompanyRoleDto deletedCompanyRole = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Companies/{id}/Users/{userId}/Roles/{roleId}");
            if (response.IsSuccessStatusCode)
            {
                deletedCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
                return true;
            }
            return false;
        }
    }
}

// Microsoft. Call a Web API From a .NET Client (C#)
// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
// Geraadpleegd op 29 maart 2019