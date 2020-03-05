using MovieTestInLog.Abstractions;
using MovieTestInLog.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestInLog.Services
{
    public class HubServiceApi : IHubServiceApi
    {
        HttpClient client;

        public HubServiceApi()
        {
            client = new HttpClient();
        }
        public async Task<IEnumerable<MoviesModel>> GetMoviesAsync(int SelectedPage)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.ApplicationURL}movie/upcoming?api_key={Constants.ApiKey}&language=pt-BR&page={SelectedPage}");
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseData)["results"]?.ToObject<IEnumerable<MoviesModel>>();

            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }



        }
        public async Task<MoviesDetailModel> GetMoviesDetailAsync(int movie_id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.ApplicationURL}movie/{movie_id}?api_key={Constants.ApiKey}&language=pt-BR");
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseData)?.ToObject<MoviesDetailModel>();

            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }



        }
        public async Task<MovieSearchModel> GetSearchMovieAsync(string searchText, string page)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.ApplicationURL}search/movie?api_key={Constants.ApiKey}&language=pt-BR&page={page}&query={searchText}");
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseData)?.ToObject<MovieSearchModel>();

            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }



        }

    }
}
