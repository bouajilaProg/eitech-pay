using System.Threading.Tasks;
using Back.Models.LicenceRelated;

namespace Back.Modules.LicenceModule.Services
{
    public interface ILicenceOrderService
    {
        Task CreateAsync(LicenceOrder order);
    }
}
