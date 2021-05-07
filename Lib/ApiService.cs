using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ApiService
    {
        public static async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    NLogLogger.Info(string.Format("Input {0}: ", uri));
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    // httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Partner-Key", partnerCode); 

                    var response = await httpClient.GetAsync(uri).ConfigureAwait(false);
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(content)) throw new Exception();

                    NLogLogger.Info("Output: " + content);
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (TaskCanceledException ex)
            {
                NLogLogger.Exception(ex);
                return default(T);
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return default(T);
            }
        }
    }
}
