using Ninject.Modules;
using SpellChecker.Contracts;

namespace SpellChecker.Core
{
    public class SpellCheckerModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ISpellChecker)).To(typeof(MnemonicSpellCheckerIBeforeE)).Named("IBeforeE");
            Bind(typeof(ISpellChecker)).To(typeof(DictionaryDotComSpellChecker)).Named("Dictionary");
        }
    }
}
