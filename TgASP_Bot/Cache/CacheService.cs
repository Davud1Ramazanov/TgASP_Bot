using Newtonsoft.Json;
using StackExchange.Redis;

namespace TgASP_Bot.Cache
{
    public class CacheService
    {
        IDatabase db;
        public CacheService()
        {
            db = ConnectionHelper.Connection.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var val = db.StringGet(key);
            if (!string.IsNullOrEmpty(val))
            {
                return JsonConvert.DeserializeObject<T>(val);
            }
            return default;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset dateTimeOffset) => db.StringSet(key, JsonConvert.SerializeObject(value), dateTimeOffset.DateTime.Subtract(DateTime.Now));
    }
}
