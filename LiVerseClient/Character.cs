using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Timers;

namespace LiVerseClient
{
    interface CharacterAnimation
    {
        /// <summary>
        /// Descriptive name for your animation, usually separated by _ or written in CamelCase
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The final position offset for the current frame, set by Animation's Update method every frame
        /// </summary> 
        public Vector2 PositionOffset { get; set; }

        /// <summary>
        /// Update method called every frame
        /// </summary>
        /// <param name="gameTime">Monogame's GameTime time step class</param>
        void Update(GameTime gameTime) { }
    }

    class IdleAnimation : CharacterAnimation
    {
        public string Name => "Idle";
        public Vector2 PositionOffset { get; set; }

        Vector2 _idleTarget = Vector2.Zero;
        Vector2 _idle = Vector2.Zero;
        float _intensity = 10;
        double _time = 0;
        int ceira = 0;

        public IdleAnimation()
        {
            
        }

        void CharacterAnimation.Update(GameTime gameTime)
        {
            _time += gameTime.ElapsedGameTime.TotalSeconds;

            PositionOffset = Vector2.SmoothStep(PositionOffset, _idleTarget, 0.1f);
            _idle = Vector2.SmoothStep(_idle, _idleTarget, 0.8f);

            if (_time >= 0.25)
            {
                ceira += 1;

                if (ceira == 1)
                {
                    _idleTarget = new Vector2(_intensity, -_intensity); // Top-Left Corner
                }
                else if (ceira == 2)
                {
                    _idleTarget = Vector2.Zero;
                }
                else if (ceira == 3)
                {
                    _idleTarget = new Vector2(_intensity, _intensity); // Bottom-Right Corner

                }
                else if (ceira == 4)
                {
                    _idleTarget = Vector2.Zero;

                } else if (ceira == 5)
                {
                    _idleTarget = new Vector2(_intensity, -_intensity); // Top-Right Corner

                }
                else if (ceira == 6)
                {
                    _idleTarget = Vector2.Zero;
                } else if (ceira == 7)
                {
                    _idleTarget = new Vector2(-_intensity, _intensity); // Bottom-Left Corner

                    ceira = 0;
                }


                _time = 0;
            }

            //Debug.WriteLine(gameTime.ElapsedGameTime.TotalSeconds);
        }

    }

    public class Character
    {
        public bool Speaking;
        public int ShakeIntensity = 10;
        public bool DrawBoundaries { get; set; } = true;

        Texture2D _mouthClosed;
        Texture2D _mouthOpened;
        Vector2 _Position = Vector2.Zero;
        Vector2 _PositionTarget = Vector2.Zero;
        bool _Shaking = false;
        bool _Idle = false;
        bool _ShakingReset = false;
        Vector2 _idleTarget = Vector2.Zero;

        Texture2D _currentFrame;

        CharacterAnimation characterAnimation;

        public Character() 
        {
            _mouthClosed = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_closed.png");
            _mouthOpened = Sprites.Texture2DFromFile(Game1.Instance.GraphicsDevice, "mouth_open.png");

            characterAnimation = new IdleAnimation();
        }

        public void Update(GameTime gameTime)
        {
            characterAnimation?.Update(gameTime);
            _Position = Vector2.SmoothStep(_Position, characterAnimation.PositionOffset, 28f * (float)gameTime.ElapsedGameTime.TotalSeconds);

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


            //if (_Idle)
            //{
            //    Random ceira = new Random();

            //    _idleTarget = new Vector2(ceira.Next(-10, 10), ceira.Next(-10, 10));
            //    _PositionTarget = Vector2.LerpPrecise(_PositionTarget, _idleTarget, 0.25f);
            //}

            //if (_Shaking)
            //{
            //    Random ceira = new Random();
            //    _PositionTarget = new Vector2(ceira.Next(-ShakeIntensity, ShakeIntensity), ceira.Next(-ShakeIntensity, ShakeIntensity));

            //}else
            //{
            //    if (_ShakingReset)
            //    {
            //        _ShakingReset = false;
            //        _PositionTarget = Vector2.Zero;
            //    }
            //}

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
