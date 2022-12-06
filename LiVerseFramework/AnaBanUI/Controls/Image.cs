using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.AnaBanUI.Controls
{
    public class Image : Element
    {
        Texture2D _image;
        public Texture2D ImageTexture
        {
            get => _image;

            set
            {
                _image = value;
                Resize();
            }
        }

        public Color BlendColor = Color.White;

        public Image()
        {

        }

        void Resize()
        {
            ContentSize = new Vector2(ImageTexture.Width, ImageTexture.Height);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image, ContentPosition, (Rectangle)new RectangleF(ContentPosition.X, ContentPosition.Y, ContentSize.X, ContentSize.Y), BlendColor);
        }
    }
}
