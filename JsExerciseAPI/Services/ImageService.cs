
namespace JsExerciseAPI.Services;
public class ImageService
{
    private List<string> _imageUrls = new()
    {
        "https://img-9gag-fun.9cache.com/photo/a3Q5VW5_460s.jpg",
        "https://media.moddb.com/images/members/5/4550/4549205/duck.jpg",
        "https://www.kibrispdr.org/dwn/5/random-pic.jpg",
        "https://www.deke.com/files/images/Dekes-Techniques-061-duck-wears-dog-mask.jpg"
    };

    public int ImageCount => _imageUrls.Count;

    public async Task<Image?> GetImage(int id)
    {
        var inRange = 1 <= id && id <= _imageUrls.Count;
        if (!inRange) return null;

        var url = _imageUrls[id - 1];

        Image img = new()
        {
            Url = url,
            Metadata = await GetMetadata(url)
        };

        return img;
    }

    private static async Task<ImageMetadata> GetMetadata(string url)
    {
        if (!OperatingSystem.IsOSPlatform("windows"))
            throw new HttpRequestException("Please buy windows", null, System.Net.HttpStatusCode.NotAcceptable);

        var response = await new HttpClient().GetAsync(url);
        await using var stream = await response.Content.ReadAsStreamAsync();
        var imageFromStream = System.Drawing.Image.FromStream(stream);
        
        var metadata = new ImageMetadata
        {
            Width = imageFromStream.Width,
            Height = imageFromStream.Height,
            Size = FormatByteSize(response.Content.Headers.ContentLength?? 0)
        };

        return metadata;
    }

    private static string FormatByteSize(long size) => $"{size:#,###} bytes";

    public async Task<Image?> AddImage(string url)
    {
        Image img = new()
        {
            Url = url,
            Metadata = await GetMetadata(url)
        };

        _imageUrls.Add(url);

        return img;
    }
}
