namespace SpellChecker.Contracts
{

    /// <summary>
    /// The base interface which defines how spell checkers work
    /// </summary>
    public interface ISpellChecker
    {

        /// <summary>
        /// All SpellCheckers will need to implement this methed, which returns <c>true</c> for words that are spelled
        /// correctly and <c>false</c> otherwise.
        /// </summary>
        /// <param name="word">The word that needs to be checked</param>
        /// <returns><c>true</c>, if the word is spelled correctly, <c>false</c> otherwise</returns>
        bool Check(string word);
    }

}
