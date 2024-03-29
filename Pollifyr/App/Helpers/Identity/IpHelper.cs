using Newtonsoft.Json;
using RestSharp;
using Pollifyr.App.Models.Identity;

namespace Pollifyr.App.Helpers.Identity;

public class IpHelper
{
    private const string BaseApiUrl = "http://ip-api.com/json/";

    public static Task<IpData> GetIpData(string ip)
    {
        var url = BaseApiUrl + ip;
        
        var client = new RestClient(url);

        var response = client.Get(new RestRequest());

        IpData ipData = JsonConvert.DeserializeObject<IpData>(response.Content!)!;
        
        return Task.FromResult(ipData);
    }
}