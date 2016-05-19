using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HM.WebApp
{
    public class HttpClientHelper
    {
        private string uri = ConfigurationManager.AppSettings["ApiUrl"] ?? "http://localhost:33333";

        public static HttpClientHelper Instance = new HttpClientHelper();

        public Result<T> Get<T>(string controller, string action)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                try
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(string.Format("api/{0}/{1}", controller, action) ).Result;

                    if (response.IsSuccessStatusCode)
                        result = response.Content.ReadAsAsync<Result<T>>().Result;
                    else
                        result.Code = response.RequestMessage.RequestUri + " - " + response.StatusCode.ToString();

                }
                catch (Exception ex)
                {
                    result.Code = ex.Message;
                }
                return result;
            }
        }

        public Result<T> Get<T>(string controller, string action, int id)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                try
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(string.Format("api/{0}/{1}/{2}", controller, action, id.ToString())).Result;

                    if (response.IsSuccessStatusCode)
                        result = response.Content.ReadAsAsync<Result<T>>().Result;
                    else
                        result.Code = response.RequestMessage.RequestUri + " - " + response.StatusCode.ToString();
                }
                catch (Exception ex)
                {
                    result.Code = ex.Message;
                }

                return result;
            }
        }

        public Result<T> Get<T>(string controller, string action, KeyValuePair<string, string> _param)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                try
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var requestParams = "?" + _param.Key + " = " + _param.Value;

                    var response = client.GetAsync(string.Format("api/{0}/{1}/{2}", controller,action, requestParams)).Result;

                    if (response.IsSuccessStatusCode)
                        result = response.Content.ReadAsAsync<Result<T>>().Result;
                }
                catch (Exception ex)
                {
                    result.Code = ex.Message;
                }

                return result;
            }
        }

        public Result<T> Get<T>(string controller, string action, List<KeyValuePair<string, string>> _params)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                try
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var requestParams = "?";
                    foreach (var p in _params)
                    {
                        requestParams += p.Key + "=" + p.Value + "&";
                    }
                    requestParams = requestParams.Substring(0, requestParams.Length - 1);

                    var response = client.GetAsync(string.Format("api/{0}/{1}/{2}",controller,action,requestParams)).Result;

                    if (response.IsSuccessStatusCode)
                        result = response.Content.ReadAsAsync<Result<T>>().Result;
                }
                catch (Exception ex)
                {
                    result.Code = ex.Message;
                }

                return result;
            }
        }

        public Result<T> Post<T>(string controller, string action, T obj)
        {
            using (var client = new HttpClient())
            {
                var result = new Result<T>();

                try
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsJsonAsync(string.Format("api/{0}/{1}",controller,action), obj).Result;

                    if (response.IsSuccessStatusCode)
                        result = response.Content.ReadAsAsync<Result<T>>().Result;
                    else
                        result.Code = response.RequestMessage.RequestUri + " - " + response.StatusCode.ToString();
                }
                catch (Exception ex)
                {
                    result.Code = ex.Message;
                }

                return result;
            }
        }

        public Result<T> Delete<T>(string controller, string action, int id)
        {
            var result = new Result<T>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.DeleteAsync(string.Format("api/{0}/{1}/{2}",controller,action,id.ToString())).Result;
                    if (response.IsSuccessStatusCode)
                        result = response.Content.ReadAsAsync<Result<T>>().Result;
                    else
                        result.Code = response.RequestMessage.RequestUri + " - " + response.StatusCode.ToString();
                }
                catch (Exception ex)
                {
                    result.Code = ex.Message;
                }
            }
            return result;
        }

        public T GetObjects<T>(string controller, string action)
        {
            return this.Get<T>(controller, action).Data;
        }

        public T GetObjectsByIndex<T>(string controller, string action, int indexKey)
        {
            return this.Get<T>(controller, action, indexKey).Data;
        }

        public T GetObject<T>(string controller, string action, int id)
        {
            return this.Get<T>(controller, action, id).Data;
        }
    }
}