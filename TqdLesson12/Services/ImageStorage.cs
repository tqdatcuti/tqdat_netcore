namespace TqdLesson12.Services;

public class ImageStorage(IWebHostEnvironment environment)
{
    private static readonly HashSet<string> AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];
    private const long MaxFileSize = 5 * 1024 * 1024;

    public async Task<string?> SaveAsync(IFormFile? file, string folder)
    {
        if (file is null || file.Length == 0)
        {
            return null;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
        {
            throw new InvalidDataException("Chỉ chấp nhận ảnh JPG, PNG, WEBP hoặc GIF.");
        }

        if (file.Length > MaxFileSize)
        {
            throw new InvalidDataException("Ảnh không được vượt quá 5 MB.");
        }

        var fileName = $"{Guid.NewGuid():N}{extension}";
        var relativePath = $"uploads/{folder}/{fileName}";
        var absoluteFolder = Path.Combine(environment.WebRootPath, "uploads", folder);
        Directory.CreateDirectory(absoluteFolder);

        await using var stream = File.Create(Path.Combine(absoluteFolder, fileName));
        await file.CopyToAsync(stream);
        return $"/{relativePath}";
    }

    public void Delete(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return;
        }

        var parts = relativePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3 || parts[0] != "uploads" || parts[1] is not ("products" or "banners" or "students"))
        {
            return;
        }

        var fileName = Path.GetFileName(parts[2]);
        var folder = parts[1];
        var path = Path.Combine(environment.WebRootPath, "uploads", folder, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}