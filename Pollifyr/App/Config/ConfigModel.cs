using Newtonsoft.Json;

namespace Pollifyr.App.Config;

public class ConfigModel
{
    
    [JsonProperty("Information")] public InformationData Information { get; set; } = new();

    public class InformationData
    {
        [JsonProperty("AppName")] public string AppName { get; set; } = "Pollifyr";

        [JsonProperty("Imprint")] public bool Imprint { get; set; } = false;
        
        [JsonProperty("ShowCredits")] public bool Credits { get; set; } = true;
    }
    
    [JsonProperty("Database")] public DatabaseData Database { get; set; } = new();
    
    public class DatabaseData
    {
        [JsonProperty("Host")] public string Host { get; set; } = "your.db.host";
        
        [JsonProperty("Port")] public int Port { get; set; } = 3306;
        
        [JsonProperty("Username")] public string Username { get; set; } = "db_user";
        
        [JsonProperty("Password")] public string Password { get; set; } = "s3cr3t";
        
        [JsonProperty("Database")] public string Database { get; set; } = "database";
    }
    
    [JsonProperty("Security")] public SecurityData Security { get; set; } = new();

    public class SecurityData
    {

        [JsonProperty("Admin")] public AdminAccountData AdminAccounts { get; set; } = new();

        public class AdminAccountData
        {

            [JsonProperty("Username")] public string AdminUsername { get; set; } = "admin";

            [JsonProperty("Password")] public string AdminPassword { get; set; } = "s3cr3t";
        }
    }


}