using Microsoft.Extensions.DependencyInjection;

using SpellChecker.Contracts;

namespace SpellChecker.Core.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IHttpClient, Core.HttpClient>();
            services.AddScoped<Core.SpellChecker>();
            services.AddScoped<ISpellChecker, MnemonicSpellCheckerIBeforeE>();
            services.AddScoped<ISpellChecker, DictionaryDotComSpellChecker>();

            services.AddHttpClient<IHttpClient, Core.HttpClient>();

            return services;
        }
    }
}
