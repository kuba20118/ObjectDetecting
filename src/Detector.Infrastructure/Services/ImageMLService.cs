﻿using Detector.Infrastructure.ImageFileHelpers;
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
using Detector.Infrastructure.Dtos;

namespace Detector.Infrastructure.Services
{
    public class ImageMLService : IImageMLService
    {
        private readonly string _imagesTmpFolder;
        private readonly IObjectDetectionService _objectDetectionService;
        private readonly IImageService _imageService;

        private string base64String = string.Empty;
        private long elapsedMs = 0;
        public ImageMLService(IObjectDetectionService objectDetectionService, IImageService imageService) //When using DI/IoC (IImageFileWriter imageWriter)
        {
            _imageService = imageService;
            _objectDetectionService = objectDetectionService;
            _imagesTmpFolder = Path.GetFullPath(@"ImagesTemp");
        }



        public async Task IdentifyObjects(IFormFile imageFile, Guid id)
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
                ////
                bool exists = System.IO.Directory.Exists(_imagesTmpFolder);
                if (!exists)
                    System.IO.Directory.CreateDirectory(_imagesTmpFolder);
                //save image to a path
                image.Save(imageFilePath, ImageFormat.Jpeg);

                ///
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
                elapsedMs = watch.ElapsedMilliseconds;

                result.ElapsedTime = elapsedMs;
                result.imageStringOriginal = imageData;

                await _imageService.AddImage(id, result);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private Result DetectAndPaintImage(ImageInputData imageInputData, string imageFilePath)
        {
            //Predict the objects in the image
            _objectDetectionService.DetectObjectsUsingModel(imageInputData);
            var img = _objectDetectionService.DrawBoundingBox(imageFilePath);

            using (MemoryStream m = new MemoryStream())
            {
                img.Image.Save(m, img.Image.RawFormat);
                byte[] imageBytes = m.ToArray();

                var result = new Result { imageStringProcessed = imageBytes, Description = img.Description, ElapsedTime = elapsedMs };

                return result;
            }
        }
    }
}
