using Cyotek.Drawing.BitmapFont;
using LiVerseFramework.Character;
using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Timers;

namespace LiVerseClient
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        WaveInEvent _waveIn;
        MMDevice _microphone;

        public static Game1 Instance;

        VolumeLevelVisualizer _volumeLevel;
        DelayLevelVisualizer _delayLevel;

        ICharacter _character;

        Timer _delayResetTimer;
        float _delayValue = 0;
        float _delayValueTarget = 0;

        public bool TransparentMode = false;
        KeyboardState _oldKeyboardState;
        SpriteFont _copyrightTextFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Instance = this;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _waveIn = new WaveInEvent();

            //int waveInDeviceCount = WaveInEvent.DeviceCount;
            //Console.WriteLine($"{waveInDeviceCount} Devices Detected");
            //Console.WriteLine($"Find the device ID of your mic below and use it as the value for the value of _micDeviceId");
            //for (int i = 0; i < waveInDeviceCount; i++)
            //{
            //    WaveInCapabilities capabilities = ;
            //    Console.WriteLine($"Device ID: {i} | Name: {capabilities.ProductName}");
            //}

            _delayResetTimer = new Timer();
            _delayResetTimer.Interval = 50;
            _delayResetTimer.Elapsed += _delayResetTimer_Elapsed;
            _delayResetTimer.Start();

            _waveIn.DeviceNumber = 0;
            _waveIn.BufferMilliseconds = 32;
            _waveIn.NumberOfBuffers = 1;
            _waveIn.StartRecording();

            MMDeviceEnumerator enm = new MMDeviceEnumerator();
            var devices = enm.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            foreach (var device in devices)
            {
                if (device.FriendlyName.Contains(WaveInEvent.GetCapabilities(0).ProductName))
                {
                    _microphone = device;
                    break;
                }
            }

            _volumeLevel = new VolumeLevelVisualizer(new RectangleF(32, 32, 20, 400));
            _delayLevel = new DelayLevelVisualizer(new Rectangle(58, 32, 20, 400));

            _character = new DefaultCharacter();
            Window.AllowUserResizing = true;

            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        private void _delayResetTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_volumeLevel != null)
            {
                if (!_volumeLevel.TriggerActive)
                {
                    if (_delayValueTarget != 0)
                        _delayValueTarget = _delayValueTarget / 2;
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Pre-Cache font
            Fonts.LoadFont(GraphicsDevice, "Ubuntu.ttf", 14);
            _copyrightTextFont = Fonts.GetFont(GraphicsDevice, "Ubuntu-Light.ttf", 24);

            Window.Title = "LiVerse";
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape))
                TransparentMode = !TransparentMode;
            _oldKeyboardState = Keyboard.GetState();

            _volumeLevel.CurrentValue = _microphone.AudioMeterInformation.MasterPeakValue * 100f;
            _volumeLevel.Update(IsActive || !TransparentMode);

            _delayValue = MathHelper.LerpPrecise(_delayValue, _delayValueTarget, 0.5f);
            _delayLevel.CurrentValue = _delayValue;

            _delayLevel.Update(IsActive || !TransparentMode);

            if (_volumeLevel.TriggerActive)
            {
                _delayValueTarget = 100;
                _delayValue = 100;
            }

            _character.Update(gameTime);
            _character.Speaking = _delayLevel.TriggerActive;



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(TransparentMode ? Color.Transparent : Color.CornflowerBlue);


            _spriteBatch.Begin();

            _character.Draw(_spriteBatch);
            if (!TransparentMode)
            {
                _delayLevel.Draw(_spriteBatch);
                _volumeLevel.Draw(_spriteBatch);
                _spriteBatch.DrawString(_copyrightTextFont, "LiVerse Alpha by Aragubas", new Vector2(16, GraphicsDevice.Viewport.Height - 38), Color.White);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}