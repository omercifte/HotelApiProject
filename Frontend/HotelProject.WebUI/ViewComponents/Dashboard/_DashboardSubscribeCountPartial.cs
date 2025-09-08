using HotelProject.WebUI.Dtos.FollowersDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HotelProject.WebUI.ViewComponents.Dashboard
{
    //public class _DashboardSubscribeCountPartial : ViewComponent
    //{
    //    public async Task<IViewComponentResult> InvokeAsync()
    //    {
    //        var client = new HttpClient();
    //        var request = new HttpRequestMessage
    //        {
    //            Method = HttpMethod.Get,
    //            RequestUri = new Uri("https://instagram-profile1.p.rapidapi.com/getprofile/didemgeziyor"),
    //            Headers =
    //{
    //    { "X-RapidAPI-Key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" },
    //    { "X-RapidAPI-Host", "instagram-profile1.p.rapidapi.com" },
    //},
    //        };
    //        using (var response = await client.SendAsync(request))
    //        {
    //            response.EnsureSuccessStatusCode();
    //            var body = await response.Content.ReadAsStringAsync();
    //            ResultInstagramFollowersDto resultInstagramFollowersDtos = JsonConvert.DeserializeObject<ResultInstagramFollowersDto>(body);
    //            ViewBag.instagram1 = resultInstagramFollowersDtos.followers;
    //            ViewBag.instagram2 = resultInstagramFollowersDtos.following;

    //        }


    //        var client2 = new HttpClient();
    //        var request2 = new HttpRequestMessage
    //        {
    //            Method = HttpMethod.Get,
    //            RequestUri = new Uri("https://twitter-followers.p.rapidapi.com/omercifte/profile"),
    //            Headers =
    //{
    //    { "x-rapidapi-key", "da0ec2c8c0msh4a5c6cf4b356cd9p1337c7jsnfb4eb0eac6b6" },
    //    { "x-rapidapi-host", "twitter-followers.p.rapidapi.com" },
    //},
    //        };
    //        using (var response2 = await client2.SendAsync(request2))
    //        {
    //            response2.EnsureSuccessStatusCode();
    //            var body2 = await response2.Content.ReadAsStringAsync();
    //            ResultTwitterFollowersDto resultTwittterFollowersDto = JsonConvert.DeserializeObject<ResultTwitterFollowersDto>(body2);
    //            ViewBag.x1 = resultTwittterFollowersDto.friends;
    //            ViewBag.x2 = resultTwittterFollowersDto.sub_count;
    //        }

            
    //        var client3 = new HttpClient();
    //        var request3 = new HttpRequestMessage
    //        {
    //            Method = HttpMethod.Get,
    //            RequestUri = new Uri("https://linkedin-scraper-api-real-time-fast-affordable.p.rapidapi.com/profile/detail?username=%C3%B6mer-%C3%A7ifte-aa3ba7240"),
    //            Headers =
    //{
    //    { "x-rapidapi-key", "da0ec2c8c0msh4a5c6cf4b356cd9p1337c7jsnfb4eb0eac6b6" },
    //    { "x-rapidapi-host", "linkedin-scraper-api-real-time-fast-affordable.p.rapidapi.com" },
    //},
    //        };
    //        using (var response3 = await client3.SendAsync(request3))
    //        {
    //            response3.EnsureSuccessStatusCode();
    //            var body3 = await response3.Content.ReadAsStringAsync();
    //            ResultLinkedlnFollowersDto resultLinkedlnFollowersDto = JsonConvert.DeserializeObject<ResultLinkedlnFollowersDto>(body3);
    //            ViewBag.l1 = resultLinkedlnFollowersDto.data?.basic_info?.connection_count ?? 0;
    //            ViewBag.l2 = resultLinkedlnFollowersDto.data?.basic_info?.follower_count ?? 0;

    //        }



    //        return View();
    //    }
    //}

    public class _DashboardSubscribeCountPartial : ViewComponent
    {
        private readonly IMemoryCache _cache;

        public _DashboardSubscribeCountPartial(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Instagram
            var instagramCacheKey = "instagram_followers";
            if (!_cache.TryGetValue(instagramCacheKey, out ResultInstagramFollowersDto instagramData))
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://instagram-profile1.p.rapidapi.com/getprofile/didemgeziyor"),
                        Headers =
                        {
                            { "X-RapidAPI-Key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" },
                            { "X-RapidAPI-Host", "instagram-profile1.p.rapidapi.com" },
                        },
                    };
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    instagramData = JsonConvert.DeserializeObject<ResultInstagramFollowersDto>(body);

                    // Cache 5 dakika sakla
                    _cache.Set(instagramCacheKey, instagramData, TimeSpan.FromMinutes(5));
                }
                catch
                {
                    instagramData = new ResultInstagramFollowersDto { followers = 0, following = 0 };
                }
            }

            ViewBag.instagram1 = instagramData.followers;
            ViewBag.instagram2 = instagramData.following;

            // Twitter
            var twitterCacheKey = "twitter_followers";
            if (!_cache.TryGetValue(twitterCacheKey, out ResultTwitterFollowersDto twitterData))
            {
                try
                {
                    var client2 = new HttpClient();
                    var request2 = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://twitter-followers.p.rapidapi.com/omercifte/profile"),
                        Headers =
                        {
                            { "x-rapidapi-key", "da0ec2c8c0msh4a5c6cf4b356cd9p1337c7jsnfb4eb0eac6b6" },
                            { "x-rapidapi-host", "twitter-followers.p.rapidapi.com" },
                        },
                    };
                    using var response2 = await client2.SendAsync(request2);
                    response2.EnsureSuccessStatusCode();
                    var body2 = await response2.Content.ReadAsStringAsync();
                    twitterData = JsonConvert.DeserializeObject<ResultTwitterFollowersDto>(body2);

                    _cache.Set(twitterCacheKey, twitterData, TimeSpan.FromMinutes(5));
                }
                catch
                {
                    twitterData = new ResultTwitterFollowersDto { friends = 0, sub_count = 0 };
                }
            }

            ViewBag.x1 = twitterData.friends;
            ViewBag.x2 = twitterData.sub_count;

            // LinkedIn
            var linkedinCacheKey = "linkedin_followers";
            if (!_cache.TryGetValue(linkedinCacheKey, out ResultLinkedlnFollowersDto linkedinData))
            {
                try
                {
                    var client3 = new HttpClient();
                    var request3 = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://linkedin-scraper-api-real-time-fast-affordable.p.rapidapi.com/profile/detail?username=%C3%B6mer-%C3%A7ifte-aa3ba7240"),
                        Headers =
                        {
                            { "x-rapidapi-key", "da0ec2c8c0msh4a5c6cf4b356cd9p1337c7jsnfb4eb0eac6b6" },
                            { "x-rapidapi-host", "linkedin-scraper-api-real-time-fast-affordable.p.rapidapi.com" },
                        },
                    };
                    using var response3 = await client3.SendAsync(request3);
                    response3.EnsureSuccessStatusCode();
                    var body3 = await response3.Content.ReadAsStringAsync();
                    linkedinData = JsonConvert.DeserializeObject<ResultLinkedlnFollowersDto>(body3);

                    _cache.Set(linkedinCacheKey, linkedinData, TimeSpan.FromMinutes(5));
                }
                catch
                {
                    linkedinData = new ResultLinkedlnFollowersDto { data = new() { basic_info = new() { connection_count = 0, follower_count = 0 } } };
                }
            }

            ViewBag.l1 = linkedinData.data?.basic_info?.connection_count ?? 0;
            ViewBag.l2 = linkedinData.data?.basic_info?.follower_count ?? 0;

            return View();
        }
    }
}


