namespace CatchLightning.Core.Abstractions
{
    internal interface IConfig
    {
        public string? GetConnectionString(string name = "Default");
        public void SetConnectionString(string connectionString, string name = "Default");
    }
}
