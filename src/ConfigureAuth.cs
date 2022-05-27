using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Text;
using System.IO;

namespace restaurant_dashboard_backend
{
    public static class ConfigureAuth
    {
        private static string contentRoot;
        public static IServiceCollection ConfigureAuthAndToken(this IServiceCollection services, IConfiguration configuration)
        {
            contentRoot = configuration.GetValue<string>(Microsoft.AspNetCore.Hosting.WebHostDefaults.ContentRootKey);
            
            var securitySettings = configuration.GetSection("Security").Get<SecuritySettings>();
            services.AddAuthentication(o=>{
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.Audience = securitySettings.Audience;
                options.TokenValidationParameters = GetTokenValidationParameters(securitySettings);
            });
            return services;
        }

        private static SymmetricSecurityKey GetSymetricSigningKey(SecuritySettings settings) {
            var secretKey = settings.SecretKey;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return signingKey;
        }

        private static SecurityKey GetAsymetricSigningKey(SecuritySettings settings) {
            var rsa = System.Security.Cryptography.RSA.Create();

            var pemPath = Path.Combine(contentRoot, "keys", settings.UseKms? "kms-public.key" : "public.key");
            var keyStr = File.ReadAllText(pemPath);

            if(settings.UseKms){
                keyStr = keyStr.Replace("-----BEGIN PUBLIC KEY-----", "");
                keyStr = keyStr.Replace(System.Environment.NewLine, "");
                keyStr = keyStr.Replace("-----END PUBLIC KEY-----", "");
                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(keyStr), out _);
            }else{
                keyStr = keyStr.Replace("-----BEGIN RSA PUBLIC KEY-----", "");
                keyStr = keyStr.Replace(System.Environment.NewLine, "");
                keyStr = keyStr.Replace("-----END RSA PUBLIC KEY-----", "");
                 rsa.ImportRSAPublicKey(Convert.FromBase64String(keyStr), out _);
            }
           
            var securityKey = new RsaSecurityKey(rsa);
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
            return credentials.Key;
        }

        private static SecurityKey GetAsymetricKmsSigningKey(SecuritySettings settings) {
            var secretKey = settings.SecretKey;
            var rsa = System.Security.Cryptography.RSA.Create();

            var pemPath = Path.Combine(contentRoot, "keys", "kms-public.key");
            var keyStr = File.ReadAllText(pemPath); 
            keyStr = keyStr.Replace("-----BEGIN PUBLIC KEY-----", "");
            keyStr = keyStr.Replace(System.Environment.NewLine, "");
            keyStr = keyStr.Replace("-----END PUBLIC KEY-----", "");

            rsa.ImportRSAPublicKey(Convert.FromBase64String(keyStr), out _);
            var securityKey = new RsaSecurityKey(rsa);

            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

            return credentials.Key;
        }

        private static TokenValidationParameters GetTokenValidationParameters(SecuritySettings settings){
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = settings.UseRsa ? GetAsymetricSigningKey(settings) : GetSymetricSigningKey(settings),
                ValidateIssuer = true,
                ValidIssuer = settings.Issuer,
                ValidateLifetime =true
                // ValidateAudience = true,
                // ValidAudience =settings.audience,
            };
            return tokenValidationParameters;
        }
    }
}
