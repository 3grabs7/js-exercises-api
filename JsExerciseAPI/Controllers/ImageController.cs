namespace JsExerciseAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly ImageService _imageService;
    private Random _rnd = new();

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Image>> GetImage(int id)
    {
        if (!Enumerable.Range(1, _imageService.ImageUrls.Length).Contains(id)) return NotFound();

        return Ok(await _imageService.GetImage(id));
    }

    [HttpGet("{id}/throttle")]
    public async Task<ActionResult<Image>> GetImageThrottled(int id)
    {
        Thread.Sleep(_rnd.Next(2, 10) * 1000);

        return await GetImage(id);
    }
}
