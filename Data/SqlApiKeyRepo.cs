using System.Linq;
using Products.Models;

namespace Products.Data
{
    public class SqlApiKeyRepo : IApiKeyRepo
    {
        private readonly ApiKeyContext _context;
        public SqlApiKeyRepo(ApiKeyContext context)
        {
            _context = context;
        }
        public ApiKey GetApiKey(string key)
        {
            return _context.ApiKeys.FirstOrDefault(k => k.key == key);
        }
        public bool Authenticated(string key)
        {
            var apikey = GetApiKey(key);
            if (apikey == null)
            {
                return false;
            }
            return true;
        }
    }
}