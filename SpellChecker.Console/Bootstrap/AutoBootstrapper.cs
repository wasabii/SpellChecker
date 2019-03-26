using System;
using System.Reflection;

using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace SpellChecker.Console.Bootstrap
{
	public sealed class AutoBootstrapper
	{
        public static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            var startup = new Startup();

            return startup.ConfigureServices(services);
        }

        public static ContainerBuilder Bootstrap()
		{
			return CreateContainerBuilder();
		}

		private static ContainerBuilder CreateContainerBuilder()
		{
			var returnValue = default(ContainerBuilder);
			var bootstrapAssembly = Assembly.GetAssembly(typeof(AutoBootstrapper));

            returnValue = AutofacFactory.RegisterAll
            (
                bootstrapAssembly
            );

            return returnValue;
		}
    }
}
