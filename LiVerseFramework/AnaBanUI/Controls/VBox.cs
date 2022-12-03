using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace LiVerseFramework.AnaBanUI.Controls
{
    public class VBox : ContainerBase
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(ContentRectangle, Color.Blue, 1);
            spriteBatch.DrawRectangle(BoxRectangle, Color.Magenta, 1);

            foreach (Element element in ChildElements)
            {
                element.Draw(spriteBatch);
            }
        }

        public override void Resized() => CalculateUI();

        void CalculateUI()
        {
            // No element to re-calculate
            if (ChildElements.Count == 0) { return; }

            // If we only have one element, it must occupy all space
            if (ChildElements.Count == 1)
            {
                Element element = ChildElements[0];

                element.BoxSize = BoxSize;

                return;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Element element in ChildElements)
            {
                element.Update(gameTime);
            }

        }
    }
}
