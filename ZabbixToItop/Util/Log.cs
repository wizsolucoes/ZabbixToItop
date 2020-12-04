using System;
using System.Text;
using System.IO;

namespace ZabbixToItop.Util
{
    public static class Log
    {
        public static string LogPath { get; set; }

        public static void WriteText(string text)
        {
            text += "\n";

            using (FileStream SourceStream = File.Open(LogPath, FileMode.OpenOrCreate))
            {
                var uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(text);
                SourceStream.Seek(0, SeekOrigin.End);
                SourceStream.Write(result, 0, result.Length);
            }
        }

        public static string ReadText()
        {
            return File.ReadAllText(LogPath);
        }
    }
}