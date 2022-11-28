
using System;
using System.IO;

namespace LiVerseClient
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Content")))
            {
                throw new DirectoryNotFoundException("Could not find content directory.");
            }

            var game = new LiVerseClient.Game1();
            game.Run();

        }
    }
}