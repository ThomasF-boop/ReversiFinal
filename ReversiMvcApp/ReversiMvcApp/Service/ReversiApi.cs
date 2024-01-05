using Newtonsoft.Json;
using ReversiMvcApp.Models;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

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

        public async Task<bool> MaakNieuwSpel(Spel spel)
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
            var post = await httpClient.PostAsync(queryParameters,null);

            return true;
        }

        public async Task<Spel> GetSpelByToken(string spelToken)
        {
            var response = await httpClient.GetAsync(spelToken);
            Console.WriteLine(response.RequestMessage);
            response.EnsureSuccessStatusCode();

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
    }
}
