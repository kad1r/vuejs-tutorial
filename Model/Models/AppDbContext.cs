using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Model.Models
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
		{
		}

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<ActivityType> ActivityTypes { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductActivity> ProductActivities { get; set; }
		public DbSet<WareHouse> WareHouses { get; set; }
	}
}
