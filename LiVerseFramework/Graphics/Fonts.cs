using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.Graphics
{
    public static class Fonts
    {
        static List<SpriteFont> CachedFonts = new List<SpriteFont>();
        static List<string> CachedFonts_Key = new List<string>();

        /// <summary>
        /// Loads a font into the font cache
        /// </summary>
        /// <param name="FontPath">Font file name (inside font content folder)</param>
        /// <param name="FontSize">Font size in pixels</param>
        public static void LoadFont(GraphicsDevice graphicsDevice, string FontPath, int FontSize)
        {
            FontPath = Path.Combine(Environment.CurrentDirectory, "Content", "Fonts", FontPath);
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

            CachedFonts.Add(fontBakeResult.CreateSpriteFont(graphicsDevice));
            CachedFonts_Key.Add($"{FontDescName}:{FontSize}");
        }

        public static SpriteFont GetFont(GraphicsDevice graphicsDevice, string FontPath, int FontSize)
        {
            int FontIndex = CachedFonts_Key.IndexOf($"{FontPath}:{FontSize}");

            if (FontSize < 1) { FontSize = 1; }

            // Font was not found in cache
            if (FontIndex == -1)
            {
                LoadFont(graphicsDevice, FontPath, FontSize);

                FontIndex = CachedFonts_Key.IndexOf($"{FontPath}:{FontSize}");

                if (FontIndex == -1)
                {
                    throw new KeyNotFoundException($"Could not find font '{FontPath}:{FontSize}'");
                }
            }

            return CachedFonts[FontIndex];
        }

    }
}
