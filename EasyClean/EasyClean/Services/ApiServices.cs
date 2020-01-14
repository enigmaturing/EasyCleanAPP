using EasyClean.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EasyClean.Services
{
    class ApiServices
    {

        // 1) TO REGISTER a user so that he can use the API:
        // Make a POST request to the endpoint: http://easyclean.azurewebsites.net/api/Account/Register with the following: 
        // In the HEADER:
        // + ContentType (because we want to send a JSON in the body of this http call)
        // In the BODY:
        // + JSON (because need to send the data of the endClient and will do it in json form with email, password and confirmpassword)
        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            // Create an instance of registerModel
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            // Make http POST request
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);  // serialize the object we are sending along with the POST request
            var content = new StringContent(json, Encoding.UTF8, "application/json");  // pack the serialized object into the content of the POST request
            var response = await httpClient.PostAsync("http://easyclean.azurewebsites.net/api/Account/Register", content);

            return response.IsSuccessStatusCode;
        }

        // 2) TO LOG IN a user so that a TOKEN is generated:
        // Make a POST request to the endpoint: http://easyclean.azurewebsites.net/Token with the following: 
        // In the HEADER:
        // + Three key-value pairs with: username, password and grant_type of the user that wants to log in
        public async Task<bool> LoginUser(string email, string password)
        {
            var keyvalues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            // Make http POST request
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://easyclean.azurewebsites.net/Token");
            request.Content = new FormUrlEncodedContent(keyvalues); // pack the email, password and grant_type in the body as urlencoded and not as a json (that is why this method is different to RegisterUser)
            var response = await httpClient.SendAsync(request);

            // Once the post request has been made, read the response from the server to get the access token
            var content = response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        // 3) TO REGISTER an EndClient in the DB:
        // Make a POST request to the endpoint: http://easyclean.azurewebsites.net/api/EndClients with the following: 
        // In the HEADER:
        // + Authorization (because endpoint api/EndClients is secured with [Authorized] in API)
        // + ContentType (because we want to send a JSON in the body of this http call)
        // In the BODY:
        // + JSON (because need to send the data of the endClient and will do it in json form)
        public async Task<bool> RegisterEndClient(EndClient endClient)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "TODO: add the token later on here");
            var json = JsonConvert.SerializeObject(endClient);  // serialize the object we are sending along with the POST request
            var content = new StringContent(json, Encoding.UTF8, "application/json");  // pack the serialized object into the content of the POST request
            var response = await httpClient.PostAsync("http://easyclean.azurewebsites.net/api/EndClients", content);

            return response.IsSuccessStatusCode;
        }

        // 4) TO LIST all EndClients present in the DB (the API returns the users already ordered by birthdate, becase we implemented the method in the api like that):
        // GET request to the endpoint: http://easyclean.azurewebsites.net/api/EndClients and the following: 
        // In the HEADER must be attached:
        // + Authorization (because endpoint api/EndClients is secured with [Authorized] in API)
        public async Task<List<EndClient>> GetEndClientsOrderedByBirthdate()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "TODO: add the token later on here");
            var json = await httpClient.GetStringAsync("http://easyclean.azurewebsites.net/api/EndClients");

            return JsonConvert.DeserializeObject<List<EndClient>>(json);
        }


        // 5) TO LIST all EndClients present in the DB:
        // GET request to the endpoint: http://easyclean.azurewebsites.net/api/EndClients?userSurname=Gonzalez
        // Pass "userSurname" as Query-Parameter, like specified oven
        // In the HEADER must be attached:
        // + Authorization (because endpoint api/EndClients is secured with [Authorized] in API)
        public async Task<List<EndClient>> GetEndClientsWithSurname(string surname)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "TODO: add the token later on here");
            var json = await httpClient.GetStringAsync($"http://easyclean.azurewebsites.net/api/EndClients?userSurname={surname}");

            return JsonConvert.DeserializeObject<List<EndClient>>(json);
        }
    }
}
