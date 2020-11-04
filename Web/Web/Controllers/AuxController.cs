using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    public class AuxController : ControllerBase
    {
        [HttpGet]
        [Route("Aux/KeepAlive")]
        public string KeepAlive()
        {
            return "alive";
        }

        [HttpPost]
        [Route("/Error")]
        public IActionResult Error() => Problem();
    }
}
