using LiVerseFramework.Character;
using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace LiVerseClient
{
    class IdleAnimation : ICharacterAnimation
    {
        public string Name => "Idle";
        public Vector2 PositionOffset { get; set; }

        Vector2 _idleTarget = Vector2.Zero;
        Vector2 _idle = Vector2.Zero;
        float _intensity = 5;
        double _time = 0;
        
        public IdleAnimation()
        {
            
        }

        void ICharacterAnimation.Update(GameTime gameTime)
        {
            _time += gameTime.ElapsedGameTime.TotalSeconds;

            PositionOffset = Vector2.SmoothStep(PositionOffset, _idle, 0.1f);
            _idle = new Vector2(MathF.Sin(_idleTarget.X) * _intensity, MathF.Cos(_idleTarget.Y) * _intensity);

            if (_time >= 0.25)
            {
                _idleTarget = new Vector2(Random.Shared.Next((int)-_intensity, (int)_intensity), Random.Shared.Next((int)-_intensity, (int)_intensity));

                _time = 0;
            }
        }

    }

    public class DefaultCharacter : ICharacter
    {
        public bool Speaking { get; set; }
        public int ShakeIntensity = 10;
        public bool DrawBoundaries { get; set; } = true;

        Texture2D _mouthClosed;
        Texture2D _mouthOpened;
        Vector2 _Position = Vector2.Zero;

        Texture2D _currentFrame;

        ICharacterAnimation idleAnimation;
        ICharacterAnimation stateChangeAnimation;

        public DefaultCharacter() 
        {
            _mouthClosed = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_closed.png");
            _mouthOpened = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_open.png");

            idleAnimation = new IdleAnimation();
        }

        public void Update(GameTime gameTime)
        {
            idleAnimation?.Update(gameTime);
            _Position = Vector2.SmoothStep(_Position, idleAnimation.PositionOffset, 28f * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Speaking)
            {
                _currentFrame = _mouthOpened;
                //_Shaking = true;
                //_Idle = false;

            }
            else
            {
                _currentFrame = _mouthClosed;
                //_Shaking = false;
                //_ShakingReset = true;
                //_Idle = true;

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
