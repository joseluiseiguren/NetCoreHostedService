using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ocr.HostedService
{
    public class OcrHostedService : IHostedService
    {
        private readonly OcrSettings _settings;
        private readonly IServiceProvider _diProvider;
        private readonly ILogger _logger;

        public OcrHostedService(IOptions<OcrSettings> settings)
        {
            this._settings = settings.Value;

            var servicesCollection = new ServiceCollection();
            servicesCollection.AddTransient<IOcrRepository, OcrRepository>();
            servicesCollection.AddLogging(configure => configure.AddSerilog(dispose: true));
            this._diProvider = servicesCollection.BuildServiceProvider();

            this._logger = new LoggerConfiguration()
                                    .WriteTo.Console()
                                    .WriteTo.File(Path.Combine(this._settings.LogFolder, "ocrlog.txt"))
                                    .CreateLogger();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var process = new OcrProcess(this._diProvider.GetService<IOcrRepository>(), this._logger);
            var result = process.Process();

            Debug.WriteLine(result);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
