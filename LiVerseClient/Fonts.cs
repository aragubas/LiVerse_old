using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseClient
{
    public static class Fonts
    {
        static List<SpriteFont> CachedFonts = new List<SpriteFont>();
        static List<string> CachedFonts_Key = new List<string>();

        public static void LoadFont(string FontPath, int FontSize)
        {
            FontPath = Path.Combine(Environment.CurrentDirectory, "Content", FontPath);
            string FontDescName = Path.GetFileName(FontPath).Replace("/", "").Replace("\\", "");

            var fontBakeResult = TtfFontBaker.Bake(File.ReadAllBytes(FontPath),
                         FontSize,
                         1024,
                         1024,
                         new[]
                         {
                                                        CharacterRange.BasicLatin,
                                                        CharacterRange.Latin1Supplement,
                                                        CharacterRange.LatinExtendedA,
                                                        CharacterRange.Cyrillic
                         }
                     );

            CachedFonts.Add(fontBakeResult.CreateSpriteFont(Game1.Instance.GraphicsDevice));
            CachedFonts_Key.Add($"{FontDescName}:{FontSize}");
        }

        public static SpriteFont GetFont(string FontPath, int FontSize)
        {
            int FontIndex = CachedFonts_Key.IndexOf($"{FontPath}:{FontSize}");

            if (FontSize < 1) { FontSize = 1; }

            // Font was not found in cache
            if (FontIndex == -1)
            {
                Console.WriteLine("A font is being added to Font Cache");
                Console.WriteLine("Please wait, the application has not frozen");

                LoadFont(FontPath, FontSize);

                Console.WriteLine("Sucefully added font to font cache");

                FontIndex = CachedFonts_Key.IndexOf($"{FontPath}:{FontSize}");

                if (FontIndex == -1)
                {
                    throw new NotImplementedException("A internal bug has occoured on the application.\nFont was added to cache, but its index could not be found.");
                }
            }

            return CachedFonts[FontIndex];
        }

    }
}
