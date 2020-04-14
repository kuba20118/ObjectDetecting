using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using imageDetect.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace imageDetect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        public ImageController()
        {

        }

        [HttpPost("add")]
        public async Task<IActionResult> Upload([FromForm]NewImageDTO image)
        {
           //var uploads = Path.Combine("x", "uploads");
            var file = image.File;
            var ext = Path.GetExtension(image.File.FileName);
            if (file.Length > 0)
            {
                //file.FileName = Guid.NewGuid().ToString();
                var filePath = Path.Combine("xx", Guid.NewGuid().ToString() + ext);
                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return Ok();
        }
    }
}