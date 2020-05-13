using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Detector.Infrastructure.Commands
{
    public class AddImage : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IFormFile ImageOriginal { get; set; }
    }
}