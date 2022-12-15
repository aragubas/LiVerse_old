using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.AnaBanUI.Controls
{
    public class Button : Element
    {
        enum ButtonState
        {
            /// <summary>
            /// Default State
            /// </summary>
            Normal,
            
            /// <summary>
            /// When pointer is hovering over the Button
            /// </summary>
            Hover,
            
            /// <summary>
            /// When clicked
            /// </summary>
            Active
        }

        Label label { get; }
        Color _normalBackground = Color.FromNonPremultiplied(15, 27, 44, 255);
        Color _hoverBackground = Color.FromNonPremultiplied(30, 48, 72, 255);
        Color _activeBackground = Color.FromNonPremultiplied(48, 66, 91, 255);

        Color _normalBorder = Color.FromNonPremultiplied(255, 51, 102, 127);
        Color _hoverBorder = Color.FromNonPremultiplied(255, 51, 102, 255);
        Color _activeBorder = Color.FromNonPremultiplied(46, 196, 182, 255);

        Color _currentBackground;
        Color _currentBorder;

        ButtonState _state = ButtonState.Normal;
        bool _mouseDownLock = false;

        MouseState _oldMouseState;

        public Button()
        {
            label = new Label("Button", new Graphics.FontDescriptor("Ubuntu.ttf", 14));

            Padding = new Vector2(4);
            ContentSize = label.ContentSize;

            label.OnResized += Label_OnResized;
        }

        private void Label_OnResized()
        {
            ContentSize = label.ContentSize;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (GlobalRectangle.Contains(mouseState.Position))
            {
                if (_oldMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    if (!_mouseDownLock) { _mouseDownLock = true; }
                }

                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && _mouseDownLock)
                {
                    _state = ButtonState.Active;

                }else { _state = ButtonState.Hover; }

            }
            else
            {
                _state = ButtonState.Normal;
                _mouseDownLock = false;
            }

            _oldMouseState = Mouse.GetState();
            
            // Changes Background/Border color based on State
            switch (_state) 
            {
                case ButtonState.Normal:
                    _currentBackground = _normalBackground;
                    _currentBorder = _normalBorder;
                    break;

                case ButtonState.Hover:
                    _currentBackground = _hoverBackground;
                    _currentBorder = _hoverBorder;
                    break;

                case ButtonState.Active:
                    _currentBackground = _activeBackground;
                    _currentBorder = _activeBorder;
                    break;
            }
        }

        protected override void Moved()
        {
            base.Moved();

            if (label != null)
                label.Position = ContentPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Background
            spriteBatch.FillRectangle(BoxRectangle, _currentBackground);

            // Button Label
            label.Draw(spriteBatch);

            // Border
            spriteBatch.DrawRectangle(BoxRectangle, _currentBorder);

        }


    }
}
