using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using AutoMapper;
using Elastic.Apm.NetCoreAll;
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
			app.UseAllElasticApm(Configuration);

			// Read config environment variables used to demonstrate Distributed Tracing
			// For more info see: https://github.com/elastic/apm-integration-testing/issues/196
			app.Use(async (context, next) =>
			{
				if (context.Request.Path.HasValue && KnownApis.Contains(context.Request.Path.Value))
				{
					var opbeansServices = Environment.GetEnvironmentVariable("OPBEANS_SERVICES");
					if (!string.IsNullOrEmpty(opbeansServices))
					{
						var allServices = opbeansServices.Split(',')?.Select(n => n.ToLower())
							.Where(n => n != "opbeans-dotnet")
							.ToList();

						if (allServices != null && allServices.Any())
						{
							var dtProbabilityEnvVar = Environment.GetEnvironmentVariable("OPBEANS_DT_PROBABILITY");

							if (!double.TryParse(dtProbabilityEnvVar, NumberStyles.Float, CultureInfo.InvariantCulture,
								out var dtProbability))
								dtProbability = 0.5;

							var random = new Random(DateTime.UtcNow.Millisecond);

							if (random.NextDouble() > dtProbability)
							{
								await next.Invoke();
								return;
							}

							var winnerService = allServices[random.Next(allServices.Count)];

							if (!winnerService.StartsWith("http"))
								winnerService = $"http://{winnerService}";
							if (winnerService.EndsWith("/"))
								winnerService = winnerService.Substring(0, winnerService.Length - 1);

							var httpClient = new HttpClient();

							try
							{
								await httpClient.GetAsync(
									$"{winnerService}:{context.Request.Host.Port}/{context.Request.Path.Value}");
							}
							catch
							{
								//Ignore error, it'll be captured by the agent, but there is nothing to do.
							}
						}
					}
				}

				await next.Invoke();
			});

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

		private List<string> KnownApis =>
			new List<string>
			{
				"/api/",
				"/api/stats",
				"/api/products",
				"/api/products/",
				"/api/products/top",
				"/api/products/customers",
				"/api/types",
				"/api/types/",
				"/api/customers",
				"/api/customers/",
				"/api/orders",
				"/api/orders/"
			};
	}
}
