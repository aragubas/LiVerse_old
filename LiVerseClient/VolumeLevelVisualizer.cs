using LiVerseFramework;
using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using System;
using System.Timers;

namespace LiVerseClient
{
    public class VolumeLevelVisualizer
    {
        Timer _peakResetTimer;
        RectangleF _rectangle;

        float _peak = 0;
        float _peakTarget = 0;
        bool _triggerGrabbed;

        public float MaxValue = 100;
        public float CurrentValue = 0;
        public float TriggerLevel = 0.25f;
        public bool TriggerActive = false;


        Color _boxColor;
        Color _meterColor;
        Color _meterDetailColor;
        Color _peakColor;
        Color _triggerColor;
        Color _triggerGrabbedColor;
        Color _triggerActiveColor;

        float _triggerLevelLastValue = 0.0f;
        bool _releaseSoundEffectToggle = false;

        public VolumeLevelVisualizer(RectangleF rectangle)
        {
            _rectangle = rectangle;
            _peakResetTimer = new Timer();

            _peakResetTimer.Interval = 4000;
            _peakResetTimer.Elapsed += _peakResetTimer_Elapsed;
            _peakResetTimer.Start();

            _boxColor = Color.FromNonPremultiplied(66, 100, 234, 255);
            _meterColor = Color.FromNonPremultiplied(102, 130, 238, 255);
            _meterDetailColor = Color.FromNonPremultiplied(2, 30, 138, 50);
            _peakColor = Color.FromNonPremultiplied(127, 219, 255, 50);

            _triggerColor = Color.FromNonPremultiplied(5, 96, 150, 150);
            _triggerActiveColor = Color.FromNonPremultiplied(96, 15, 160, 150);
            _triggerGrabbedColor = Color.FromNonPremultiplied(196, 115, 260, 255);
        }

        private void _peakResetTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _peakTarget = 0;
        }

        public void Update(GameTime gameTime, bool focused)
        {
            if (focused)
            {
                MouseState mouseState = Mouse.GetState();
                
                if (mouseState.LeftButton == ButtonState.Pressed && _rectangle.Intersects(new Rectangle(mouseState.Position.X, mouseState.Position.Y, 1, 1)))
                {
                    float mouseRelativePos = _rectangle.Bottom - mouseState.Y;
                    TriggerLevel = (mouseRelativePos / _rectangle.Height);

                    TriggerLevel = Math.Clamp(TriggerLevel, 0, 1);

                    if (_triggerLevelLastValue != TriggerLevel)
                    {
                        _triggerLevelLastValue = TriggerLevel;
                        SoundEffectManager.PlaySoundEffect("core.progress", 0.05f);
                    }

                    _triggerGrabbed = true;
                    _releaseSoundEffectToggle = false;

                }
                else 
                {
                    _triggerGrabbed = false;

                    if (!_releaseSoundEffectToggle)
                    {
                        _releaseSoundEffectToggle = true;
                        SoundEffectManager.PlaySoundEffect("core.progress", 0.15f);
                    }
                }
            }
            else
            {
                _triggerGrabbed = false;
            }

            TriggerActive = CurrentValue / MaxValue >= TriggerLevel;

            // math is weird
            _peak = MathHelper.LerpPrecise(_peak, _peakTarget, 1 - MathF.Pow(0.00000000000000001f, (float)gameTime.ElapsedGameTime.TotalSeconds));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float ratio = CurrentValue / MaxValue;

            if (_peakTarget < ratio)
            {
                _peakTarget = ratio;
            }
 
            // Draw level
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * ratio), 20, (_rectangle.Height * ratio)), _meterColor);
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * ratio) + 1, 20, 1), _meterDetailColor);

            // Draw peak
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * _peak) - 2, 20, 4), _peakColor);

            // Draw trigger
            Color triggerColor = ratio >= TriggerLevel ? _triggerActiveColor : _triggerColor;
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * TriggerLevel), 20, 4), _triggerGrabbed ? _triggerGrabbedColor : triggerColor);

            if (_triggerGrabbed)
            {
                float Y = _rectangle.Y + _rectangle.Height - (_rectangle.Height * TriggerLevel) + 2;

                spriteBatch.DrawLine(_rectangle.X + 2, Y, _rectangle.Right - 2, Y, Color.DarkRed);
            }

            // Draw box
            spriteBatch.DrawRectangle(new RectangleF(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height), _boxColor);
        }


    }
}
