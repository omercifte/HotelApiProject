using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RapidApiConsume.Models;

namespace RapidApiConsume.Controllers
{
    public class ImdbController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ApiMovieViewModel> apiMovieViewModels = new List<ApiMovieViewModel>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://movies-tv-shows-database.p.rapidapi.com/?year=2020&page=1"),
                Headers =
                {
                    { "x-rapidapi-key", "da0ec2c8c0msh4a5c6cf4b356cd9p1337c7jsnfb4eb0eac6b6" },
                    { "x-rapidapi-host", "movies-tv-shows-database.p.rapidapi.com" },
                    { "Type", "get-shows-byyear" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(body);
                apiMovieViewModels = json["tv_results"]?.ToObject<List<ApiMovieViewModel>>() ?? new List<ApiMovieViewModel>();

                return View(apiMovieViewModels);
            }
        }
    }
}
