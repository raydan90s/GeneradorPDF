using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Yachasoft.Sri.FacturacionElectronica.Services
{
    public interface IFrappeFileUploader
    {
        Task<FrappeUploadResult> UploadFileAsync(string filePath, string fileName, 
            string folder = "Home/Attachments", string doctype = null, string docname = null);
        
        Task<FrappeUploadResult> UploadFileStreamAsync(Stream fileStream, string fileName,
            string folder = "Home/Attachments", string doctype = null, string docname = null);
    }

    public class FrappeUploadResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public JObject RawResponse { get; set; }
    }

    public class FrappeFileUploader : IFrappeFileUploader
    {
        private readonly string _frappeUrl;
        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly string _defaultFolder;
        private readonly HttpClient _httpClient;
    public FrappeFileUploader(IConfiguration configuration)
    {
        _frappeUrl = configuration["Frappe:Url"] ?? throw new ArgumentNullException("Frappe:Url no configurado");
        _apiKey = configuration["Frappe:ApiKey"] ?? throw new ArgumentNullException("Frappe:ApiKey no configurado");
        _apiSecret = configuration["Frappe:ApiSecret"] ?? throw new ArgumentNullException("Frappe:ApiSecret no configurado");
        _defaultFolder = configuration["Frappe:FolderRetencion"] ?? "Home/Attachments";

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_frappeUrl),
            Timeout = TimeSpan.FromMinutes(5)
        };

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("token", $"{_apiKey}:{_apiSecret}");
    }

        public async Task<FrappeUploadResult> UploadFileAsync(
            string filePath, 
            string fileName, 
            string folder = "Home/Attachments", 
            string doctype = null, 
            string docname = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"[ERROR] El archivo no existe: {filePath}");
                    return new FrappeUploadResult
                    {
                        Success = false,
                        Error = "file_not_found",
                        FileName = fileName
                    };
                }

                using var fileStream = File.OpenRead(filePath);
                return await UploadFileStreamAsync(fileStream, fileName, folder, doctype, docname);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception subiendo archivo a Frappe: {ex.Message}");
                return new FrappeUploadResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<FrappeUploadResult> UploadFileStreamAsync(
            Stream fileStream,
            string fileName,
            string folder = "Home/Attachments",
            string doctype = null,
            string docname = null)
        {
            try
            {
                folder ??= "Home/Attachments";

                using var content = new MultipartFormDataContent();

                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Add(fileContent, "file", fileName);

                content.Add(new StringContent("0"), "is_private");
                content.Add(new StringContent(folder), "folder");

                if (!string.IsNullOrEmpty(doctype) && !string.IsNullOrEmpty(docname))
                {
                    content.Add(new StringContent(doctype), "attached_to_doctype");
                    content.Add(new StringContent(docname), "attached_to_name");
                }

                var response = await _httpClient.PostAsync("/api/method/upload_file", content);

                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[ERROR] Frappe respondió con código {response.StatusCode}: {responseBody}");
                    return new FrappeUploadResult
                    {
                        Success = false,
                        Error = "upload_failed",
                        FileName = fileName,
                        RawResponse = JObject.Parse(responseBody)
                    };
                }

                var jsonResponse = JObject.Parse(responseBody);
                var messageData = jsonResponse["message"];

                return new FrappeUploadResult
                {
                    Success = true,
                    FileUrl = messageData?["file_url"]?.ToString(),
                    FileName = messageData?["file_name"]?.ToString() ?? fileName,
                    RawResponse = jsonResponse
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception en UploadFileStreamAsync: {ex.Message}");
                Console.WriteLine($"[TRACE] {ex.StackTrace}");
                return new FrappeUploadResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }
}