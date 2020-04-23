using System;

namespace Detector.Core.Domain
{
    public class Image
    {
        public Guid Id { get; protected set; }
        public Guid StatsId { get; set; }
        public byte[] ImageOriginal { get; protected set; }
        public byte[] ImageProcessed { get; protected set; }
        public DateTime Added { get; protected set; }

        protected Image()
        {
        }

        public Image(Guid guid, byte[] image)
        {
            Id = guid;
            SetOriginalImage(image);
            Added = DateTime.UtcNow;
        }

        void SetOriginalImage(byte[] image)
        {
            if(image == null || image.LongLength <= 0)
            {
                throw new Exception("Niepoprawny obraz");
            }

            if(ImageOriginal == image)
            {
                return;
            }

            ImageOriginal = image;
        }
    }
}