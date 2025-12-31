using Gym.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Gym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ChatController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequestDto request)
        {
            var apiKey = _configuration["GeminiApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey == "PASTE_YOUR_KEY_HERE")
            {
                return BadRequest(new { response = "API Key is missing or not configured. Please add your GeminiApiKey in appsettings.json." });
            }

            var prompt = "You are a helpful assistant for a Gym Management System. Answer the following question: " + request.Message;

            var payload = new
            {
                contents = new[]
                {
                    new {
                        parts = new[] { new { text = prompt } }
                    }
                }
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try 
            {
                var response = await _httpClient.PostAsync($"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    
                    // Debug: parsing error to see if it's 404
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // Try to list available models to see what IS supported
                         var listResponse = await _httpClient.GetAsync($"https://generativelanguage.googleapis.com/v1beta/models?key={apiKey}");
                         if (listResponse.IsSuccessStatusCode)
                         {
                             var listJson = await listResponse.Content.ReadAsStringAsync();
                             return StatusCode((int)response.StatusCode, new { response = $"Model 'gemini-1.5-flash' not found. Available models: {listJson}" }); 
                         }
                    }

                    return StatusCode((int)response.StatusCode, new { response = $"Error from AI provider: {error}" });
                }

                var responseString = await response.Content.ReadAsStringAsync();
                
                using (var doc = JsonDocument.Parse(responseString))
                {
                    var text = doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return Ok(new { response = text });
                }
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { response = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
