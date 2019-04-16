using Microsoft.EntityFrameworkCore;

namespace OpbeansDotnet.Data
{
	public class OpbeansDbContext : DbContext
	{
		public OpbeansDbContext()
		{
		}

		public OpbeansDbContext(DbContextOptions<OpbeansDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Customers> Customers { get; set; }
		public virtual DbSet<OrderLines> OrderLines { get; set; }
		public virtual DbSet<Orders> Orders { get; set; }
		public virtual DbSet<ProductTypes> ProductTypes { get; set; }
		public virtual DbSet<Products> Products { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=opbeans.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

			modelBuilder.Entity<Customers>(entity =>
			{
				entity.ToTable("customers");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.ValueGeneratedNever();

				entity.Property(e => e.Address)
					.IsRequired()
					.HasColumnName("address")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.City)
					.IsRequired()
					.HasColumnName("city")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.CompanyName)
					.IsRequired()
					.HasColumnName("company_name")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.Country)
					.IsRequired()
					.HasColumnName("country")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.Email)
					.IsRequired()
					.HasColumnName("email")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.FullName)
					.IsRequired()
					.HasColumnName("full_name")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.PostalCode)
					.IsRequired()
					.HasColumnName("postal_code")
					.HasColumnType("VARCHAR(1000)");
			});

			modelBuilder.Entity<OrderLines>(entity =>
			{
				entity.ToTable("order_lines");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.ValueGeneratedNever();

				entity.Property(e => e.Amount).HasColumnName("amount");

				entity.Property(e => e.OrderId).HasColumnName("order_id");

				entity.Property(e => e.ProductId).HasColumnName("product_id");
			});

			modelBuilder.Entity<Orders>(entity =>
			{
				entity.ToTable("orders");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.ValueGeneratedNever();

				entity.Property(e => e.CreatedAt)
					.IsRequired()
					.HasColumnName("created_at")
					.HasColumnType("VARCHAR(4000)");

				entity.Property(e => e.CustomerId).HasColumnName("customer_id");
			});

			modelBuilder.Entity<ProductTypes>(entity =>
			{
				entity.ToTable("product_types");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.ValueGeneratedNever();

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasColumnType("VARCHAR(1000)");
			});

			modelBuilder.Entity<Products>(entity =>
			{
				entity.ToTable("products");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.ValueGeneratedNever();

				entity.Property(e => e.Cost).HasColumnName("cost");

				entity.Property(e => e.Description)
					.IsRequired()
					.HasColumnName("description")
					.HasColumnType("VARCHAR(4000)");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.SellingPrice).HasColumnName("selling_price");

				entity.Property(e => e.Sku)
					.IsRequired()
					.HasColumnName("sku")
					.HasColumnType("VARCHAR(1000)");

				entity.Property(e => e.Stock).HasColumnName("stock");

				entity.Property(e => e.TypeId).HasColumnName("type_id");
			});
		}
	}
}
