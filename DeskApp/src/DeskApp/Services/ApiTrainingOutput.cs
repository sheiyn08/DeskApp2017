using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DeskApp.DataLayer;
using Newtonsoft.Json;

namespace DeskApp.Services
{
    public static class ApiTrainingOutput
    {

        public static string url = @"https://ncddpdb.dswd.gov.ph";


        public static async Task<List<community_training>> GetTrainings(string username, string password, string city_code = null, Guid? record_id = null, bool? getPax = null)
        {



            string token = username + ":" + password;

            byte[] toBytes = Encoding.ASCII.GetBytes(token);


            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                // var model = new auth_messages();

                HttpResponseMessage response = client.GetAsync("api/offline/v1/trainings/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<community_training>>(responseJson.Result);

                    return model;
                }
                else
                {
                    return new List<community_training>();
                }
            }


        }


        public static async Task<List<person_training>>GetOnlineParticipants(string username, string password, Guid? community_training_id)
        {


            string token = username + ":" + password;

            byte[] toBytes = Encoding.ASCII.GetBytes(token);


            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                // var model = new auth_messages();
        
                HttpResponseMessage response = client.GetAsync("api/online/v1/training/participants/get_mapped?community_training_id=" + community_training_id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<person_training>>(responseJson.Result);


                    return model;
                }
                else
                {
                    return new List<person_training>();
                }
            }


        }

    }
}
