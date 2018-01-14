using System.Linq;
using HospitalProject.DataAccess.Entities;

namespace HospitalProject.DataAccess.Repositories
{
    public class HospitalRepository
    {
        private readonly HospitalDbContext _dbContext;

        public HospitalRepository()
        {
        }

        public HospitalRepository(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual Hospital GetByName(string name)
        {
            return _dbContext.Set<Hospital>().SingleOrDefault(h => h.Name == name);
        }

        public virtual Hospital Add(Hospital hospital)
        {
            return _dbContext.Set<Hospital>().Add(hospital);
        }
    }
}
