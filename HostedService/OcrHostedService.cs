using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
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
            this._diProvider = servicesCollection.BuildServiceProvider();

            this._logger = new LoggerConfiguration()
                                    .WriteTo.File(
                                                path: Path.Combine(this._settings.LogFolder, "logOcrHostedService.txt"), 
                                                rollingInterval: RollingInterval.Day, 
                                                rollOnFileSizeLimit: true, 
                                                fileSizeLimitBytes: 5242880, //5MB
                                                levelSwitch: new LoggingLevelSwitch(this.ConvertStringToLogEventLevel(this._settings.LogLevel)))
                                    .CreateLogger();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var process = new OcrProcess(this._diProvider.GetService<IOcrRepository>(), this._logger);
            var result = await process.Process();

            Debug.WriteLine(result);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private LogEventLevel ConvertStringToLogEventLevel(string logLevel)
        {
            try
            {
                return (LogEventLevel)Enum.Parse(typeof(LogEventLevel), logLevel);
            }
            catch (Exception)
            {
                return LogEventLevel.Debug;
            }
        }
    }
}
