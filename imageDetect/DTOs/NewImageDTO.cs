using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imageDetect.DTOs
{
    public class NewImageDTO
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public DateTime AddTime { get; set; }

        public NewImageDTO()
        {
            AddTime = DateTime.Now;
        }
    }
}
