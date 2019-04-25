using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMiIsImVtYWlsIjoibWljaGFlbEBnbWFpbC5jb20iLCJuYmYiOjE1NTQyMjU1ODAsImV4cCI6MTU1NDMxMTk4MCwiaWF0IjoxNTU0MjI1NTgwfQ.Qe8R3CvFyQkZvu-bLdOT4yrrYjSEGK5aeVvmqMIPkpH-qlYGx2cBrcsoIJ4TL-KjrEuudWCIgOWDrD2364cq_w");
            }
        }

        public static async Task<List<CompanyDto>> GetAllCompaniesAsync()
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

        public static async Task<CompanyDto> GetCompanyByIdAsync(int id)
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

        public static async Task<CompanyDto> CreateCompanyAsync(CompanyToCreateDto company)
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

        public static async Task<CompanyDto> UpdateCompanyByIdAsync(CompanyToUpdateDto company, int id)
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

        public static async Task<CompanyDto> DeleteCompanyByIdAsync(int id)
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
    }
}
