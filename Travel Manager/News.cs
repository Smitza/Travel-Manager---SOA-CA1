using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Travel_Manager
{
    public class News
    {
        static readonly string NEWS_URL = "https://newsapi.org/v2/top-headlines";
        static string API_KEY = "e82c5c5f920247d8b31b3eb9f5279e8f";

        public string GetNewsApiUrl(string country)
        {
            var client = new RestClient(NEWS_URL);
            var request = new RestRequest();

            // Ensure the correct format for URL parameters
            request.AddParameter("country", country);
            request.AddParameter("apiKey", API_KEY);

            // Return the correctly formatted URL
            return client.BuildUri(request).ToString();
        }

        public async Task<List<Article>> NewsResult(string country)
        {
            var client = new RestClient(NEWS_URL);
            var request = new RestRequest();

            request.Method = Method.Get;

            request.AddParameter("country", country);
            request.AddParameter("apiKey", API_KEY);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var articlesResult = JsonSerializer.Deserialize<Rootobject>(response.Content);
                return articlesResult?.articles?.ToList() ?? new List<Article>();
            }
            else
            {
                return new List<Article>
                {
                    new Article
                    {
                        title = "Error",
                        description = $"Failed to fetch news: {response.StatusDescription}"
                    }
                };
            }
        }
    }

}
