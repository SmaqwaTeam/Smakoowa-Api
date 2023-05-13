﻿namespace Smakoowa_Api.Models.Identity
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtExpireDays { get; set; }
    }
}
