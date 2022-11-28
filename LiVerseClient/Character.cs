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
        Vector2 _Position = Vector2.Zero;
        Vector2 _PositionTarget = Vector2.Zero;
        bool _Shaking = false;
        bool _Idle = false;
        bool _ShakingReset = false;
        Vector2 _idleTarget = Vector2.Zero;

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
                _Shaking = true;
                _Idle = false;

            }
            else
            {
                _currentFrame = _mouthClosed;
                _Shaking = false;
                _ShakingReset = true;
                _Idle = true;

            }

            _Position = Vector2.LerpPrecise(_Position, _PositionTarget, 0.5f);

            if (_Idle)
            {
                Random ceira = new Random();

                _idleTarget = new Vector2(ceira.Next(-10, 10), ceira.Next(-10, 10));
                _PositionTarget = Vector2.LerpPrecise(_PositionTarget, _idleTarget, 0.25f);
            }

            if (_Shaking)
            {
                Random ceira = new Random();
                _PositionTarget = new Vector2(ceira.Next(-ShakeIntensity, ShakeIntensity), ceira.Next(-ShakeIntensity, ShakeIntensity));

            }else
            {
                if (_ShakingReset)
                {
                    _ShakingReset = false;
                    _PositionTarget = Vector2.Zero;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 newSize = new Vector2(_currentFrame.Width / 2, _currentFrame.Height / 2);
                
            spriteBatch.Draw(_currentFrame, new Rectangle(spriteBatch.GraphicsDevice.Viewport.Width / 2 - (int)newSize.X / 2 + (int)_Position.X, spriteBatch.GraphicsDevice.Viewport.Height / 2 - (int)newSize.Y / 2 + (int)_Position.Y, (int)newSize.X, (int)newSize.Y), Color.White);
        }

    }
}
