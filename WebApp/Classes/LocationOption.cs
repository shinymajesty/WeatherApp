using System.Text.Json.Serialization;

/// <summary>
/// This class represents the possible locations returned from the api autcompletion query.
/// </summary>
public class LocationOption
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("lat")]
    public float Latitude { get; set; }

    [JsonPropertyName("lon")]
    public float Longitude { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
