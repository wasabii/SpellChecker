using System.Threading.Tasks;

namespace SpellChecker.Contracts
{
    public interface ISpellChecker
    {
        Task<SpellCheckResult> CheckAsync(string word);
    }
}
