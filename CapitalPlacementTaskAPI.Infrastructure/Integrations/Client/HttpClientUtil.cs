using Flurl;
using Flurl.Http;
using Flurl.Http.Xml;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Flurl.Http.Configuration;
using System.Security.Cryptography.X509Certificates;
using Flurl.Http.Content;
using System.Net;
using CapitalPlacementTaskAPI.Domain.Const;

namespace CapitalPlacementTaskAPI.Infrastructure.Integrations.Client
{
    public class HttpClientUtil
    {
        private readonly ILogger<HttpClientUtil> _logger;

        public HttpClientUtil(ILogger<HttpClientUtil> logger)
        {
            _logger = logger;
        }
        public async Task<T> GetJSON<T>(string path, object queryParams = null, object headers = null, object cookies = null)
        {
            try
            {
                var req = new Url(path)
                    .SetQueryParams(queryParams ?? new { })
                    .WithCookies(cookies ?? new { })
                    .WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .WithHeaders(headers ?? new { });
                var ret = await req.GetAsync().ReceiveJson<T>();

                return ret;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public async Task<FlurlResponse> GetJSONAsync(string path, object queryParams = null, object headers = null, object cookies = null)
        {
            try
            {
                return (FlurlResponse)await new Url(path)
                    .SetQueryParams(queryParams ?? new { })
                    .WithCookies(cookies ?? new { })
                    .WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .WithHeaders(headers ?? new { })
                    .GetAsync();
            }
            catch (TaskCanceledException)
            {
                return default;
            }
        }

        public async Task<string> GetString(string path, object queryParams = null, object headers = null,
            object cookies = null)
        {
            try
            {
                return await new Url(path)
                    .SetQueryParams(queryParams ?? new { })
                    .WithCookies(cookies ?? new { })
                    .WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .WithHeaders(headers ?? new { })
                    .GetStringAsync();
            }
            catch (TaskCanceledException)
            {
                return string.Empty;
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }

        public async Task<T> PostJSONAsync<T>(string path, object payload = null,
            int timeout = Configs.REST_REQUEST_TIMEOUT,
            object headers = null, object cookies = null)
        {
            try
            {
                var result = await new Url(path)
                    .WithTimeout(timeout)
                    .WithCookies(cookies ?? new { })
                    .WithHeaders(headers ?? new { })
                    .PostJsonAsync(payload ?? new object()).ReceiveJson<T>();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                _logger.LogError($"Error message: {(error == "" ? ex.Message : error)}");
                _logger.LogInformation($"Error happened while trying to process request with endpoint: {ex.Call.Request.Url} and payload: {ex.Call.RequestBody}");

                T obj = (T)Activator.CreateInstance(typeof(T));
                if (error == "")
                {
                    var customError = new
                    {
                        Successful = false,
                        StatusMessage = ex.Message,
                        StatusCode = "UNKNOWN"
                    };
                    return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(customError));
                }
                return JsonConvert.DeserializeObject<T>(error);
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }
        public async Task<T> PostJsonAsyncWithMasterCardTaskCert<T>(string path, object payload = null,
            int timeout = Configs.REST_REQUEST_TIMEOUT,
            object headers = null, object cookies = null, string httpsCertificate = "", string certificatePassword = "")
        {
            var privateBytes = Convert.FromBase64String(httpsCertificate);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#if DEBUG
            var cert = new X509Certificate2(Path.Combine("data/certs", "123456equitybankClientCustTest_cert_out.pfx"), "1234pass");
#else
            var cert = new X509Certificate2(privateBytes, string.Empty, X509KeyStorageFlags.MachineKeySet); 
#endif

            //_logger.LogError($"Cerificate used: {JsonConvert.SerializeObject(cert)}");


            try
            {
                FlurlHttp.Configure(settings => settings.HttpClientFactory = new X509HttpFactory(cert));
                var result = await new Url(path)
                    .WithTimeout(timeout)
                    .WithCookies(cookies ?? new { })
                                   .WithHeaders(headers ?? new { })
                                    .PostJsonAsync(payload ?? new object()).ReceiveJson<T>();

                return result;

            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                _logger.LogError($"Error message: {error}");
                _logger.LogInformation($"Error happened while trying to process request with endpoint: {ex.Call.Request.Url} and payload: {ex.Call.RequestBody}");

                T obj = (T)Activator.CreateInstance(typeof(T));
                return JsonConvert.DeserializeObject<T>(error);
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }
        public async Task<string> PostXMLStringAsync(string path, string payload = null,
            object headers = null, int timeout = Configs.REST_REQUEST_TIMEOUT, object cookies = null)
        {
            try
            {
                var content = new CapturedStringContent(payload, "application/xml");
                var result = await new Url(path)
                    .WithTimeout(timeout)
                    .WithCookies(cookies ?? new { })
                                   .WithHeaders(headers ?? new { })
                                    .PostAsync(content).ReceiveString();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError($"Error message: {ex.Message}");
                //var response = ex.GetResponseJson<T>();
                return default;
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<Stream> PostPDFStringAsync(string path, object payload = null,
            int timeout = Configs.REST_REQUEST_TIMEOUT,
            object headers = null, object cookies = null)
        {
            try
            {
                var result = await new Url(path)
                    .WithTimeout(timeout)
                    .WithCookies(cookies ?? new { })
                    .WithHeaders(headers ?? new { })
                    .PostJsonAsync(payload ?? new object()).ReceiveStream();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                _logger.LogError($"Error message: {error}");
                _logger.LogInformation($"Error happened while trying to process request with endpoint: {ex.Call.Request.Url} and payload: {ex.Call.RequestBody}");

                var response = ex.GetResponseJsonAsync();
                return default;
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<string> PostPDFStringAsync(string path, string payload = null,
            object headers = null, int timeout = Configs.REST_REQUEST_TIMEOUT, object cookies = null)
        {
            try
            {
                var content = new CapturedStringContent(payload, "application/pdf");
                var result = await new Url(path)
                    .WithTimeout(timeout)
                    .WithCookies(cookies ?? new { })
                                   .WithHeaders(headers ?? new { })
                                    .PostAsync(content).ReceiveString();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError($"FlurlHttpException message: {ex.Message}");
                //var response = ex.GetResponseJson<T>();
                return default;
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<T> PostXMLStringForObjectAsync<T>(string path, string payload = null,
    object headers = null, int timeout = Configs.REST_REQUEST_TIMEOUT, object cookies = null)
        {
            try
            {
                var content = new CapturedStringContent(payload, "application/xml");
                var result = await new Url(path)
                    .WithTimeout(timeout)
                    .WithCookies(cookies ?? new { })
                                   .WithHeaders(headers ?? new { })
                                    .PostAsync(content).ReceiveXml<T>();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError($"FlurlHttpException message: {ex.Message}");
                //var response = ex.GetResponseJson<T>();
                return default;
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<string> PostJSONForString(string path, object payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                var result = await new Url(path)
                    .WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .WithCookies(cookies ?? new { })
                                   .WithHeaders(headers ?? new { })
                    .PostJsonAsync(payload ?? new object()).ReceiveString();
                return result;
            }
            catch (TaskCanceledException)
            {
                return string.Empty;
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }

        public async Task<T> PostXMLAsync<T>(string path, object payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                var result = await new Url(path)
                    .WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .WithCookies(cookies ?? new { })
                                   .WithHeaders(headers ?? new { })
                    .PostXmlAsync(payload ?? new object()).ReceiveXml<T>();
                return result;
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
            catch (TimeoutException)
            {
                return default;
            }
        }

        public async Task<FlurlResponse> PostJSONAsync(string path, object payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                return (FlurlResponse)await new Url(path).WithCookies(cookies ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                                  .WithHeaders(headers ?? new { })
                                   .AllowHttpStatus("400")
                                   .PostJsonAsync(payload ?? new object());
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException ex)
            {
                _logger?.LogError(ex, ex?.Message);
                return default;
            }
        }

        public async Task<string> UploadByteArrayAsync(string path, byte[] imageBytes, byte[] secondaryImageBytes, string token, ICollection<KeyValuePair<string, string>> payload = null)
        {
            try
            {
                using (Stream primaryFileStream = new MemoryStream(imageBytes))
                {
                    using (Stream secondaryFileStream = secondaryImageBytes == null ? new MemoryStream() : new MemoryStream(secondaryImageBytes))
                        return await new Url(path).WithTimeout(Configs.REST_REQUEST_TIMEOUT).AllowHttpStatus("400").PostMultipartAsync((mp) =>
                        {
                            mp.AddFile("File", primaryFileStream, "my_uploaded_image.jpg");
                            if (secondaryImageBytes != null)
                            {
                                mp.AddFile("BackFile", secondaryFileStream, "my_secondary_uploaded_image.jpg");
                            }
                            if (payload != null)
                            {
                                foreach (var item in payload)
                                {
                                    mp.AddString(item.Key, item.Value);
                                }
                            }
                        }).ReceiveJson<string>();
                }
            }
            catch (TaskCanceledException)
            {
                return string.Empty;
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }

        public async Task<T> PostUrlEncodedAsync<T>(string path, object payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                return await new Url(path).WithCookies(cookies ?? new { }).AllowHttpStatus("400").WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                                   .WithHeaders(headers ?? new { })
                                          .PostUrlEncodedAsync(payload ?? new object()).ReceiveJson<T>();
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<HttpResponseMessage> PostUrlEncodedAsync(string path, object payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                return (HttpResponseMessage)await new Url(path).WithCookies(cookies ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .AllowHttpStatus("400")
                                  .WithHeaders(headers ?? new { })
                                   .PostUrlEncodedAsync(payload ?? new object());
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<string> PostStringAsync(string path, string payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                return await new Url(path).WithCookies(cookies ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .AllowHttpStatus("400")
                                  .WithHeaders(headers ?? new { })
                                   .PostStringAsync(payload).ReceiveString();
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<T> PutJSONAsync<T>(string path, object payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                return await new Url(path).WithCookies(cookies ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .AllowHttpStatus("400")
                                   .WithHeaders(headers ?? new { })
                                          .PutJsonAsync(payload ?? new object()).ReceiveJson<T>();
            }
            catch (TaskCanceledException)
            {
                return default;
            }
        }

        public async Task PutJSONAsync(string path, object payload = null, object headers = null,
                                                  object cookies = null)
        {
            try
            {
                await new Url(path).WithCookies(cookies ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .AllowHttpStatus("400")
                                  .WithHeaders(headers ?? new { })
                                   .PutJsonAsync(payload ?? new object());
            }
            catch (TaskCanceledException)
            {

            }
            catch (IOException)
            {

            }
        }
        public async Task<HttpResponseMessage> DeleteJsonAsync(string path, object payload = null, object headers = null,
                                                         object cookies = null)
        {
            try
            {
                return (HttpResponseMessage)await new Url(path).WithCookies(cookies ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                    .AllowHttpStatus("400")
                                  .WithHeaders(headers ?? new { })
                                   .SendJsonAsync(HttpMethod.Delete, payload ?? new object());
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task<T> DeleteAsync<T>(string path,
                               object queryParams = null, object headers = null,
                              object cookies = null)
        {
            try
            {
                return await new Url(path)
                         .SetQueryParams(queryParams ?? new { })
                         .AllowHttpStatus("400")
                    .WithCookies(cookies ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                                   .WithHeaders(headers ?? new { })
                    .DeleteAsync().ReceiveJson<T>();
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        public async Task DeleteAsync(string path,
                               object queryParams = null, object headers = null,
                              object cookies = null)
        {
            try
            {
                await new Url(path)
                   .SetQueryParams(queryParams ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                   .AllowHttpStatus("400")
                   .WithCookies(cookies ?? new { })
                                  .WithHeaders(headers ?? new { })
                                         .DeleteAsync();
            }
            catch (TaskCanceledException)
            {

            }
            catch (IOException)
            {

            }
        }
        public async Task<byte[]> GetBytesAsync(string path,
                               object queryParams = null, object headers = null,
                              object cookies = null)
        {
            try
            {
                return await new Url(path)
                   .SetQueryParams(queryParams ?? new { }).WithTimeout(Configs.REST_REQUEST_TIMEOUT)
                   .AllowHttpStatus("400")
                   .WithCookies(cookies ?? new { })
                    .WithHeaders(headers ?? new { })
                    .GetBytesAsync();
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }
    }
    public class X509HttpFactory : DefaultHttpClientFactory
    {
        private readonly X509Certificate2 _cert;

        public X509HttpFactory(X509Certificate2 cert)
        {
            _cert = cert;
        }

        public override HttpMessageHandler CreateMessageHandler()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(_cert);
            return handler;
        }
    }
}
