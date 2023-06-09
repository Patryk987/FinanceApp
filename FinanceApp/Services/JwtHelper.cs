using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace FinanceApp.Services
{

    public class JwtHelper
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public JwtHelper(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }

        public bool ValidateToken(string token)
        {
            Console.WriteLine(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return claimsPrincipal.Identity.IsAuthenticated;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // List<string> jwtData = new List<string>();
            // jwtData.Add(jwtToken.id);
            // TODO: Poprawić na bezpośrednie pobieranie id i pozostałych danych
            string id = jwtToken.Claims.First().Value;

            return id;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };
        }
    }
}