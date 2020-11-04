using AppCore.Implementations;
using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public class AuthorizeService:IAuthorizeService
    {
        IAuthenticationService service;
        public AuthorizeService(IAuthenticationService _service)
        {
            service = _service;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            return service.Authenticate(request);
        }

        public User GetById(int id)
        {
            return service.GetById(id);
        }
    }
}
