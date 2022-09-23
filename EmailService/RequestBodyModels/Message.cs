using System.Text.Json.Serialization;

namespace EmailService.RequestBodyModels;

public class Message
{
    [JsonPropertyName("recipient")]
    public string Recipient { get; set; }
    
    [JsonPropertyName("subject")]
    public string Subject { get; set; }
    
    [JsonPropertyName("text")]
    public string Text { get; set; }
    
    [JsonPropertyName("carbon_copy_recipients")]
    public List<string>? CarbonCopyRecipients { get; set; }
}