namespace JsExerciseAPI.Controllers;

[Route("api/image")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly ImageService _imageService;
    private readonly Random _rnd = new();

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }

    /// <summary>
    /// Hämtar en bild och metadata om bilden
    /// </summary>
    /// <param name="id"> En siffra ett eller högre </param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Image>> GetImage(int id)
    {
        var image = await _imageService.GetImage(id);

        if(image == null)
            return NotFound();

        return Ok(image);
    }

    /// <summary>
    /// Hämtar en bild och metadata om bilden, men tar mellan 2 och 10 sek extra att genomföra
    /// </summary>
    /// <param name="id"> En siffra ett eller högre </param>
    /// <returns></returns>
    [HttpGet("{id}/throttle")]
    public async Task<ActionResult<Image>> GetImageThrottled(int id)
    {
        Thread.Sleep(_rnd.Next(2, 10) * 1000);

        return await GetImage(id);
    }

    /// <summary>
    /// Lägger till en ny bild till tjänsten
    /// </summary>
    /// <param name="url">den nya url:en</param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<ActionResult<Image>> PostImage(string url)
    {
        var imageLocation = $"api/image/{_imageService.ImageCount+1}";
        var newImage = await _imageService.AddImage(url);

        return new CreatedResult(imageLocation, newImage);
    }
}
