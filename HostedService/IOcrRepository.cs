namespace Ocr.HostedService
{
    public interface IOcrRepository
    {
        string InsertOcr(string ocrName);
    }
}
