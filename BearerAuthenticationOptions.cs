using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Authentication
{
    public class BearerAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Token { set; get; }
    }
}
