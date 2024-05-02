using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLMChat.MVVM.Model
{
    class LLM
    {
        public async Task<string> PostAPIAsync(string message, string model)
        {
            try
            {
                var request = new RestRequest("/chat/completions", Method.Post);
                request.AddJsonBody(new
                {
                    model = model,
                    messages = new List<object>
                    {
                        new { role = "system", content = "Always answer in Spanish." },
                        new { role = "user", content = message }
                    },
                    temperature = 0.7
                });

                var response = await RestClientInstance.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    dynamic data = JsonConvert.DeserializeObject(response.Content);
                    string result = data.choices[0].message.content;
                    return result;
                }
                else
                {
                    throw new Exception("Error al hacer la solicitud: " + response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al hacer la solicitud: " + e.Message);
            }
        }

        private static string _baseUrl = "http://localhost:1234/v1";
        public static string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                _baseUrl = value;
                _restClientInstance = new RestClient(_baseUrl);
            }
        }

        private static RestClient _restClientInstance;
        private static readonly object _lock = new object();
        private static RestClient RestClientInstance
        {
            get
            {
                if (_restClientInstance == null)
                {
                    lock (_lock)
                    {
                        if (_restClientInstance == null)
                        {
                            _restClientInstance = new RestClient(_baseUrl);
                        }
                    }
                }
                return _restClientInstance;
            }
        }
    }
}
