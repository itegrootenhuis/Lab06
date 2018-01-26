using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab06
{
    class Program
    {
        private static string response;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the pig latin translator.");
            GetUserInput();
            WriteResponse(response);
        }

        private static void WriteResponse(string response)
        {
            Console.WriteLine(response + "\n");
            QuitConsoleApp();
        }

        private static void GetUserInput()
        {
            response = "";
            Console.WriteLine("Enter in text you want translated to Pig Latin:");
            string userInput = Console.ReadLine();

            SplitUpStringIntoSingleWords(userInput.ToLower());
        }

        private static void SplitUpStringIntoSingleWords(string userInput)
        {
            char[] punctuation = userInput.Where(Char.IsPunctuation).Distinct().ToArray();
            IEnumerable<string> words = userInput.Split().Select(x => x.Trim(punctuation));
            List<string> wordsList = words.ToList<string>();

            FindVowelOrConsonantWords(wordsList);
        }

        private static void FindVowelOrConsonantWords(List<string> words)
        {
            List<string> vowels = new List<string>();
            vowels.Add("a");
            vowels.Add("e");
            vowels.Add("i");
            vowels.Add("o");
            vowels.Add("u");

            string lastWord = words.Last();

            foreach (string word in words)
            {
                if (vowels.Contains(word[0].ToString()))
                {
                    TranslateVowelWord(word);
                }
                else if (!vowels.Contains(word[0].ToString()))
                {
                    TranslateConsonantWord(word, vowels);
                }

                if (word == lastWord)
                    WriteResponse(response);
            }
        }

        private static void TranslateConsonantWord(string word, List<string> vowels)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (vowels.Contains(word[i].ToString()))
                {
                    string subStringBegining = word.Substring(0, i);
                    string subStringEnd = word.Substring(i);
                    string translatedConsonant = string.Format("{0}{1}ay ", subStringEnd, subStringBegining);
                    BuildResponse(translatedConsonant);
                    break;
                }
            }
        }

        private static void TranslateVowelWord(string word)
        {
            string translatedVowel = string.Format("{0}way ", word);

            BuildResponse(translatedVowel);
        }

        private static void BuildResponse(string translatedword)
        {
            response += translatedword;
        }

        private static void QuitConsoleApp()
        {
            Console.WriteLine("\n\nPress R to repeat or any other key to exit the app.");
            ConsoleKeyInfo quitKey = Console.ReadKey();

            if (quitKey.Key.ToString().ToLower() == "r")
            {
                Console.Clear();
                GetUserInput();
            }
        }
    }
}
