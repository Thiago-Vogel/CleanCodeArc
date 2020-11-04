using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Implementations;
using AppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        IAuthorizeService _service;

        public AuthController(IAuthorizeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Aut/Authenticate")]
        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            return _service.Authenticate(request);
        }


    }
}
