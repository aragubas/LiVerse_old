using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.AnaBanUI
{
    public abstract class Element
    {
        Vector2 _contentSize;
        Vector2 _boxSize;
        Vector2 _padding;
        public Vector2 Padding
        {
            get => _padding;

            set
            {
                _padding = value;
                ResizeBox();
            }
        }

        public Vector2 BoxSize
        {
            get => _boxSize;

            set
            {
                _contentSize = value + _padding;

                if (MaximumContentSize != Vector2.Zero &&
                    (_contentSize.X > MaximumContentSize.X || _contentSize.Y > MaximumContentSize.Y))
                {
                    _contentSize = MaximumContentSize + Padding;
                }

                if (_contentSize.X < MinimumContentSize.X || _contentSize.Y < MinimumContentSize.Y)
                {
                    _contentSize = MinimumContentSize + Padding;
                }

                ResizeBox();
            }
        }

        public Vector2 ContentSize
        {
            get => _contentSize;

            set
            {
                _contentSize = value + _padding;

                if (MaximumContentSize != Vector2.Zero &&
                    (_contentSize.X > MaximumContentSize.X || _contentSize.Y > MaximumContentSize.Y))
                {
                    _contentSize = MaximumContentSize + Padding;
                }

                if (_contentSize.X < MinimumContentSize.X || _contentSize.Y < MinimumContentSize.Y)
                {
                    _contentSize = MinimumContentSize + Padding;
                }

                ResizeBox();
            }
        }
        public Vector2 MaximumContentSize { get; set; } = Vector2.Zero;
        public Vector2 MinimumContentSize { get; set; } = Vector2.Zero;

        public Vector2 Margin { get; set; } = Vector2.Zero;

        public Vector2 Position { get; set; } = Vector2.Zero;

        public Vector2 ContentPosition
        {
            get => Position + (Padding / 2);
        }

        public RectangleF ContentRectangle
        {
            get => new RectangleF(ContentPosition, ContentSize);
        }

        public RectangleF BoxRectangle
        {
            get => new RectangleF(Position, BoxSize);
        }

        public string ID { get; set; } = string.Empty;

        void ResizeBox()
        {
            _boxSize = _contentSize + _padding;
            Resized();
        }

        public virtual void Resized() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void Update(GameTime gameTime) { }
    }
}
