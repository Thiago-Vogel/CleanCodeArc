using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Web.HealthChecks
{
    public class BasicHealthCheck : IHealthCheck
    {
        IServiceScopeFactory _service;

        public BasicHealthCheck(IServiceScopeFactory service)
        {
            _service = service;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var scope = _service.CreateScope())
                {
                    var client = scope.ServiceProvider.GetService<IHttpClientFactory>().CreateClient("Self");
                    var response = await client.GetAsync("/Entidade/Get");
                    response.EnsureSuccessStatusCode();
                }
                return new HealthCheckResult(HealthStatus.Healthy,"Application running");
            }
            catch(Exception ex)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, ex.Message);
            }
        }
    }
}
