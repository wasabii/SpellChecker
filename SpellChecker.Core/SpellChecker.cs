using System.Threading.Tasks;
using SpellChecker.Contracts;
using Autofac;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a top level spell checker that is used by clients, it internally manages
    /// several spell checkers that it uses to evaluate whether a word is spelled correctly
    /// or not.
    /// </summary>
    public class SpellChecker :
        ISpellChecker
    {

        readonly ISpellChecker[] spellCheckers;
        /// AF container
        private IContainer Container { get; set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public SpellChecker()
        {

            //register the IoC components
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            builder //get everything that implements ISpellChecker
                .RegisterAssemblyTypes(assembly)
                .Except<SpellChecker>() //avoid infinite loop
                .Where(t => typeof(ISpellChecker).IsAssignableFrom(t))
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
            Container = builder.Build();
            spellCheckers = Container.Resolve<IEnumerable<ISpellChecker>>().ToArray();
        }

        /// <summary>
        /// Iterates through all the internal spell checkers and returns false if any one of them finds a word to be
        /// misspelled
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns>True if all spell checkers agree that a word is spelled correctly, false otherwise</returns>
        public async Task<bool> Check(string word)
        {
            var result = false;
            foreach (var checker in spellCheckers)
                if (await checker.Check(word))
                    result = true; //sticky true
            return result;
        }

    }

}
