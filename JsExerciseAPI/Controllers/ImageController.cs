using Microsoft.AspNetCore.Mvc;

namespace JsExerciseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private string[] _imageUrls = new string[] {
            "https://img-9gag-fun.9cache.com/photo/a3Q5VW5_460s.jpg",
            "https://media.moddb.com/images/members/5/4550/4549205/duck.jpg",
            "https://www.kibrispdr.org/dwn/5/random-pic.jpg"
        };

        private Random _rnd = new();

        [HttpGet("{id}")]
        public ActionResult GetImage(int id)
        {
            if (!Enumerable.Range(0, _imageUrls.Length).Contains(id)) return NotFound();

            Thread.Sleep(_rnd.Next(2, 10) * 1000);

            return Ok(new { url = _imageUrls[(int)id] });
        }


    }
}
