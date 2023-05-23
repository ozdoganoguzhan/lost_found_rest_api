using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace LostFound.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        [HttpPost("/upload/post-image")]
        public ActionResult UploadPostImage(IFormFile image) {
            if (image.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ASUS\\Desktop\\React\\esya-app-ts\\src\\images\\posts", image.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                return Ok(new { status = true, message = "Image posted successfully." });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/delete/post-image")]
        public ActionResult DeletePostImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return BadRequest();
            string path = Path.Combine("C:\\Users\\ASUS\\Desktop\\React\\esya-app-ts\\src\\images\\posts", imageName);
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                return NotFound();
            }
            file.Delete();
            return Ok(new { status = true, imageName = imageName });
        }
        [HttpPost("/upload/user-image")]
        public ActionResult UploadUserImage(IFormFile image)
        {
            if (image.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\ASUS\\Desktop\\React\\esya-app-ts\\src\\images\\profiles", image.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                return Ok(new { status = true, message = "Image posted successfully." });
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("/delete/user-image")]
        public ActionResult DeleteUserImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return BadRequest();
            string path = Path.Combine("C:\\Users\\ASUS\\Desktop\\React\\esya-app-ts\\src\\images\\profiles", imageName);
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                return NotFound();
            }
            file.Delete();
            return Ok(new { status = true, imageName = imageName });
        }
        /*        [HttpPost("/upload/post-image")]
                public async Task<IActionResult> UploadImage()
                {

                    var file = Request.Content.ReadAsStringAsync();
                    var path = Path.Combine("C:\\Users\\ASUS\\Desktop\\React\\esya-app-ts\\src");
                    var pathPath = Path.Combine(path, fileName);
                    string errorMsg = string.Empty;

                    try
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        var stream = new FileStream(pathPath, FileMode.Create);
                        await upload.CopyToAsync(stream);
                        await stream.FlushAsync();
                        return new JsonResult(new { uploaded:1 });
                    }
                }*/
    }
}
