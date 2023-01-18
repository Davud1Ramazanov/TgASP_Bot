namespace TgASP_Bot.Cache
{
    public interface ICacheService
    {
        T GetData<T>(string key);

        bool SetData<T>(string key, T value, DateTimeOffset dateTimeOffset);
    }
}
