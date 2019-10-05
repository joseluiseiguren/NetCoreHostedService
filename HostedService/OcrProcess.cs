
using Serilog;

namespace Ocr.HostedService
{
    public class OcrProcess
    {
        private readonly IOcrRepository _ocrReporitory;
        private readonly ILogger _logger;

        public OcrProcess(IOcrRepository ocrReporitory, 
                          ILogger logger)
        {
            this._ocrReporitory = ocrReporitory;
            this._logger = logger;
        }
    
        public string Process()
        {
            this._logger.Warning("XXX");
            return this._ocrReporitory.InsertOcr("Pepe");
        }

    }
}
