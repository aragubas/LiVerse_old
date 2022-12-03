using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace LiVerseFramework.Graphics
{
    public static class Fonts
    {
        static Dictionary<FontDescriptor, SpriteFont> Cache = new();

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

            FontDescriptor fontDescriptor = new(FontDescName, FontSize);

            Cache.Add(fontDescriptor, fontBakeResult.CreateSpriteFont(graphicsDevice));
        }

        public static SpriteFont GetFont(GraphicsDevice graphicsDevice, FontDescriptor fontDescriptor)
        {
            if (Cache.TryGetValue(fontDescriptor, out SpriteFont spriteFont))
            {
                return spriteFont;
            }

            LoadFont(graphicsDevice, fontDescriptor.Path, fontDescriptor.FontSize);

            return Cache[fontDescriptor];
        }

    }
}
