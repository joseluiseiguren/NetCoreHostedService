namespace Ocr.HostedService
{
    public class OcrRepository : IOcrRepository
    {
        public string InsertOcr(string ocrName)
        {
            return ocrName + ": OK";
        }
    }
}
