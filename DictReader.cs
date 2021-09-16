using System;
using System.IO;
using System.Text;

namespace GallowGame
{
    internal class DictReader
    {
        public readonly string[] Words;
        private readonly string _dictName ;
        private string _dictPath;


        public DictReader(string dictName)
        {
            _dictName = dictName;
            _dictPath = FileSearcher.FindFileInProject(_dictName);
            Words = ReadAllLinesFromFile(_dictPath);
        }


        private string[] ReadAllLinesFromFile(string path)
        {
            try
            {
                return File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        void StreamReadFile(string path)
        {
            using (FileStream readingStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] temp = new byte[readingStream.Length];
                int bytesToRead = (int)readingStream.Length; // mb error
                int bytesRead = 0;
                try
                {
                    while (bytesToRead > 0)
                    {
                        int n = readingStream.Read(temp, bytesRead, bytesToRead); // ==0 => END
                        if (n == 0)
                            break;
                        bytesRead += n;
                        bytesToRead -= n;
                    }

                    string str = Encoding.ASCII.GetString(temp, 0, temp.Length);
                    Console.WriteLine(str);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}