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
    public class LogServices
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

        public async Task<List<LogDto>> GetAllLogsAsync()
        {
            GetClient();
            string getAllLogs = null;
            List<LogDto> LogList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Logs/");
            if (response.IsSuccessStatusCode)
            {
                getAllLogs = await response.Content.ReadAsStringAsync();
                LogList = JsonConvert.DeserializeObject<List<LogDto>>(getAllLogs);
            }
            return LogList;
        }

        public async Task<LogDto> CreateLogAsync(LogToCreateDto log)
        {
            GetClient();
            LogDto createdLog = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Log/", log);
            if (response.IsSuccessStatusCode)
            {
                createdLog = await response.Content.ReadAsAsync<LogDto>();
            }
            return createdLog;
        }

        public async Task<LogDto> GetLogByIdAsync(int id)
        {
            GetClient();
            LogDto getLog = null;
            HttpResponseMessage response = await client.GetAsync($"api/Log/{id}");
            if (response.IsSuccessStatusCode)
            {
                getLog = await response.Content.ReadAsAsync<LogDto>();
            }
            return getLog;
        }

        public async Task<LogDto> UpdateLogByIdAsync(LogToUpdateDto log, int id)
        {
            GetClient();
            LogDto updatedLog = null;
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Log/{id}", log);
            if (response.IsSuccessStatusCode)
            {
                updatedLog = await response.Content.ReadAsAsync<LogDto>();
            }
            return updatedLog;
        }

        public async Task<bool> DeleteLogByIdAsync(int id)
        {
            GetClient();
            LogDto deletedLog = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Log/{id}");
            if (response.IsSuccessStatusCode)
            {
                deletedLog = await response.Content.ReadAsAsync<LogDto>();
                return true;
            }
            return false;
        }
    }
}