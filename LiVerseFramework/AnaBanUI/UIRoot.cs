using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LiVerseFramework.AnaBanUI
{
    public class UIRoot
    {
        readonly IClientInstance _clientInstance;
        List<Element> _elements = new();

        public UIRoot(IClientInstance clientInstance)
        {
            _clientInstance = clientInstance;
        }

        public void AddElement(Element element)
        {
            _elements.Add(element);

            // First element must occupy all space
            if (_elements.Count == 1)
            {
                element.BoxSize = new Vector2(_clientInstance.GameInstance.Window.ClientBounds.Width, _clientInstance.GameInstance.Window.ClientBounds.Height);
            }
        }

        public void WindowResized()
        {
            if (_elements.Count >= 1)
            {
                _elements[0].BoxSize = new Vector2(_clientInstance.GameInstance.Window.ClientBounds.Width, _clientInstance.GameInstance.Window.ClientBounds.Height);
            }
        }

        public void Update(GameTime gameTime) 
        {
            foreach (Element element in _elements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (Element element in _elements)
            {
                element.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

    }
}
