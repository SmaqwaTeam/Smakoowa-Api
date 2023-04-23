namespace Smakoowa_Api.Services.Interfaces.Helper
{
    public interface IJsonKeyValueGetter
    {
        public string GetValueByKey(string jsonString, string key);
    }
}
