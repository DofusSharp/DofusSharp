namespace Tests.UnitTests.DofusDb.ApiClients.Extensions;

static class StreamExtensions
{
    public static byte[] ReadToByteArray(this Stream stream)
    {
        if (stream is MemoryStream memoryStream)
        {
            return memoryStream.ToArray();
        }

        using MemoryStream result = new();
        stream.CopyTo(result);
        return result.ToArray();
    }
}
