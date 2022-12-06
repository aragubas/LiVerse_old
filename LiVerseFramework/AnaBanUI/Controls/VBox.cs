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
            foreach (Element element in ChildElements)
            {
                element.Draw(spriteBatch);
            }
        }

        public override void Resized() => CalculateUI();

        public override void AddElement(Element element)
        {
            base.AddElement(element);
            CalculateUI();
        }

        void CalculateUI()
        {
            // No element to re-calculate
            if (ChildElements.Count == 0) { return; }

            // If we only have one element, it must occupy all space
            if (ChildElements.Count == 1)
            {
                Element element = ChildElements[0];

                element.Position = Vector2.Zero;
                element.BoxSize = BoxSize;

                return;
            }
            
            // TODO: Needs iteration count
            float nextY = 0;

            for(int i = 0; i < ChildElements.Count; i++)
            {
                Element element = ChildElements[i];
                float nextMargin = 0;


                // If not the last item
                if (i + 1 < ChildElements.Count)
                {
                    nextMargin = Math.Abs(ChildElements[i + 1].Margin.Y - element.Margin.Y);

                    if (element.Margin.Y >= ChildElements[i + 1].Margin.Y)
                    {
                        nextMargin = element.Margin.Y;
                    }
                }

                // First Element
                if (i == 0)
                {
                    nextY = element.Margin.Y;
                }

                element.Position = new Vector2(element.Margin.X, nextY);
                nextY += element.BoxRectangle.Height + nextMargin;
            }

            //foreach (Element element in ChildElements)
            //{
            //    if (nextY == 0) 
            //    {
            //        nextY += element.BoxRectangle.Height + element.Margin.Y;

            //        continue;
            //    }
            //    element.Position = new Vector2(0, nextY);

            //    nextY += element.BoxRectangle.Height + element.Margin.Y;
            //}


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
