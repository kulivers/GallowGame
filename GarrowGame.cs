using System;
using System.Collections.Generic;

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
        private string _hiddenWord;
        private List<HiddenChar> _chars = new List<HiddenChar>();

        public Garrow(string dictName = "WordsStockRus.txt")
        {
            _words = new DictReader(dictName).Words;
            _hiddenWord = _words[new Random().Next(_words.Length)];
            foreach (var c in _hiddenWord)
            {
                _chars.Add(new HiddenChar(c));
            }
            
        }
    }
}