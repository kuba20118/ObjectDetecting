using System;

namespace Detector.Infrastructure.Dtos
{
    public class ImageDto
    {
        public Guid Id { get;  set; }
        public byte[] ImageOriginal { get;  set; }
    }
}