using System.Data.Entity.ModelConfiguration;

namespace HospitalProject.DataAccess.Entities
{
    public class HospitalMapping : EntityTypeConfiguration<Hospital>
    {
        public HospitalMapping()
        {
            HasKey(k => k.Id);
        }
    }
}