using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Web.Infrastructure
{
    public static class Countries
    {

        static readonly HttpClient client = new HttpClient();

        public static async Task<bool> IsExisted(string countryName)
        {
            try	
            {
                HttpResponseMessage response = await client.GetAsync($"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return true;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
            return false;
        }
    }
}