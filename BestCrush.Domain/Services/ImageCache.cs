namespace BestCrush.Domain.Services;

public class ImageCache(string imageCacheDirectory)
{
    public Task<bool> HasImageAsync(string key)
    {
        string path = GetPath(key);
        return Task.FromResult(File.Exists(path));
    }

    public Task<byte[]> LoadImageAsync(string key)
    {
        string path = GetPath(key);
        return File.ReadAllBytesAsync(path);
    }

    public Task StoreImageAsync(string key, byte[] data)
    {
        string path = GetPath(key);
        string? directory = Path.GetDirectoryName(path);
        if (directory != null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return File.WriteAllBytesAsync(path, data);
    }

    string GetPath(string key) => Path.Combine(imageCacheDirectory, key);
}
