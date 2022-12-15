using LiVerseFramework.AnaBanUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace LiVerseFramework.AnaBanUI
{
    public abstract class Element : IElementBase
    {
        #region Size Properties
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
                _contentSize = value;

                if (MaximumContentSize != Vector2.Zero &&
                    (_contentSize.X > MaximumContentSize.X || _contentSize.Y > MaximumContentSize.Y))
                {
                    _contentSize = MaximumContentSize;
                }

                if (_contentSize.X < MinimumContentSize.X || _contentSize.Y < MinimumContentSize.Y)
                {
                    _contentSize = MinimumContentSize;
                }

                ResizeBox();
            }
        }

        public Vector2 ContentSize
        {
            get => _contentSize;

            set
            {
                _contentSize = value;

                if (MaximumContentSize != Vector2.Zero &&
                    (_contentSize.X > MaximumContentSize.X || _contentSize.Y > MaximumContentSize.Y))
                {
                    _contentSize = MaximumContentSize;
                }

                if (_contentSize.X < MinimumContentSize.X || _contentSize.Y < MinimumContentSize.Y)
                {
                    _contentSize = MinimumContentSize;
                }

                ResizeBox();
            }
        }

        public Vector2 MaximumContentSize { get; set; } = Vector2.Zero;

        public Vector2 MinimumContentSize { get; set; } = Vector2.Zero;
        #endregion

        Vector2 _margin;
        public Vector2 Margin
        {
            get => _margin;
            set
            {
                _margin = value;
                Moved();
            }
        }

        Vector2 _position;
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                Moved();
            }
        }

        public Vector2 ContentPosition
        {
            get => Position + _padding;
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

        public Vector2 ParentContentPosition { get; set; } = Vector2.Zero;

        public Vector2 GlobalPosition
        {
            get => Position + ParentContentPosition;
        }

        public RectangleF GlobalRectangle
        {
            get => new RectangleF(GlobalPosition, BoxSize);
        }

        public bool Visible { get; set; } = true;

        void ResizeBox()
        {
            _boxSize = _contentSize + (_padding * 2);
            Resized();
        }

        public Element()
        {
            Resized();
            Moved();
        }


        #region Events
        public event Action OnMoved;
        public event Action OnResized;

        /// <summary>
        /// Called when Content or Box has been resized.<br></br>Don't forget to call the base method as it contains some stuff
        /// </summary>
        protected virtual void Resized() { OnResized?.Invoke(); }

        /// <summary>
        /// Called when Content or Box has been moved.<br></br>Don't forget to call the base method as it contains some stuff
        /// </summary>
        protected virtual void Moved() { OnMoved?.Invoke(); }

        #endregion

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void Update(GameTime gameTime) { }
    }
}
