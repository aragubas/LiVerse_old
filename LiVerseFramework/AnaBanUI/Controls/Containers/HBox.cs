using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.AnaBanUI.Controls.Containers
{
    public class HBox : ContainerBase
    {
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

            float nextX = 0;

            for (int i = 0; i < ChildElements.Count; i++)
            {
                Element element = ChildElements[i];
                float nextMargin = 0;


                // If not the last item
                if (i + 1 < ChildElements.Count)
                {
                    if (element.Margin.X >= ChildElements[i + 1].Margin.X)
                    {
                        nextMargin = element.Margin.X;
                    }

                    if (element.Margin.X <= ChildElements[i + 1].Margin.X)
                    {
                        nextMargin = ChildElements[i + 1].Margin.X;
                    }
                }

                // If the first element, respect X margin
                if (i == 0)
                {
                    nextX = element.Margin.X;
                }

                float elementY = element.Margin.Y;


                element.Position = new Vector2(nextX, elementY);
                nextX += element.BoxRectangle.Width + nextMargin;
            }


        }

        protected override void Resized() => CalculateUI();

        protected override void Moved()
        {
            foreach (Element element in ChildElements)
            {
                element.ParentContentPosition = ContentPosition;
            }
            CalculateUI();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Element element in ChildElements.Reverse<Element>())
            {
                element.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();


            foreach (Element element in ChildElements)
            {
                spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(ContentPosition, 0f)));
                element.Draw(spriteBatch);
                spriteBatch.End();
            } 


            spriteBatch.Begin();
        }

    }
}
