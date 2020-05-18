using System.Threading.Tasks;

namespace Detector.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}