namespace Ocr.HostedService
{
    internal interface IOcrRepository
    {
        string InsertOcr(string ocrName);
    }
}
