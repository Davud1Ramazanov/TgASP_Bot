using StackExchange.Redis;

namespace TgASP_Bot.Cache
{
    public class ConnectionHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConn;
        public static ConnectionMultiplexer Connection
        {
            get { return lazyConn.Value; }
        }

        static ConnectionHelper()
        {
            lazyConn = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisUrl"]);
            });
        }

    }
}
