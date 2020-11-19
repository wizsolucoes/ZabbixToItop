using System;
using System.Text;
using System.IO;

namespace ZabbixToItop.Util
{
    public class Log
    {
        private static string FileName { get; set; }
        private static string CurrentDirectory { get; set; }
        
        public Log(string fileName)
        {
            FileName = fileName;
            CurrentDirectory = Directory.GetCurrentDirectory();
            WriteText("-------------------- " + DateTime.Now + " --------------------");
        }

        public Log(string fileName, string directory)
        {
            FileName = fileName;
            CurrentDirectory = directory;
            WriteText("-------------------- " + DateTime.Now + " --------------------");
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