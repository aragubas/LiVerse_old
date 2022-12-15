using LiVerseFramework.AnaBanUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;

namespace LiVerseFramework.AnaBanUI.Window
{
    public abstract class WindowBase : IElementBase
    {
        Vector2 _position;
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateRectangle();
            }

        }

        Vector2 _size;
        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                UpdateRectangle();
            }
        }

        Rectangle _rectangle;
        public Rectangle Rectangle
        {
            get => _rectangle;
            set
            {
                _position.X = value.X;
                _position.Y = value.Y;
                _size.X = value.Width;
                _size.Y = value.Height;

                UpdateRectangle();
            }

        }

        public Rectangle RelativeRectangle
        {
            get => new Rectangle(Point.Zero, Size.ToPoint());
        }

        protected Rectangle TitlebarRectangle
        {
            get => new Rectangle(Point.Zero, new Point((int)Size.X, TitlebarHeight));
        }

        string _title = "AnaBan Window";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                TitleChanged();
            }
        }

        protected int TitlebarHeight = 14;

        void UpdateRectangle()
        {
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y);
            RectangleChanged();
        }

        protected virtual void RectangleChanged() { }
        protected virtual void TitleChanged() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void Update(GameTime gameTime) { }

    }

}
