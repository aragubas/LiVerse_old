using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.Graphics
{
    public static class Sprites
    {
        // Load Sprite From File
        public static Texture2D Texture2DFromFile(GraphicsDevice graphicsDevice, string FileLocation)
        {
            FileLocation = Path.Combine(Environment.CurrentDirectory, "Content", "Images", FileLocation);
            FileStream fileStream = new FileStream(FileLocation, FileMode.Open);
            Texture2D ValToReturn = Texture2D.FromStream(graphicsDevice, fileStream);
            fileStream.Dispose();

            return ValToReturn;
        }
    }
}
