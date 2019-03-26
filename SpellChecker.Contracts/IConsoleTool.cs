namespace SpellChecker.Contracts
{
    public interface IConsoleTool
    {
        string NewLine { get; }

        void WriteLine(string text, bool isAddNewLine = true);
    }
}
