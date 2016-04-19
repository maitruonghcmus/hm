using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using HM.DataModels;

namespace HM.WebApp
{
    public class HttpClientHelper<T>
    {
        private const string uri = "http://localhost:3333";

        public Result<T> CallGetMethod(string controller, string action)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync($"api/{controller}/{action}").Result;

                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<Result<T>>().Result;

                return result;
            }
        }

        public Result<T> CallGetMethod(string controller, string action, int id)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync($"api/{controller}/{action}/{id}").Result;

                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<Result<T>>().Result;

                return result;
            }
        }

        public Result<T> CallPostMethod(string controller, string action, T obj)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync($"api/{controller}/{action}", obj).Result;

                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<Result<T>>().Result;

                return result;
            }
        }

        public Result<T> CallPostMethod(string controller, string action, int id)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync($"api/{controller}/{action}", id).Result;

                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<Result<T>>().Result;

                return result;
            }
        }
    }
}