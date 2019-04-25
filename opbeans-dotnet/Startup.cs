using System.Linq;
using AutoMapper;
using Elastic.Apm.All;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpbeansDotnet.Data;
using OpbeansDotnet.Model;

//using OpbeansDotnet.Data;

namespace OpbeansDotnet
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			var context = new OpbeansDbContext();
			context.Database.EnsureCreated();
			if (!context.Products.Any() || !context.Customers.Any() || !context.Orders.Any())
				Seed.SeedDb(context);

			services.AddDbContext<OpbeansDbContext>
				(options => options.UseSqlite(@"Data Source=opbeans.db"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseElasticApm();

			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Orders, Order>()
					.ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
					.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName));

				cfg.CreateMap<Products, Product>()
					.ForMember(dest => dest.Type_id, opt => opt.MapFrom(src => src.Type.Id))
					.ForMember(dest => dest.Type_name, opt => opt.MapFrom(src => src.Type.Name));
				cfg.CreateMap<ProductTypes, ProductType>();
			});

			app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
