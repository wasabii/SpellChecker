using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SpellChecker.Contracts;
using SpellChecker.Core.Extension;

namespace SpellChecker.Core
{
    public class SpellCheckerService : ISpellCheckerService
    {
        public SpellCheckerService(ISpellCheckerCore spellChecker, IConsoleTool consoleTool)
        {
            SpellChecker = spellChecker;
            ConsoleTool = consoleTool;
        }

        private ISpellChecker SpellChecker { get; set; }

        private IConsoleTool ConsoleTool { get; set; }

        public void RunSpellCheck(string sentence)
        {
            ConsoleTool.WriteLine(ConsoleTool.NewLine + "Spell Check Starting...");
            CheckSentence(sentence);
            ConsoleTool.WriteLine(ConsoleTool.NewLine + "Spell Check Complete", false);
        }

        private void CheckSentence(string sentence)
        {
            var checkerTasks = GetCheckerTasks(sentence);
            var misspelledWords = new List<string>();

            ConsoleTool.WriteLine("Misspelled Words:", false);

            while (checkerTasks.Count() > 0)
            {
                var index = Task.WaitAny(checkerTasks.ToArray());
                var result = checkerTasks[index].Result;

                if (!result.IsCorrect && !misspelledWords.Contains(result.WordChecked))
                {
                    ConsoleTool.WriteLine($"\t{result.WordChecked}", false);
                    misspelledWords.Add(result.WordChecked);
                }

                checkerTasks.RemoveAt(index);
            }
        }

        private List<Task<SpellCheckResult>> GetCheckerTasks(string sentence)
        {
            var returnValue = new List<Task<SpellCheckResult>>();
            var words = sentence.GetWordsFromSentence();

            words.Each(w => returnValue.Add(SpellChecker.CheckAsync(w)));

            return returnValue;
        }
    }
}
