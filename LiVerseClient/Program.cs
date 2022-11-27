
namespace LiVerseClient
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            using var game = new LiVerseClient.Game1();
            game.Run();

        }
    }
}