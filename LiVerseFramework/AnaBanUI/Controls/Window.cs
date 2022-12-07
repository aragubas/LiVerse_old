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

    public class Window : WindowBase
    {
        Matrix transformMatrix;
        Color borderColor;
        Color backgroundColor;
        Color titlebarColor;

        public Window()
        {
            borderColor = Color.FromNonPremultiplied(94, 114, 219, 255);
            backgroundColor = Color.FromNonPremultiplied(217, 217, 217, 225);
            titlebarColor = Color.FromNonPremultiplied(20, 20, 58, 255);

            transformMatrix = Matrix.CreateTranslation(new Vector3(Position, 0));
        }

        protected override void RectangleChanged()
        {
            transformMatrix = Matrix.CreateTranslation(new Vector3(Position, 0));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: transformMatrix);

            // Draw Window Background
            spriteBatch.FillRectangle(RelativeRectangle, backgroundColor);

            // Draw Titlebar 
            spriteBatch.FillRectangle(new Rectangle(Point.Zero, new Point((int)Size.X, 14)), titlebarColor);


            // Draw Window Border
            spriteBatch.DrawRectangle(RelativeRectangle, borderColor);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {

        }

    }

}
