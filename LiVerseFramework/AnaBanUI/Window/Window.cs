using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.AnaBanUI.Window
{

    public class Window : WindowBase
    {
        Matrix transformMatrix;
        Color borderColor;
        Color backgroundColor;
        Color titlebarColor;
        Vector2 titlebarTextPos = Vector2.Zero;
        FontDescriptor titlebarFont;

        bool _dragging = false;
        Point _dragPositionStart = Point.Zero;

        MouseState _oldMouseState;
        public Window()
        {
            borderColor = Color.FromNonPremultiplied(94, 114, 219, 255);
            backgroundColor = Color.FromNonPremultiplied(217, 217, 217, 225);
            titlebarColor = Color.FromNonPremultiplied(20, 20, 58, 255);

            transformMatrix = Matrix.CreateTranslation(new Vector3(Position, 0));
            titlebarFont = new FontDescriptor("Ubuntu.ttf", 12);
        }

        protected override void RectangleChanged()
        {
            transformMatrix = Matrix.CreateTranslation(new Vector3(Position, 0));

            Vector2 taskbarTextSize = Fonts.GetFont(titlebarFont).MeasureString(Title);
            titlebarTextPos = new Vector2(Size.X / 2 - taskbarTextSize.X / 2, TitlebarHeight / 2 - taskbarTextSize.Y / 2);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: transformMatrix);

            // Draw Window Background
            spriteBatch.FillRectangle(RelativeRectangle, backgroundColor);

            // Draw Titlebar 
            spriteBatch.FillRectangle(TitlebarRectangle, titlebarColor);
            // Draw Title
            spriteBatch.DrawString(Fonts.GetFont(titlebarFont), Title, titlebarTextPos, Color.White);


            // Draw Window Border
            spriteBatch.DrawRectangle(RelativeRectangle, borderColor);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            Point mousePos = Mouse.GetState().Position;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released && new Rectangle(TitlebarRectangle.X + (int)Position.X, TitlebarRectangle.Y + (int)Position.Y, TitlebarRectangle.Width, TitlebarRectangle.Height).Contains(mousePos))
            {
                _dragPositionStart = Position.ToPoint() - Mouse.GetState().Position;
                _dragging = true;

            }

            if (_dragging)
            {
                Position = (_dragPositionStart + Mouse.GetState().Position).ToVector2();

                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    _dragging = false;
                }

            }

            if (Position.X < 0) { Position = new Vector2(0, Position.Y); }
            if (Position.Y < 0) { Position = new Vector2(Position.X, 0); }

            _oldMouseState = Mouse.GetState();
        }

    }

}
