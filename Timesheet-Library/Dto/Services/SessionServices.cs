using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet_Library.Dto.Services
{
    public class SessionServices
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

        public static async Task<string> CreateSessionAsync(UserToLoginDto user)
        {
            GetClient();
            string session = "";
            HttpResponseMessage response = await client.PostAsJsonAsync("api/session/", user);
            if (response.IsSuccessStatusCode)
            {
                session = await response.Content.ReadAsStringAsync();
            }
            return session;
        }

        public static string CreateSession(UserToLoginDto user)
        {
            GetClient();
            string session = "";
            HttpResponseMessage response = client.PostAsJsonAsync("api/session/", user).Result;
            if (response.IsSuccessStatusCode)
            {
                session = response.Content.ReadAsStringAsync().Result;
            }
            return session;
        }

        public static async Task<string> DeleteSessionAsync()
        {
            GetClient();
            string session = "";
            HttpResponseMessage response = await client.DeleteAsync("api/session/");
            if (response.IsSuccessStatusCode)
            {
                session = await response.Content.ReadAsStringAsync();
            }
            return session;
        }
    }
}
