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
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMiIsImVtYWlsIjoibWljaGFlbEBnbWFpbC5jb20iLCJuYmYiOjE1NTQxMzYzMTQsImV4cCI6MTU1NDIyMjcxNCwiaWF0IjoxNTU0MTM2MzE0fQ.5LPuMM1l_z8ToFLqw0X-kdl8Xy1dCDbjWtd_l_sDvaKC6YiXnQsZB2sff7HaPxSsGK6u5sLL49-4n6_CEm8J9A");
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
