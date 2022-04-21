namespace GremlinIssueAzureFunction.Common
{
    public class ConnectionPoolSettingsConfigValues
    {
        public int PoolSize { get; set; } = 200;
        public int MaxInProcessPerConnection { get; set; } = 1600;
        public int ReconnectionAttempts { get; set; } = 3;
        public int ReconnectionBaseDelayInMilliseconds { get; set; } = 100;
    }
}
