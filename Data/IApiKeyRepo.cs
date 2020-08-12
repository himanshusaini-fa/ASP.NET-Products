using Products.Models;

namespace Products.Data
{
    public interface IApiKeyRepo
    {
        public ApiKey GetApiKey(string key);
        public bool Authenticated(string key);
    }
}