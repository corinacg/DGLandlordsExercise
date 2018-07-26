using System.Collections.Generic;
using System.Threading.Tasks;
using LandlordPropertiesManager.Domain;

namespace LandlordPropertiesManager.Services
{
    interface ILandlordRepository
    {
        Task<List<Landlord>> GetLandlordsAsync();
        Task<List<Property>> GetPropertiesAsync(int landlordId);
        Task AddPropertyAsync(Property property);
        Task UpdatePropertyAsync(Property property);
    }
}
