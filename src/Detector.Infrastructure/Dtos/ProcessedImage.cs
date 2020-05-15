using System.Collections.Generic;
using System.Drawing;

namespace Detector.Infrastructure.Dtos
{
    public class ProcessedImage
    {
        public Image Image { get; set; }
        public List<string> Description { get; set; }
    }
}