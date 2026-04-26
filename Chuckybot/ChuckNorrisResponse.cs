using System.Text.Json.Serialization;

namespace Chuckybot;

public record ChuckNorrisSearchResponse(
    [property: JsonPropertyName("total")] int Total,
    [property: JsonPropertyName("result")] List<ChuckNorrisResponse> Result
);

public record ChuckNorrisResponse
(
    [property: JsonPropertyName("value")] string Value
);