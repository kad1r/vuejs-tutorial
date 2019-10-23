using Microsoft.EntityFrameworkCore;

namespace Model.Models
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
		{
			ChangeTracker.LazyLoadingEnabled = false;
			ChangeTracker.AutoDetectChangesEnabled = false;
		}

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<ActivityType> ActivityTypes { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductActivity> ProductActivities { get; set; }
		public DbSet<WareHouse> WareHouses { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<MenuAuthorization> MenuAuthorizations { get; set; }
	}
}
