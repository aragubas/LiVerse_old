using LiVerseFramework.Graphics;
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
    public class Label : Element
    {
        FontDescriptor _fontDescriptor;
        string _text;

        public string Text 
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    Resize();
                }
            }
        }

        public Label(string Text, FontDescriptor font)
        {
            _fontDescriptor = font;
            this.Text = Text;
            Resize();
        }

        void Resize()
        {
            Vector2 textSize = Fonts.GetFont(_fontDescriptor).MeasureString(Text);
            MinimumContentSize = textSize;
            ContentSize = textSize;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(ContentRectangle, Color.Blue, 1);
            spriteBatch.DrawRectangle(BoxRectangle, Color.Magenta, 1);

            spriteBatch.DrawString(Fonts.GetFont(_fontDescriptor), Text, ContentPosition, Color.White);
        }
    }
}
