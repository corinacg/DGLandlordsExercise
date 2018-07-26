using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using LandlordPropertiesManager.Domain;

namespace LandlordPropertiesManager.Services
{
    class LandlordRepository : ILandlordRepository
    {
        private DgCodeTestEntities dbContext = new DgCodeTestEntities();

        public Task AddPropertyAsync(Property property)
        {
            dbContext.Properties.Add(property);
            return dbContext.SaveChangesAsync();
        }

        public Task<List<Landlord>> GetLandlordsAsync()
        {
            return dbContext.Landlords.ToListAsync();
        }

        public Task<List<Property>> GetPropertiesAsync(int landlordId)
        {
            return dbContext.Properties.Where(p=> p.LandlordId == landlordId).ToListAsync();
        }

        public Task UpdatePropertyAsync(Property property)
        {
            return dbContext.SaveChangesAsync();
        }

        public void UndoPropertyChanges(Property property)
        {
            dbContext.Entry(property).CurrentValues.SetValues(dbContext.Entry(property).OriginalValues);
            dbContext.Entry(property).State = EntityState.Unchanged;
        }
    }
}
