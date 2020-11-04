using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Implementations
{
    public interface IAuthorizeService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        User GetById(int id);
    }
}
