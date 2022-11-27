using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
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

        public float MaxLevel = 100;
        public float CurrentLevel = 100;
        public float TriggerLevel = 0.5f;
        public bool TriggerActive = false;


        Color _boxColor;
        Color _meterColor;
        Color _peakColor;
        Color _triggerColor;
        Color _triggerGrabbedColor;
        Color _triggerActiveColor;

        public VolumeLevelVisualizer(RectangleF rectangle)
        {
            _rectangle = rectangle;
            _peakResetTimer = new Timer();

            _peakResetTimer.Interval = 4000;
            _peakResetTimer.Elapsed += _peakResetTimer_Elapsed;
            _peakResetTimer.Start();

            _boxColor = Color.FromNonPremultiplied(66, 100, 234, 255);
            _meterColor = Color.FromNonPremultiplied(102, 130, 238, 255);
            _peakColor = Color.FromNonPremultiplied(7, 19, 67, 50);

            _triggerColor = Color.FromNonPremultiplied(5, 96, 150, 150);
            _triggerActiveColor = Color.FromNonPremultiplied(96, 15, 160, 150);
            _triggerGrabbedColor = Color.FromNonPremultiplied(196, 115, 260, 255);
        }

        private void _peakResetTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _peakTarget = 0;
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                float mouseRelativePos = _rectangle.Bottom - mouseState.Y;
                TriggerLevel = (mouseRelativePos / _rectangle.Height);

                TriggerLevel = Math.Clamp(TriggerLevel, 0, 1);
                _triggerGrabbed = true;

            }else { _triggerGrabbed = false; }

            TriggerActive = CurrentLevel / MaxLevel >= TriggerLevel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float ratio = CurrentLevel / MaxLevel;

            if (_peakTarget < ratio)
            {
                _peakTarget = ratio;
            }

            _peak = MathHelper.LerpPrecise(_peak, _peakTarget, 0.6f);


            // Draw level
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * ratio), 20, (_rectangle.Height * ratio)), _meterColor);

            // Draw peak
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * _peak) - 2, 20, 4), _peakColor);

            // Draw trigger
            Color triggerColor = ratio >= TriggerLevel ? _triggerActiveColor : _triggerColor;
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * TriggerLevel), 20, 4), _triggerGrabbed ? _triggerGrabbedColor : triggerColor);
            
            if (_triggerGrabbed)
            {
                float Y = _rectangle.Y + _rectangle.Height - (_rectangle.Height * TriggerLevel) + 2;

                spriteBatch.DrawLine(_rectangle.X, Y, _rectangle.Right, Y, Color.DarkRed);
            }

            // Draw box
            spriteBatch.DrawRectangle(new RectangleF(32, 32, 20, 400), _boxColor);

            spriteBatch.DrawString(Fonts.GetFont("Ubuntu.ttf", 14), $"Trigger: {TriggerLevel} Value: {ratio}", new Vector2(16, 16), Color.White);
        }


    }
}
