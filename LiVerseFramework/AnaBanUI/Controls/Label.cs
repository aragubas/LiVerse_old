using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        }

        void Resize()
        {
            ContentSize = Fonts.GetFont(_fontDescriptor).MeasureString(Text);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Fonts.GetFont(_fontDescriptor), Text, ContentPosition, Color.White);
        }
    }
}
