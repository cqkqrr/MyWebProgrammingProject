using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MyWebProgrammingProject.Services
{
    public class GeminiRestRecommendationService : IAiRecommendationService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<GeminiRestRecommendationService> _logger;

        public GeminiRestRecommendationService(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<GeminiRestRecommendationService> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<string> GetWorkoutAndDietAsync(int heightCm, int weightKg, string bodyType, string goal, CancellationToken cancellationToken = default)
        {
            var apiKey = _configuration["Gemini:ApiKey"] ?? Environment.GetEnvironmentVariable("GOOGLE_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKey)) return "API Anahtarı bulunamadı.";

            var model = _configuration["Gemini:Model"] ?? "gemini-1.5-flash";
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

            var prompt = $"Boy: {heightCm}cm, Kilo: {weightKg}kg, Vücut: {bodyType}, Hedef: {goal}. Bana Türkçe spor ve diyet programı hazırla.";

            var requestBody = new
            {
                contents = new[] { new { parts = new[] { new { text = prompt } } } }
            };

            try
            {
                var client = _httpClientFactory.CreateClient("Gemini");
                using var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                using var resp = await client.SendAsync(req, cancellationToken);
                var respText = await resp.Content.ReadAsStringAsync(cancellationToken);

                if (!resp.IsSuccessStatusCode) return $"Hata: {resp.StatusCode}";

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var geminiResp = JsonSerializer.Deserialize<GeminiResponse>(respText, options);

                return geminiResp?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text ?? "AI cevap veremedi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gemini Hatası");
                return "Bir hata oluştu.";
            }
        }
    }

    // JSON Karşılama Sınıfları
    public class GeminiResponse { public Candidate[] Candidates { get; set; } }
    public class Candidate { public Content Content { get; set; } }
    public class Content { public Part[] Parts { get; set; } }
    public class Part { public string Text { get; set; } }
}