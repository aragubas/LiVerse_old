using LiVerseFramework.Character;
using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiVerseClient
{
    public class DefaultCharacter : ICharacter
    {
        public bool Speaking { get; set; }
        public int ShakeIntensity = 10;
        public bool DrawBoundaries { get; set; } = true;

        Texture2D _mouthClosed;
        Texture2D _mouthOpened;
        Vector2 _Position = Vector2.Zero;

        Texture2D _currentFrame;

        ICharacterAnimation currentAnimation;

        public DefaultCharacter() 
        {
            _mouthClosed = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_closed.png");
            _mouthOpened = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_open.png");

            //currentAnimation = new IdleAnimation();
            //currentAnimation = Game1.Instance.Animations.(animation => animation.Name == "default_idle");

            // When the Client loads no animations are available, so when plugins start loading, look for the 'default_idle' animation
            Game1.Instance.Animations.CollectionChanged += Animations_CollectionChanged;
        }

        void Animations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (currentAnimation == null)
            {
                currentAnimation = Game1.Instance.Animations.Single(animation => animation.Name == "default_idle");
            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentAnimation != null)
            {
                currentAnimation.Update(gameTime);
                _Position = Vector2.SmoothStep(_Position, currentAnimation.PositionOffset, 28f * (float)gameTime.ElapsedGameTime.TotalSeconds);

                currentAnimation.IsCharacterSpeaking = Speaking;
            }

            // Change character frame
            if (Speaking)
            {
                _currentFrame = _mouthOpened;

            }
            else
            {
                _currentFrame = _mouthClosed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 newSize = new Vector2(_currentFrame.Width / 2, _currentFrame.Height / 2);

            spriteBatch.Draw(_currentFrame, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 + _Position.X, spriteBatch.GraphicsDevice.Viewport.Height / 2 + _Position.Y), null, Color.White, 0f, newSize, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
            
            if (DrawBoundaries)
                spriteBatch.DrawRectangle(new RectangleF(spriteBatch.GraphicsDevice.Viewport.Width / 2 - newSize.X / 2 + _Position.X, spriteBatch.GraphicsDevice.Viewport.Height / 2 - newSize.Y / 2 + _Position.Y, newSize.X, newSize.Y), Color.Magenta);
        }

    }
}
