namespace DofusSharp.DofusDb.ApiClients.Http;

/// <summary>
///     A stream that wraps an <see cref="HttpResponseMessage" /> to ensure it is disposed when the stream is disposed.
///     This allows to return the content of the response as a stream without copying to a new stream.
/// </summary>
/// <remarks>https://stackoverflow.com/a/75668203/26358508</remarks>
class HttpResponseMessageStream : Stream
{
    readonly HttpResponseMessage _response;
    readonly Stream _inner;

    HttpResponseMessageStream(Stream stream, HttpResponseMessage response)
    {
        _inner = stream;
        _response = response;
    }

    public override bool CanRead => _inner.CanRead;

    public override bool CanSeek => _inner.CanSeek;

    public override bool CanWrite => _inner.CanWrite;

    public override long Length => _inner.Length;

    public override long Position {
        get => _inner.Position;
        set => _inner.Position = value;
    }

    public static async Task<HttpResponseMessageStream> Create(HttpResponseMessage response) => new(await response.Content.ReadAsStreamAsync(), response);

    public override ValueTask DisposeAsync()
    {
        _response.Dispose();
        return base.DisposeAsync();
    }

    public override void Flush() => _inner.Flush();

    public override int Read(byte[] buffer, int offset, int count) => _inner.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => _inner.Seek(offset, origin);

    public override void SetLength(long value) => _inner.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) => _inner.Write(buffer, offset, count);

    protected override void Dispose(bool disposing)
    {
        _response.Dispose();
        base.Dispose(disposing);
    }
}
