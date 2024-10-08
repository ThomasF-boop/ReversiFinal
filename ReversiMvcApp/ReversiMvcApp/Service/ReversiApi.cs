using Newtonsoft.Json;
using ReversiMvcApp.Models;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace ReversiMvcApp.Service
{
    public class ReversiApi
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public ReversiApi(IConfiguration configuration)
        {
            this.httpClient = new HttpClient();
            this.configuration = configuration;
            httpClient.BaseAddress = new Uri("https://localhost:7020/api/Spel/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        public async Task<List<Spel>> GetSpellenMetWachtendeSpeler()
        {
            var response = await httpClient.GetAsync(string.Empty);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var temp = JsonConvert.DeserializeObject<List<Spel>>(content);
            return temp;
        }

        public async Task<List<Spel>> GetAllSpellen()
        {
            var response = await httpClient.GetAsync("all");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var spellen = JsonConvert.DeserializeObject<List<Spel>>(content);
            return spellen;
        }

        public async Task<string> MaakNieuwSpel(Spel spel)
        {
            var nieuwSpel = new
            {
                spelerToken = spel.Speler1Token,
                omschrijving = spel.Omschrijving
            };

            var queryParameters = $"?spelerToken={nieuwSpel.spelerToken}&omschrijving={Uri.EscapeDataString(nieuwSpel.omschrijving)}";

            var json = JsonConvert.SerializeObject(nieuwSpel);
            Console.WriteLine(json);
            var content = new StringContent(queryParameters,Encoding.UTF8,"application/json");
            Console.WriteLine(content);
              // Send the POST request
            var response = await httpClient.PostAsync(queryParameters, content);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + responseContent);

                // Optionally, you can deserialize the response if it's in JSON format
                // var result = JsonConvert.DeserializeObject<YourResponseType>(responseContent);
                var tokenMatch = Regex.Match(responseContent, @"speltoken:\s([a-f0-9\-]+)");

                if (tokenMatch.Success)
                {
                    // Return the extracted token
                    var token = tokenMatch.Groups[1].Value;
                    return token;
                }
                else
                {
                    Console.WriteLine("Token not found in the response.");
                    return null; // If no token is found
                }
                
            }
            else
            {
                // Handle the error response
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return null; // Indicate failure if the response was not successful
            }
        }

        public async Task<Spel> GetSpelByToken(string spelToken)
        {
            var response = await httpClient.GetAsync(spelToken);
            Console.WriteLine(response.RequestMessage);
            

            var content = await response.Content.ReadAsStringAsync();
            var temp = JsonConvert.DeserializeObject<Spel>(content);
            return temp;
        }
        public async Task<Spel> GetSpelBySpelerToken(string spelerToken)
        {
            var response = await httpClient.GetAsync(spelerToken);
            Console.WriteLine(response.RequestMessage);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var temp = JsonConvert.DeserializeObject<Spel>(content);
            return temp;
        }

        public async Task<bool> JoinSpel(string spelToken,string spelerToken)
        {
            var joinSpel = new
            {
                SpelToken = spelToken,
                SpelerToken = spelerToken,
            };

            var json = JsonConvert.SerializeObject(joinSpel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync("JoinSpel", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSpel(string spelToken, string spelerToken)
        {
            var gamePlayerToken = new GamePlayerToken
            {
                Token = spelToken,
                PlayerToken = spelerToken
            };

            var json = JsonConvert.SerializeObject(gamePlayerToken);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:7020/api/Spel/DeleteSpel"),
                Content = new StringContent(JsonConvert.SerializeObject(gamePlayerToken), Encoding.UTF8, "application/json")
            };

  
            var requestUri = "https://localhost:7020/api/Spel/DeleteSpel"; //link is correct

            var response = await httpClient.DeleteAsync("DeleteSpel"); //415

            var response2 = await httpClient.DeleteAsync(requestUri); //415

            var response3 = await httpClient.SendAsync(request); //400


            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteGame(string spelToken, string spelerToken)
        {
            var deleteSpel = new
            {
                spelToken = spelToken,
                spelerToken = spelerToken
            };

            // Serialize the object to JSON
            var json = JsonConvert.SerializeObject(deleteSpel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Create an HttpRequestMessage to send a DELETE request
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete, // Use DELETE method
                RequestUri = new Uri(httpClient.BaseAddress, "DeleteSpel"), // Ensure the endpoint is correct
                Content = content // Include the JSON body
            };

            // Send the DELETE request
            var response = await httpClient.SendAsync(request);

            // Return whether the operation was successful (status code 200-299)
            return response.IsSuccessStatusCode;
        }

        private async Task UpdatePlayerStats(string spelerToken)
        {
            // Implement your logic to update player stats here
            // Example: Retrieve player details and update their stats
        }

    }
}
