using System.Data.Entity;
using LineCat.Web.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LineCat.Web.Repository
{
    //enable-migrations //-contexttypename [LineCatDb]
    //add-migration [InitialDB]


    public class LineCatDb : DbContext
    {
        public LineCatDb() : base("HuoxingPlatform") { }

        public DbSet<Mall> Mall { get; set; }
        public DbSet<MallRule> MallRule { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PriceHistory> PriceHistory { get; set; }

        public DbSet<UserProduct> UserProduct { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //防止表中的数据以复数(Students,Courses,Enrollments)命名
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}