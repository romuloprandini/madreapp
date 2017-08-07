using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MadreApp.Helpers
{
    public class HttpRequest
    {
        public const string BASEURL = "http://104.154.65.154/api";
        private static string _token = "";
        private readonly HttpClient _client;

        private HttpRequest()
        {
            _client = new HttpClient() { MaxResponseContentBufferSize = 256000, Timeout = new TimeSpan(0, 5, 0) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static HttpRequest _instance;
        public static HttpRequest Instance => _instance ?? (_instance = new HttpRequest());
        
        protected async Task<string> ProcessResponse(HttpResponseMessage response)
        {
            try
            {
                var contentString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return contentString;
                
                //var obj = JsonConvert.DeserializeObject(contentString);
                //throw new ValidationException(obj); ????
                throw new Exception(contentString);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Request - " + ex?.Message);
            }
        }

        public static string Token
        {
            set { _token = value; }
            get { return _token; }
        }

        public async Task<List<T>> GetRequest<T>(string relativeUri) where T : class, new()
        {
            var uri = BASEURL + relativeUri;
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.GetAsync(uri);
            var content = await ProcessResponse(response);
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public async Task<T> GetOneRequest<T>(string relativeUri) where T : new()
        {
            var uri = BASEURL + relativeUri;
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.GetAsync(uri);
            var content = await ProcessResponse(response);
            var item = JsonConvert.DeserializeObject<T>(content);
            return item;
        }

        public async Task<T> PostRequest<T>(string relativeUri, object item = null) where T : class, new()
        {
            var uri = BASEURL + relativeUri;
            var json = "";
            if (item != null)
            {
                json = JsonConvert.SerializeObject(item);
            }
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.PostAsync(uri, stringContent);
            var content = await ProcessResponse(response);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> PutRequest<T>(string relativeUri, object item) where T : class, new()
        {
            var uri = BASEURL + relativeUri;
            var json = JsonConvert.SerializeObject(item);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.PutAsync(uri, stringContent);
            var content = await ProcessResponse(response);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<IList<T>> PostRequest<T>(string relativeUri, IList<object> items) where T : class, new()
        {
            var uri = BASEURL + relativeUri;
            var json = JsonConvert.SerializeObject(new { data = items });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.PostAsync(uri, stringContent);
            var content = await ProcessResponse(response);
            return JsonConvert.DeserializeObject<IList<T>>(content);
        }

        public async Task<T> NewFileRequest<T>(string relativeUri, byte[] file, string fileName, Dictionary<string, string> fields = null, string id = null)
        {
            var uri = BASEURL + relativeUri;
            var multipartFormDataContent = new MultipartFormDataContent();
            var streamContent = new StreamContent(new MemoryStream(file));
            streamContent.Headers.ContentLength = file.Length;
            multipartFormDataContent.Add(streamContent, "file", fileName);
            if (fields != null)
            {
                foreach (var key in fields.Keys)
                {
                    var value = string.Empty;
                    if (fields[key] != null)
                    {
                        value = fields[key];
                    }
                    multipartFormDataContent.Add(new StringContent(value), key);
                }
            }

            if (!(string.IsNullOrEmpty(id) || id.Equals("0")))
            {
                uri += "/" + id;
                multipartFormDataContent.Add(new StringContent("PUT"), "_method");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = multipartFormDataContent };
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.SendAsync(request);
            var content = await ProcessResponse(response);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<bool> DeleteRequest(string relativeUri)
        {
            var uri = BASEURL + relativeUri;

            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }

        public async Task<int> CountRequest(string relativeUri)
        {
            var uri = BASEURL + relativeUri;

            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _token);
            var response = await _client.GetAsync(uri);
            var content = await ProcessResponse(response);
            var def = new { count = 0 };
            var result = JsonConvert.DeserializeAnonymousType(content, def);
            return result.count;

        }

        public void CancelRequests()
        {
            _client?.CancelPendingRequests();
        }
    }
}
