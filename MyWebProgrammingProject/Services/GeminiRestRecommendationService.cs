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

        public async Task<string> GetWorkoutAndDietAsync(
            int heightCm,
            int weightKg,
            string bodyType,
            string goal,
            CancellationToken cancellationToken = default)
        {
            var apiKey =
                _configuration["Gemini:ApiKey"]
                ?? Environment.GetEnvironmentVariable("GOOGLE_API_KEY")
                ?? Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return "Gemini API anahtarı bulunamadı. User-Secrets ile `Gemini:ApiKey` ayarla veya GOOGLE_API_KEY / GEMINI_API_KEY ortam değişkeni ekle.";
            }

            var model = _configuration["Gemini:Model"];
            if (string.IsNullOrWhiteSpace(model))
                model = "gemini-3-flash-preview";

            // ✅ Senin istediğin URL formatı:
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

            var bmi = weightKg / Math.Pow(heightCm / 100.0, 2);

            var prompt = $@"
Sen bir spor salonu antrenörü ve beslenme koçusun.
Tıbbi teşhis/tedavi iddiası yok, sakatlık/rahatsızlık varsa doktora yönlendir.
Cevabı Türkçe, başlıklar ve maddeler halinde ver.

Kullanıcı:
- Boy: {heightCm} cm
- Kilo: {weightKg} kg
- BMI (yaklaşık): {bmi:0.0}
- Vücut tipi: {bodyType}
- Hedef: {goal}

ÇIKTI FORMAT:
1) Durum özeti (BMI yorumu + kısa not)
2) 1 haftalık örnek antrenman planı (gün gün, 45-60 dk)
3) Beslenme önerisi (yaklaşık kalori yaklaşımı + örnek 1 günlük menü)
4) Güvenlik ve sakatlanma önleme
5) 3 soru (bir sonraki iyileştirme için)
".Trim();

            // Gemini generateContent body: contents -> parts -> text :contentReference[oaicite:1]{index=1}
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.4,
                    maxOutputTokens = 900
                }
            };

            try
            {
                var client = _httpClientFactory.CreateClient("Gemini");
                client.Timeout = TimeSpan.FromSeconds(60);

                using var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                req.Content = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json");

                using var resp = await client.SendAsync(req, cancellationToken);
                var respText = await resp.Content.ReadAsStringAsync(cancellationToken);

                if (!resp.IsSuccessStatusCode)
                {
                    // Hata mesajını mümkünse JSON içinden çek
                    var msg = TryExtractError(respText);
                    return $"AI isteği başarısız: {(int)resp.StatusCode} {resp.ReasonPhrase}\n{msg}";
                }

                var result = ExtractCandidateText(respText);
                return string.IsNullOrWhiteSpace(result)
                    ? "AI yanıtı boş döndü. Tekrar dene."
                    : result.Trim();
            }
            catch (OperationCanceledException)
            {
                return "AI isteği iptal edildi/timeout oldu. Lütfen tekrar dene.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gemini REST çağrısında hata.");
                return $"AI çağrısı sırasında hata: {ex.Message}";
            }
        }

        private static string ExtractCandidateText(string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                if (!doc.RootElement.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
                    return string.Empty;

                var content = candidates[0].GetProperty("content");
                if (!content.TryGetProperty("parts", out var parts) || parts.GetArrayLength() == 0)
                    return string.Empty;

                var sb = new StringBuilder();
                foreach (var part in parts.EnumerateArray())
                {
                    if (part.TryGetProperty("text", out var t))
                        sb.AppendLine(t.GetString());
                }

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string TryExtractError(string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                // Bazı hatalarda: { "error": { "message": "...", ... } }
                if (doc.RootElement.TryGetProperty("error", out var err) &&
                    err.TryGetProperty("message", out var msg))
                {
                    return msg.GetString() ?? json;
                }

                return json;
            }
            catch
            {
                return json;
            }
        }
    }
}
