using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rick_and_Morty.Application.Responses;
using System.IO;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Controllers
{
    public class ImageUploadController : ApiControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string path = Path.Combine("wwwroot/images/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var fileStream = new FileStream(path + file.FileName, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(new Response<bool>(true));
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string name)
        {
            string path = Path.Combine("wwwroot/images/") + name;

            var file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            return Ok(new Response<bool>(true));
        }
    }
}
