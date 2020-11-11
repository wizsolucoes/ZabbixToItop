using System.Text;
using System.IO;
using ZabbixToItop.Services;
using ZabbixToItop.Models;
using System.Threading.Tasks;
using System;

namespace ZabbixToItop.Services
{
    public class Log
    {
        private static string FileName { get; set; }
        private static string CurrentDirectory { get; set; }
        
        public Log(string fileName)
        {
            FileName = fileName;
            CurrentDirectory = Directory.GetCurrentDirectory();
        }

        public Log(string fileName, string directory)
        {
            Console.WriteLine(directory);
            FileName = fileName;
            CurrentDirectory = directory;
        }

        public static void WriteText(string text)
        {
            var completePath = CurrentDirectory + "/" + FileName;
            text += "\n";

            using (FileStream SourceStream = File.Open(completePath, FileMode.OpenOrCreate))
            {
                var uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(text);
                SourceStream.Seek(0, SeekOrigin.End);
                SourceStream.Write(result, 0, result.Length);
            }
        }

        public static string ReadText()
        {
            return File.ReadAllText(CurrentDirectory + "/" + FileName);
        }
    }
}