using System.Text.Json;
using System.Text.Json.Serialization;

namespace TimeControl
{
    public class AppConfig
    {
        public string PathAppsettings { get; set; } = Directory.GetCurrentDirectory() + "\\appsetings.json";
        public string PathLog { get; set; } = Directory.GetCurrentDirectory() + "\\log.txt";
        public string Username { get; set; } = "user";
        public string ConnectionString { get; set; } = "Data Source=" + Directory.GetCurrentDirectory() + "\\data.db";

        public JsonSerializerOptions OptionsJson = new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public void CreateConfig()
        {
            string json = JsonSerializer.Serialize(this, OptionsJson);
            File.WriteAllText(PathAppsettings, json);
        }

        public AppConfig? ReadConfig()
        {
            string json = File.ReadAllText(PathAppsettings);
            return JsonSerializer.Deserialize<AppConfig>(json);
        }
    }
}
