using AppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore.Handlers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roles = context.Filters;
            //Checks if the controller does not require authentication
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;
            else
            {
                var user = (User)context.HttpContext.Items["User"];
                bool hasAccess = false;
                var auth = context.ActionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>().FirstOrDefault();
                string[] _roles = (auth != null? auth.Roles:null);
                if (_roles != null && user != null)
                    hasAccess = _roles.Contains(user.Role);
                else
                    hasAccess = true;

                if (user == null | !hasAccess)
                {
                    //not logged in
                    context.Result = new JsonResult(new { message = "Unauthorized" })
                    { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
