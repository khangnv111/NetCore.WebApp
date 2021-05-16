using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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

        public static async Task<T> PostAsync<T>(string uri, dynamic data, string accessToken = null, bool isAuthen = false)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string data_str = JsonConvert.SerializeObject(data);
                    NLogLogger.Info(string.Format("Đầu vào {0}: {1}", uri, data_str));

                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Partner-Key", partnerCode);
                    if (isAuthen)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", accessToken);

                    var result = await client.PostAsync(uri, content).ConfigureAwait(false);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(resultContent)) return default(T);

                    NLogLogger.Info("Đầu ra: " + resultContent);

                    return JsonConvert.DeserializeObject<T>(resultContent);
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

        public static async Task<T> PostAsyncWithFile<T>(string uri, string jsonData, IFormFile fileData)
        {
            try
            {
                NLogLogger.Info(string.Format("Đầu vào {0}: {1}", uri, jsonData));
                // var partnerCode = ConfigurationManager.AppSettings["Partner-Key"].ToString();
                using (var cts = new CancellationTokenSource())
                {
                    using (var client = new HttpClient())
                    {
                        using (var content = new MultipartFormDataContent())
                        {
                            byte[] data;

                            using (var br = new BinaryReader(fileData.OpenReadStream()))
                                data = br.ReadBytes((int)fileData.OpenReadStream().Length);

                            ByteArrayContent bytes = new ByteArrayContent(data);

                            content.Add(bytes, "fileUpload", fileData.FileName);
                            content.Add(new StringContent(jsonData), "jsonData");
                            //content.Add(new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json"), "jsonData");
                            using (
                               var result = await client.PostAsync(uri, content, cts.Token).ConfigureAwait(false))
                            {
                                string resultContent = await result.Content.ReadAsStringAsync();
                                if (string.IsNullOrWhiteSpace(resultContent)) return default(T);
                                NLogLogger.Info("Đầu ra: " + resultContent);

                                return JsonConvert.DeserializeObject<T>(resultContent);
                            }
                        }
                    }
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
