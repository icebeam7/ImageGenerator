using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using ImageGenerator.Models;
using ImageGenerator.Helpers;
using System.Text.Json;

namespace ImageGenerator.Services
{
    public class OpenAIService
    {
        HttpClient client;

        public OpenAIService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.OpenAIServer);

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Constants.OpenAIKey);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GenerateImage(string text)
        {
            var request = new GenerationRequest()
            {
                prompt = text
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(
                json, Encoding.UTF8, "application/json");

            var openai = await client.PostAsync(
                Constants.OpenAIGenerationsEndpoint, content);

            if (openai.IsSuccessStatusCode)
            {
                var result = await openai.Content.ReadAsStringAsync();
                var images = JsonSerializer.Deserialize<GenerationResponse>(result);
                return images.data.FirstOrDefault().url;
            }

            return string.Empty;
        }
    }
}
