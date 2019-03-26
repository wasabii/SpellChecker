using System;
using System.Reflection;
using Autofac;

namespace SpellChecker.Console.Bootstrap
{
    public class AutofacFactory
    {
        public static ContainerBuilder CreateContainerBuilder(Assembly bootstrapAssembly = null)
        {
            var returnValue = new ContainerBuilder();

            returnValue.RegisterAssemblyModules(bootstrapAssembly);

            return returnValue;
        }

		public static ContainerBuilder RegisterAll
        (
            Assembly bootstrapAssembly = null
        )
        {
            return RegisterAll(CreateContainerBuilder(bootstrapAssembly), bootstrapAssembly);
        }

        public static ContainerBuilder RegisterAll
        (
            ContainerBuilder builder,
            Assembly bootstrapAssembly = null
        )
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var thisAssembly = Assembly.GetAssembly(typeof(AutofacFactory));

            return builder
                    .RegisterProgram()
                    .RegisterConsoleTool()
                    .RegisterCheckerService()
                    .RegisterSpellCheckers();
        }
    }
}
