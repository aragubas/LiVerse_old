using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Timers;

namespace LiVerseClient
{
    public class DelayLevelVisualizer
    {
        Timer _peakResetTimer;
        RectangleF _rectangle;

        bool _triggerGrabbed;

        public float MaxValue = 100;
        public float CurrentValue = 1;
        public float TriggerLevel = 0.8f;
        public bool TriggerActive = false;

        Color _boxColor;
        Color _meterColor;
        Color _triggerColor;
        Color _triggerGrabbedColor;
        Color _triggerActiveColor;

        SpriteFont _font;

        public DelayLevelVisualizer(RectangleF rectangle)
        {
            _rectangle = rectangle;

            _boxColor = Color.FromNonPremultiplied(66, 100, 234, 255);
            _meterColor = Color.FromNonPremultiplied(102, 130, 238, 255);

            _triggerColor = Color.FromNonPremultiplied(5, 96, 150, 150);
            _triggerActiveColor = Color.FromNonPremultiplied(96, 15, 160, 150);
            _triggerGrabbedColor = Color.FromNonPremultiplied(196, 115, 260, 255);

            _font = Fonts.GetFont(Game1.Instance.GraphicsDevice, "Ubuntu.ttf", 14);
        }

        public void Update(bool focused)
        {
            if (focused)
            {
                MouseState mouseState = Mouse.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed && _rectangle.Intersects(new Rectangle(mouseState.Position.X, mouseState.Position.Y, 1, 1)))
                {
                    float mouseRelativePos = _rectangle.Bottom - mouseState.Y;
                    TriggerLevel = (mouseRelativePos / _rectangle.Height);

                    TriggerLevel = Math.Clamp(TriggerLevel, 0, 1);
                    _triggerGrabbed = true;

                }else { _triggerGrabbed = false; }
            }else
            {
                _triggerGrabbed = false;
            }

            TriggerActive = CurrentValue / MaxValue >= TriggerLevel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float ratio = CurrentValue / MaxValue;

            // Draw level
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * ratio), _rectangle.Width, (_rectangle.Height * ratio)), _meterColor);

            // Draw trigger
            Color triggerColor = ratio >= TriggerLevel ? _triggerActiveColor : _triggerColor;
            spriteBatch.FillRectangle(new RectangleF(_rectangle.X, _rectangle.Bottom - (_rectangle.Height * TriggerLevel), _rectangle.Width, 4), _triggerGrabbed ? _triggerGrabbedColor : triggerColor);
            
            if (_triggerGrabbed)
            {
                float Y = _rectangle.Y + _rectangle.Height - (_rectangle.Height * TriggerLevel) + 2;

                spriteBatch.DrawLine(_rectangle.X, Y, _rectangle.Right, Y, Color.DarkRed);
            }

            // Draw box
            spriteBatch.DrawRectangle(new RectangleF(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height), _boxColor);

            spriteBatch.DrawString(_font, $"Trigger: {TriggerLevel} Value: {ratio}", new Vector2(_rectangle.X, _rectangle.Y - 16), Color.White);
        }


    }
}
