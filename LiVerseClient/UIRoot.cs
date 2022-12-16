using LiVerseFramework;
using LiVerseFramework.AnaBanUI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using LiVerseFramework.AnaBanUI.Window;
using LiVerseFramework.AnaBanUI.Controls.Containers;

namespace LiVerseClient
{
    internal class UIRoot : IUiRoot
    {
        readonly IClientInstance _clientInstance;
        public ContainerBase RootContainer { get; set; }
        List<WindowBase> _windows = new();

        public UIRoot(IClientInstance clientInstance)
        {
            _clientInstance = clientInstance;
        }

        public void SetRootContainer(ContainerBase container)
        {
            RootContainer = container;

            RootContainer.BoxSize = new Vector2(_clientInstance.GameInstance.Window.ClientBounds.Width, _clientInstance.GameInstance.Window.ClientBounds.Height);
        }

        public void AddWindow(WindowBase window)
        {
            _windows.Add(window);
        }

        public void WindowResized()
        {
            RootContainer.BoxSize = new Vector2(_clientInstance.GameInstance.Window.ClientBounds.Width, _clientInstance.GameInstance.Window.ClientBounds.Height);
            RootContainer.MaximumContentSize = new Vector2(_clientInstance.GameInstance.Window.ClientBounds.Width, _clientInstance.GameInstance.Window.ClientBounds.Height);
        }

        public void Update(GameTime gameTime)
        {
            foreach (WindowBase window in _windows)
            {
                window.Update(gameTime);
            }

            // Update RootElement
            RootContainer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            RootContainer.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void DrawWindows(SpriteBatch spriteBatch)
        {
            foreach (WindowBase window in _windows)
            {
                window.Draw(spriteBatch);
            }
        }

    }
}
