using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Newtonsoft.Json;
using System.Linq;

namespace WebApi.Controllers
{

    // http://www.omdbapi.com/?apikey=ce974a95&s=star+trek

    [ApiController]
    [Route("Movie")]
    // public class MovieController : Controller
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        // need to create factory,
        // the instance is open until the app is closed.
        // private readonly HttpClient _httpClient;

        public MovieController(ILogger<MovieController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            // _httpClient = new HttpClient();
        }

        #region HttpGets return json as string

        [HttpGet]
        //[System.Web.Mvc.ValidateInput(false)]
        [Route("GetAllMovies/{SearchText}")]
        public async Task<string> GetAllMovies(string SearchText)
        {

            // var URL = $"http://www.omdbapi.com/?apikey=ce974a95&s={SearchText}";
            var URL = $"?apikey=ce974a95&s={SearchText}";

            // var httpClient = new HttpClient();

            var httpClient = _httpClientFactory.CreateClient("Movie");

            var response = httpClient.GetAsync(URL).Result;

            // response.Content.ReadAsStringAsync().Wait();

            return await response.Content.ReadAsStringAsync();
        }

        [HttpGet]
        [Route("GetMovie/{id}")]
        public async Task<string> GetMovie(string id)
        {

            var URL = $"?apikey=ce974a95&i={id}";

            // var httpClient = new HttpClient();

            var httpClient = _httpClientFactory.CreateClient("Movie");

            var response = httpClient.GetAsync(URL).Result;

            // response.Content.ReadAsStringAsync().Wait();

            return await response.Content.ReadAsStringAsync();
        }

        #endregion HttpGets return json as string

        #region HttpGets return json as List<T>

        [HttpGet]
        [Route("GetAll/{SearchText}")]
        public async Task<List<Movie>> GetAll(string SearchText)
        {

            List<Movie> movies = new List<Movie>();

            Root root = new Root();

            // var URL = $"http://www.omdbapi.com/?apikey=ce974a95&s={SearchText}";
            var URL = $"?apikey=ce974a95&s={SearchText}";

            // var httpClient = new HttpClient();

            var httpClient = _httpClientFactory.CreateClient("Movie");

            var response = httpClient.GetAsync(URL).Result;

            string data = await response.Content.ReadAsStringAsync();
            // movies = await response.Content.ReadFromJsonAsync<Movie[]>();


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            root = JsonConvert.DeserializeObject<Root>(data);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            movies = root.Search;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            // return JsonConvert.SerializeObject(movies);
#pragma warning disable CS8603 // Possible null reference return.
            return movies;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpGet]
        [Route("GetMovieById/{id}")]
        public async Task<MovieDetails> GetMovieById(string id)
        {

            MovieDetails movieDetails = new MovieDetails();

            var URL = $"?apikey=ce974a95&i={id}";
            var httpClient = _httpClientFactory.CreateClient("Movie");
            var response = httpClient.GetAsync(URL).Result;

            string data = await response.Content.ReadAsStringAsync();



#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            movieDetails = JsonConvert.DeserializeObject<MovieDetails>(data);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            // return JsonConvert.SerializeObject(movies);
#pragma warning disable CS8603 // Possible null reference return.
            return movieDetails;
#pragma warning restore CS8603 // Possible null reference return.



        }

        #endregion HttpGets return json as List<T>

    }
}
