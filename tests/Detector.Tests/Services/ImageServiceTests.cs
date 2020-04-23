using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using Detector.Core.Repositories;
using Detector.Infrastructure.Services;
using AutoMapper;
using System;
using NUnit;
using Detector.Core.Domain;

namespace Detector.Tests.Services
{
    [TestFixture]
    public class ImageServiceTests
    {
        [SetUp]
        public void Setup()
        {
            
        }


        [Test]
        public async Task addAsync_should_invoke_addAsync_on_repository_once()
        {
            byte[] imgArray =new byte[] {0,100,255,1,0};
            var guid = Guid.NewGuid();
            var imageRepositoryMock = new Mock<IImageRepository>();
            var mapperMock = new Mock<IMapper>();
            var imageService = new ImageService(imageRepositoryMock.Object, mapperMock.Object);
            await imageService.AddImage(imgArray);

            imageRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Image>()), Times.Once);
        
        }
    }
}