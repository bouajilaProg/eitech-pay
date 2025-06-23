
using System.Threading.Tasks;
using Back.Modules.SubscriptionModule.Dtos;

namespace Back.Modules.SubscriptionModule.Services
{
    public interface ISubscriptionPublicService
    {
        Task<SubscriptionDetailsDto?> GetSubscriptionDetailsAsync(string subscriptionId);
    }
}
