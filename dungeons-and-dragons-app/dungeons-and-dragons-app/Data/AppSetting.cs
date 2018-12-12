public class AppSetting
{
    public string ConnectionString { get; set; }

    public AppSetting(string conn)
    {
        this.ConnectionString = conn;
    }
}
