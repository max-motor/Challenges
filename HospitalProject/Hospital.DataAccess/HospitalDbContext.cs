using System.Data.Entity;
using HospitalProject.DataAccess.Repositories;

namespace HospitalProject.DataAccess
{
    /// <summary>
    /// Database context class
    /// </summary>
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() : base("HospitalProject.Messaging.Server.Properties.Settings.ConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<HospitalDbContext>());
            Hospitals = new HospitalRepository(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(HospitalDbContext).Assembly);
        }

        /// <summary>
        /// Hospital repository
        /// </summary>
        public virtual HospitalRepository Hospitals { get; }
    }
}
