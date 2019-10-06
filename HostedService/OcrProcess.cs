
using Serilog;
using System.Threading.Tasks;

namespace Ocr.HostedService
{
    internal class OcrProcess
    {
        private readonly IOcrRepository _ocrReporitory;
        private readonly ILogger _logger;

        public OcrProcess(IOcrRepository ocrReporitory, 
                          ILogger logger)
        {
            this._ocrReporitory = ocrReporitory;
            this._logger = logger;
        }
    
        public async Task<string> Process()
        {
            return await Task.Run(() => 
            {
                while (true)
                {
                    this._logger.Warning("XXX");
                    System.Threading.Thread.Sleep(200);
                }                

                return this._ocrReporitory.InsertOcr("Pepe");
            });
            
            
        }

    }
}
