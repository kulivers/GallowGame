using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GallowGame
{
    class HiddenChar
    {
        public bool IsOpen { get; set; }
        public char Value { get; set; }

        public HiddenChar(char value)
        {
            Value = value;
            IsOpen = false;
        }
    }

    public class Garrow
    {
        private string[] _words;

        private string _hiddenWordStr;
        private List<HiddenChar> _hiddenWord = new List<HiddenChar>();
        private int _counts = 6;

        bool OpenChar(char c)
        {
            bool isTrueKey = false;
            foreach (var hiddenChar in _hiddenWord)
            {
                if (hiddenChar.Value == c)
                {
                    hiddenChar.IsOpen = true;
                    isTrueKey = true;
                }
            }

            return isTrueKey;
        }

        string GetStarsWord()
        {
            StringBuilder result = new StringBuilder();
            foreach (var hiddenChar in _hiddenWord)
            {
                if (hiddenChar.IsOpen)
                {
                    result.Append(hiddenChar.Value);
                }
                else
                {
                    result.Append('*');
                }
            }


            return result.ToString();
        }

        public Garrow(string dictName = "WordsStockRus.txt")
        {
            _words = new DictReader(dictName).Words;
            _hiddenWordStr = _words[new Random().Next(_words.Length)];
            _counts = _hiddenWordStr.Length + 2;
            foreach (var c in _hiddenWordStr)
            {
                _hiddenWord.Add(new HiddenChar(c));
            }
        }

        public void StartTheGame()
        {
            Console.WriteLine("Guess the word: " + GetStarsWord());
            while (_counts > 0 && !_hiddenWord.All(hiddenChar => hiddenChar.IsOpen))
            {
                char key = Char.ToLower(Console.ReadKey().KeyChar);
                bool isTrueChar = OpenChar(key);
                if (!isTrueChar) _counts--;
                Console.WriteLine();
                Console.WriteLine(GetStarsWord() + "       осталось попыток: " + _counts);
            }

            if (_counts <= 0)
            {
                Console.WriteLine("ХХААА YOU LOSE");
                Console.WriteLine("The word was: {0}", _hiddenWordStr);
            }

            if (_hiddenWord.All(hiddenChar => hiddenChar.IsOpen))
            {
                Console.WriteLine("CONGRATULATIONS, YOU WON");
            }
        }
    }
}