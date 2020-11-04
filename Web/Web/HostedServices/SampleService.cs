using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Web.HostedServices
{
    public class SampleService : IHostedService, IDisposable
    {
        private readonly ILogger<SampleService> _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public SampleService(IServiceScopeFactory factory, ILogger<SampleService> logger)
        {
            _logger = logger;
            _scopeFactory = factory;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(500));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _ = KeepBackgroundServiceAliveAsync();
            _logger.LogInformation("Service start");
            //Do your stuff here
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async Task KeepBackgroundServiceAliveAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _httpClient = scope.ServiceProvider.GetService<IHttpClientFactory>().CreateClient("Self");
                var _absolutePath = _httpClient.BaseAddress.Segments.Length > 1 ? _httpClient.BaseAddress.AbsolutePath : string.Empty;
                var result = await _httpClient.GetAsync("/Aux/KeepAlive");
            }
        }

    }
}
