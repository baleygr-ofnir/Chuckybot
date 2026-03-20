using System.Text.Json.Serialization;

namespace Chuckybot;

public class ChuckNorrisResponse
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}