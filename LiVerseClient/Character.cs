using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LiVerseClient
{
    public class Character
    {
        Texture2D _mouthClosed;
        Texture2D _mouthOpened;
        Vector2 _characterPos = Vector2.Zero;
        Vector2 _characterPosTarget = Vector2.Zero;
        bool _characterIsShaking = false;
        bool _characterIsShakingReset = false;

        Texture2D _currentFrame;
        public bool Speaking;
        public int ShakeIntensity = 10;


        public Character() 
        {
            _mouthClosed = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_closed.png");
            _mouthOpened = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_open.png");
        }

        public void Update()
        {
            if (Speaking)
            {
                _currentFrame = _mouthOpened;
                _characterIsShaking = true;

            }else
            {
                _currentFrame = _mouthClosed;
                _characterIsShaking = false;
                _characterIsShakingReset = true;

            }

            _characterPos = Vector2.LerpPrecise(_characterPos, _characterPosTarget, 0.5f);

            if (_characterIsShaking)
            {
                Random ceira = new Random();
                _characterPosTarget = new Vector2(ceira.Next(-ShakeIntensity, ShakeIntensity), ceira.Next(-ShakeIntensity, ShakeIntensity));

            }else
            {
                if (_characterIsShakingReset)
                {
                    _characterIsShakingReset = false;
                    _characterPosTarget = Vector2.Zero;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 newSize = new Vector2(_currentFrame.Width / 2, _currentFrame.Height / 2);
                
            spriteBatch.Draw(_currentFrame, new Rectangle(spriteBatch.GraphicsDevice.Viewport.Width / 2 - (int)newSize.X / 2 + (int)_characterPos.X, spriteBatch.GraphicsDevice.Viewport.Height / 2 - (int)newSize.Y / 2 + (int)_characterPos.Y, (int)newSize.X, (int)newSize.Y), Color.White);
        }

    }
}
