using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GallowGame
{
    public class FileSearcher
    {
        private static List<string> _files = new List<string>();
        private string _path;
        private string _fileName;
        private static bool IsSlnFileInDir(string directory)
        {
            var files = Directory.GetFiles(directory);
            return files.Any(file => Path.GetExtension(file) == ".sln");
        }

        private static string SlnDirPath(string curDir)
        {
            try
            {
                while (!IsSlnFileInDir(curDir))
                {
                    curDir = Path.GetDirectoryName(curDir);
                    if (curDir == null)
                        return null;
                }

                return curDir;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string FindSlnPathInProject(string directoryInProject) //just in sln folder
        {
            string slnPath = SlnDirPath(curDir: directoryInProject);

            if (slnPath == null)
                return null;
            
            return slnPath;
        }

        public static string FindFileInProject(string fileName)
        {
            var slnPath = FindSlnPathInProject(directoryInProject: Directory.GetCurrentDirectory());
            return FindFileInSubDirs("WordsStockRus.txt", slnPath);
        }
        public static string FindFileInSubDirs(string fileName, string directory)
        {
            var files = Directory.GetFiles(directory);
            var dirs = Directory.GetDirectories(directory);
            string result = null;
            
            foreach (var file in files)
            {
                if (Path.GetFileName(file) == fileName)
                {
                    return file;
                }
            }

            foreach (var dir in dirs)
            {
                // Console.WriteLine(dir);
                 result = FindFileInSubDirs(fileName, dir);
            }

            return result;
        }
        public static List<string> FindFilesInDirectory(string fileName, string directory)
        {
            var files = Directory.GetFiles(directory);
            var dirs = Directory.GetDirectories(directory);
            
            foreach (var file in files)
            {
                if (Path.GetFileName(file) == fileName)
                {
                    _files.Add(file);
                }
            }

            foreach (var dir in dirs)
            {
                // Console.WriteLine(dir);
                var result = FindFileInSubDirs(fileName, dir);
                if (result != null)
                {
                    _files.Add(result);
                }
            }

            return _files;
        }

        public FileSearcher(string fileName = "WordsStockRus.txt")
        {
            _fileName = fileName;
        }
    }
}