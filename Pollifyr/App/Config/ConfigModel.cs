using System.ComponentModel;
using Newtonsoft.Json;

namespace Pollifyr.App.Config;

public class ConfigModel
{
    
    [JsonProperty("Information")] public InformationData Information { get; set; } = new();
    
    [JsonProperty("Properties")] public PropertyData Properties { get; set; } = new();
    
    [JsonProperty("Database")] public DatabaseData Database { get; set; } = new();
    
    [JsonProperty("Security")] public SecurityData Security { get; set; } = new();
    
    public class InformationData
    {
        [JsonProperty("App-Name")] public string AppName { get; set; } = "Pollifyr";

        [JsonProperty("Use-Imprint")] public bool UseImprint { get; set; }
        
        [JsonProperty("Show-Credits")] public bool ShowCredits { get; set; } = true;
    }
    
    public class DatabaseData
    {
        [JsonProperty("Host")] public string Host { get; set; } = "your.db.host";
        
        [JsonProperty("Port")] public int Port { get; set; } = 3306;
        
        [JsonProperty("Username")] public string Username { get; set; } = "db_user";
        
        [JsonProperty("Password")] public string Password { get; set; } = "s3cr3t";
        
        [JsonProperty("Database")] public string Database { get; set; } = "database";
    }

    public class SecurityData
    {
        [JsonProperty("Token-Duration")] public int TokenDuration { get; set; } = 30;
        
        [JsonProperty("Token-Secret")] public string Secret { get; set; } = "S1QFoCJQ2TAZDRGjO5eWD4g5gT0kUgdUcyWq4w0AVWUEW";
    }

    public class PropertyData
    {
        [JsonProperty("Application-Port")] public int Port { get; set; } = 80;
        
        [JsonProperty("Allow-Register")] public bool AllowRegister { get; set; }

        [JsonProperty("Use-HSTS")] public bool UseHsts { get; set; } = true;
    }


}