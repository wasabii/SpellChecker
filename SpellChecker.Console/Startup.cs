using System;
using SpellChecker.Console.Bootstrap;

using SpellChecker.Core;
using SpellChecker.Core.Extension;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SpellChecker.Console
{
	public class Startup
	{
		public Startup()
		{
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; private set; }

        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var spellCheckerConfig = services.ConfigurePoco<SpellCheckerConfig>(Configuration.GetSection("SpellCheckerConfig"));
            var builder = AutoBootstrapper.Bootstrap();

            builder.Populate(services);

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

		public void Configure() { }
	}
}