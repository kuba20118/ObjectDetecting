using System;
using System.Collections.Generic;

namespace Detector.Core.Domain
{
    public class Image
    {
        public Guid Id { get; protected set; }
        public Guid StatsId { get; set; }
        public byte[] ImageOriginal { get; protected set; }
        public byte[] ImageProcessed { get; protected set; }
        public List<string> Description { get; set; }
        public long ElapsedTime { get; set; }
        public DateTime Added { get; protected set; }

        protected Image()
        {
        }

        public Image(Guid guid, byte[] imageOriginal, byte[] imageProcessed, List<string> description, long time)
        {
            Id = guid;
            SetOriginalImage(imageOriginal);
            SetProcessedImage(imageProcessed);
            SetDescriptionList(description);
            SetElapsedTime(time);
            Added = DateTime.UtcNow;
        }

        private void SetElapsedTime(long time)
        {
            if (time > 0)
                ElapsedTime = time;
            else
                ElapsedTime = 0;
        }

        private void SetDescriptionList(List<string> description)
        {
            Description = description;
        }

        private void SetProcessedImage(byte[] image)
        {
            ImageProcessed = SetImage(image);
        }

        void SetOriginalImage(byte[] image)
        {
            ImageOriginal = SetImage(image);
        }

        private byte[] SetImage(byte[] image)
        {
            if (image == null || image.LongLength <= 0)
            {
                throw new DomainException(ErrorCodes.InvalidImage, "Niepoprawny obraz");
            }

            return image;

        }
    }
}