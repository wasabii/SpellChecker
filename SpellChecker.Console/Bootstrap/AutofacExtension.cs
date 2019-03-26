using SpellChecker.Contracts;
using SpellChecker.Core;

using SpellChecker.Core.Checker;
using SpellChecker.Core.Utility;

using Autofac;

namespace SpellChecker.Console.Bootstrap
{
    public static class AutofacExtension
    {
        public static ContainerBuilder RegisterProgram(this ContainerBuilder builder)
        {
            builder
                .RegisterType<Program>()
                .As<Program>()
                .SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterConsoleTool(this ContainerBuilder builder)
        {
            builder
                .RegisterType<ConsoleTool>()
                .As<IConsoleTool>()
                .SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterCheckerService(this ContainerBuilder builder)
        {
            builder
                .RegisterType<SpellCheckerService>()
                .As<ISpellCheckerService>()
                .SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterSpellCheckers(this ContainerBuilder builder)
        {
            builder
                .RegisterType<MnemonicSpellCheckerIBeforeE>()
                .As<ISpellChecker>()
                .Named<ISpellChecker>("MnemonicSpellCheckerIBeforeE")
                .SingleInstance();

            builder
                .RegisterType<DictionaryDotComSpellChecker>()
                .As<ISpellChecker>()
                .Named<ISpellChecker>("DictionaryDotComSpellChecker")
                .SingleInstance();

            builder
                .RegisterType<Core.Checker.SpellChecker>()
                .As<ISpellCheckerCore>()
                .UsingConstructor(typeof(ISpellChecker[]))
                .Named<ISpellCheckerCore>("SpellChecker")
                .SingleInstance();

            return builder;
        }
    }
}
