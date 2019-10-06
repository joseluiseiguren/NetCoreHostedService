namespace Ocr.HostedService
{
    internal class OcrRepository : IOcrRepository
    {
        public string InsertOcr(string ocrName)
        {
            return ocrName + ": OK";
        }
    }
}
