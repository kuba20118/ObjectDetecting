using System.Net.Http;
using Detector.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections;
using Detector.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Detector.Tests.EndToEnd.Controllers
{
    public class ImagesControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        
        public ImagesControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task test()
        {      
            var response = await _client.GetAsync("images/" + "8b289ef3-0d5b-4722-9ef8-2733f3b0e68a");
            response.EnsureSuccessStatusCode();
            
            var responseString = await response.Content.ReadAsStringAsync();
            var images = JsonConvert.DeserializeObject<ImageDto>(responseString);

            images.Id.Should().Equals("8b289ef3-0d5b-4722-9ef8-2733f3b0e68a");
            
        }
    }
}