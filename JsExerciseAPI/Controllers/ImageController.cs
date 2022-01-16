using Microsoft.AspNetCore.Mvc;

namespace JsExerciseAPI.Controllers
{
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
        public ActionResult GetImage(int id)
        {
            if (!Enumerable.Range(1, _imageUrls.Length).Contains(id)) return NotFound();

            return Ok(new { url = _imageUrls[(int)id - 1] });
        }

        [HttpGet("{id}/throttle")]
        public ActionResult GetImageThrottled(int id)
        {
            Thread.Sleep(_rnd.Next(2, 10) * 1000);

            return GetImage(id);
        }


    }
}
