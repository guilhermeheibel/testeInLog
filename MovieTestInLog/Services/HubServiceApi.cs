﻿using MovieTestInLog.Abstractions;
using MovieTestInLog.Models;
using MovieTestInLog.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            try
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
                    if (Application.Current.MainPage is MasterDetailPage master)
                    {
                        if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel autenticar sua chave. Verifiue o token API e tente novamente.", "OK"); }
                    }
                }
                else
                {
                    if (Application.Current.MainPage is MasterDetailPage master)
                    {
                        if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel buscar as informações no servidor. Verifique sua conexão e tente novamente.", "OK"); }
                    }

                }
            }
            catch (Exception ex)
            {
                if (Application.Current.MainPage is MasterDetailPage master)
                {
                    if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel buscar as informações no servidor. Verifique sua conexão e tente novamente.\n" + ex.Message, "OK"); }
                }
            }
            return null;

        }
        public async Task<MoviesDetailModel> GetMoviesDetailAsync(int movie_id)
        {
            try
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
                    if (Application.Current.MainPage is MasterDetailPage master)
                    {
                        if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel autenticar sua chave. Verifiue o token API e tente novamente.", "OK"); }
                    }
                }
                else
                {
                    if (Application.Current.MainPage is MasterDetailPage master)
                    {
                        if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel buscar as informações no servidor. Verifique sua conexão e tente novamente.", "OK"); }
                    }

                }
            }
            catch (Exception ex)
            {
                if (Application.Current.MainPage is MasterDetailPage master)
                {
                    if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel buscar as informações no servidor. Verifique sua conexão e tente novamente.\n" + ex.Message, "OK"); }
                }
            }
            return null;
        }
        public async Task<MovieSearchModel> GetSearchMovieAsync(string searchText, string page)
        {
            try
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
                    if (Application.Current.MainPage is MasterDetailPage master)
                    {
                        if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel autenticar sua chave. Verifiue o token API e tente novamente.", "OK"); }
                    }
                }
                else
                {
                    if (Application.Current.MainPage is MasterDetailPage master)
                    {
                        if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel buscar as informações no servidor. Verifique sua conexão e tente novamente.", "OK"); }
                    }

                }
            }
            catch (Exception ex)
            {
                if (Application.Current.MainPage is MasterDetailPage master)
                {
                    if (master.Detail.Navigation.NavigationStack.LastOrDefault() is BasePage ActualPage) { await ActualPage.DisplayAlert("Aviso", "Não foi possivel buscar as informações no servidor. Verifique sua conexão e tente novamente.\n" + ex.Message, "OK"); }
                }
            }
            return null;
        }

    }
}
