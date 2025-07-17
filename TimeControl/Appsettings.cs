namespace TimeControl
{
    public class Appsettings
    {
        public string ConnectionString { get; set; } = Directory.GetCurrentDirectory() + "\\data.db";

        public string UserName { get; set; } = "user";

        public string AddressAppseting { get; set; } = Directory.GetCurrentDirectory() + "\\appsetting.json";
    }
}
