using AppCore.Helpers;
using AppCore.Implementations;
using AppCore.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private List<User> _users { get; set; }
        

        public AuthenticationService()
        {
            //You should implement something to get your users on your environment
            _users = new List<User>
            {
                new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", Role= Role.adm },
                new User { Id = 2, FirstName = "Test", LastName = "User", Username = "test2", Password = "test", Role= Role.common }
            };
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            //This implementations is only for tests purposes, you must have a validation service
            var user = _users.SingleOrDefault(x => x.Username == request.User && x.Password == request.Password);
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.AppSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

    }
}
