namespace Detector.Infrastructure.Commands
{
    public class AddImage : ICommand
    {
        public byte[] ImageOriginal { get; set; }
    }
}