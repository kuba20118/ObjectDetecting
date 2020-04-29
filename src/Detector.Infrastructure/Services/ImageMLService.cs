using Detector.Infrastructure.ImageFileHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Detector.ML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Detector.Infrastructure.Services
{
    public class ImageMLService : IImageMLService
    {
        private readonly string _imagesTmpFolder;
        private readonly IObjectDetectionService _objectDetectionService;
        private readonly IImageFileWriter _imageWriter;

        private string base64String = string.Empty;
        public ImageMLService(IObjectDetectionService ObjectDetectionService, IImageFileWriter imageWriter) //When using DI/IoC (IImageFileWriter imageWriter)
        {
            _imageWriter = imageWriter;
            _objectDetectionService = ObjectDetectionService;
            _imagesTmpFolder = Path.GetFullPath(@"../Detector.Infrastructure/ImagesTemp");
            System.Console.WriteLine(_imagesTmpFolder);
        }


        public class Result
        {
            public string imageString { get; set; }
        }

        public async Task<Result> IdentifyObjects(IFormFile imageFile)
        {
            try
            {
                MemoryStream imageMemoryStream = new MemoryStream();
                await imageFile.CopyToAsync(imageMemoryStream);
                //Check that the image is valid
                byte[] imageData = imageMemoryStream.ToArray();
                //Convert to Image
                Image image = Image.FromStream(imageMemoryStream);
                string fileName = string.Format("{0}.Jpeg", image.GetHashCode());
                string imageFilePath = Path.Combine(_imagesTmpFolder, fileName);
                //save image to a path
                image.Save(imageFilePath, ImageFormat.Jpeg);
                //Convert to Bitmap
                Bitmap bitmapImage = (Bitmap)image;


                //Measure execution time
                var watch = System.Diagnostics.Stopwatch.StartNew();

                //Set the specific image data into the ImageInputData type used in the DataView
                ImageInputData imageInputData = new ImageInputData { Image = bitmapImage };

                //Detect the objects in the image                
                var result = DetectAndPaintImage(imageInputData, imageFilePath);

                //Stop measuring time
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                byte[] imageBytes = Convert.FromBase64String(result.imageString);
                MemoryStream ms = new MemoryStream(imageBytes);
                System.Console.WriteLine(imageBytes.Count());
                Image xx = Image.FromStream(ms, true, true);
                xx.Save(imageFilePath + "x" + ImageFormat.Jpeg);

                return result;
            }
            catch (Exception e)
            {
            }

            return null;
        }

        private Result DetectAndPaintImage(ImageInputData imageInputData, string imageFilePath)
        {
            //Predict the objects in the image
            _objectDetectionService.DetectObjectsUsingModel(imageInputData);
            var img = _objectDetectionService.DrawBoundingBox(imageFilePath);

            using (MemoryStream m = new MemoryStream())
            {
                img.Save(m, img.RawFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes);
                var result = new Result { imageString = base64String };
                return result;
            }
        }
    }
}
