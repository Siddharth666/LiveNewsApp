using Microsoft.AspNetCore.Mvc;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using System.Net.Http.Headers;
using System.Reflection;
using NewsApp.Models;
using NewsApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAPIController : ControllerBase
    {
        // GET: api/<NewsAPIController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public async Task<IActionResult> GetNews(string query)
        {
            try
            {
                // init with your API key
                var newsApiClient = new NewsApiClient("4c3997835893492d8623a7004f2f7446");


                var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = query,
                    SortBy = SortBys.Popularity,
                    Language = Languages.EN,
                    From = new DateTime(2024, 01, 01)
                });
                if (articlesResponse.Status == Statuses.Ok)
                {
                    // total results found
                    Console.WriteLine(articlesResponse.TotalResults);
                    // here's the first 20
                    //News newsModel = new News();
                   List<News> newsList = new List<News>();
                    foreach (var article in articlesResponse.Articles)
                    {
                        News newsModel = new News();
                        newsModel.Name = article.Source.Name;
                        newsModel.Title = article.Title;
                        newsModel.Description = article.Description;
                        newsModel.Author = article.Author;
                        newsModel.Url = article.Url;

                        if (article.UrlToImage != null) { newsModel.UrlToImage = article.UrlToImage; }
                        
                        newsModel.Content= article.Content;
                        newsModel.PublishedAt = Convert.ToDateTime(article.PublishedAt);

                        newsList.Add(newsModel);

                    }
                    StoreData storeData = new StoreData();
                    var x = storeData.Store(newsList);
                }
                //  Console.ReadLine();
                return Ok(articlesResponse);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw;
            }
        }


        // GET api/<NewsAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NewsAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NewsAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NewsAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
