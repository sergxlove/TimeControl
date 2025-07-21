namespace TimeControl
{
    public class AppConfig
    {
        public string PathAppsettings { get; set; } = Directory.GetCurrentDirectory() + "\\appsetings.json";
        public string Username { get; set; } = "user";
        public string ConnectionString { get; set; } = "Data Source=" + Directory.GetCurrentDirectory() + "\\data.db";
    }
}
