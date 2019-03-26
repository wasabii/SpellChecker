using SpellChecker.Contracts;

namespace SpellChecker.Core.Utility
{
    public class ConsoleTool : IConsoleTool
    {
        public string NewLine => "\r\n";

        public void WriteLine(string text, bool isAddNewLine = true)
        {
            if (isAddNewLine)
                text += NewLine;

            System.Console.WriteLine(text);
        }
    }
}
