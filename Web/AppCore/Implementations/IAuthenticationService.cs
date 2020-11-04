using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Implementations
{
    public interface IAuthenticationService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        User GetById(int id);
    }
}
