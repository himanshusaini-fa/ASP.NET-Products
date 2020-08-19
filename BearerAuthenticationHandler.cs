using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Products.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Products.Authentication
{
    public class BearerAuthenticationHandler : AuthenticationHandler<BearerAuthenticationOptions>
    {

        private const string AuthorizationHeaderName = "Authorization";
        private const string SchemeName = "Bearer";
        private readonly ProductsContext _context;

        public BearerAuthenticationHandler(IOptionsMonitor<BearerAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ProductsContext context) : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthorizationHeaderName))
            {
                return AuthenticateResult.Fail("Improper Auth Header");
            }

            if (!AuthenticationHeaderValue.TryParse(Request.Headers[AuthorizationHeaderName], out AuthenticationHeaderValue headerValue))
            {
                return AuthenticateResult.Fail("Improper Auth Scheme");
            }

            if (!SchemeName.Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Auth Header");
            }


            var key = headerValue.Parameter;
            var apiKey = _context.ApiKeys.FirstOrDefault(a => a.key == key);
            if (apiKey == null)
            {
                return AuthenticateResult.Fail("Invalid user!");
            }
            var userClaim = new Claim(ClaimTypes.NameIdentifier, JsonConvert.SerializeObject(apiKey));
            var claims = new[] { new Claim(ClaimTypes.Name, key), userClaim };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            //return AuthenticateResult.NoResult();
            return AuthenticateResult.Success(ticket);

        }
    }
}
