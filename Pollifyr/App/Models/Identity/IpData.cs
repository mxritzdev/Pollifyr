using Newtonsoft.Json;

namespace Pollifyr.App.Models.Identity;

public class IpData
{
    [JsonProperty("status")] public string Status { get; set; } = "fail";
    
    [JsonProperty("country")] public string Country { get; set; } = "N/A";
    
    [JsonProperty("countryCode")] public string CountryCode { get; set; } = "N/A";
    
    [JsonProperty("regionName")] public string Region { get; set; } = "N/A";
    
    [JsonProperty("city")] public string City { get; set; } = "N/A";
    
    [JsonProperty("zip")] public string ZipCode { get; set; } = "N/A";

    [JsonProperty("lat")] public double Latitude { get; set; } = 0.0;

    [JsonProperty("lon")] public double Longitude { get; set; } = 0.0;
    
    [JsonProperty("timezone")] public string TimeZone { get; set; } = "N/A";
    
    [JsonProperty("isp")] public string InternetServiceProvider { get; set; } = "N/A";
    
    [JsonProperty("org")] public string Organization { get; set; } = "N/A";

    [JsonProperty("query")] public string Ip { get; set; } = "N/A";
}
