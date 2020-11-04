using AppCore.Helpers;
using AppCore.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Handlers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        IServiceScopeFactory _scopeFactory;

        public JwtMiddleware(RequestDelegate next, IServiceScopeFactory factory)
        {
            _scopeFactory = factory;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Gets the authorization token from the current request
            string token = null;
            if (context.Request.Headers.ContainsKey("Authorization"))
                token = context.Request.Headers["Authorization"];

            if (token != null)
                attachUserToContext(context, token);
            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettings.AppSecret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.ToArray().FirstOrDefault(x => x.Type == "id").Value);

                //attach user to context on successful jwt validation
                using (var scope = _scopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<IAuthorizeService>();
                    context.Items["User"] = service.GetById(userId);
                }
            }
            catch(Exception ex)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}

