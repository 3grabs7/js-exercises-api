
namespace JsExerciseAPI.Services;
public class ImageService
{
    public string[] ImageUrls = new string[]
    {
        "https://img-9gag-fun.9cache.com/photo/a3Q5VW5_460s.jpg",
        "https://media.moddb.com/images/members/5/4550/4549205/duck.jpg",
        "https://www.kibrispdr.org/dwn/5/random-pic.jpg"
    };

    public async Task<Image> GetImage(int id)
    {
        Image img = new();
        img.Url = ImageUrls[id - 1];
        img.Metadata = await GetMetadata(img.Url);

        return img;
    }

    async public Task<ImageMetadata> GetMetadata(string url)
    {
        if (!OperatingSystem.IsOSPlatform("windows"))
            throw new HttpRequestException("Please buy windows", null, System.Net.HttpStatusCode.NotAcceptable);

        var response = await new HttpClient().GetAsync(url);
        var stream = await response.Content.ReadAsStreamAsync();
        var imageFromStream = System.Drawing.Image.FromStream(stream);
        stream.Close();

        var metadata = new ImageMetadata();
        metadata.Width = imageFromStream.Width;
        metadata.Height = imageFromStream.Height;
        metadata.Size = FormatByteSize(response.Content.ReadAsByteArrayAsync().Result.Length);

        return metadata;
    }

    private string FormatByteSize(long size) => String.Format("{0:#,###} bytes", size);

}
