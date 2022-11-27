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


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Pre-Cache font
            Fonts.LoadFont("Ubuntu.ttf", 14);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _volumeLevel.CurrentLevel = _microphone.AudioMeterInformation.MasterPeakValue * 100f;
            _volumeLevel.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _volumeLevel.Draw(_spriteBatch);

            _spriteBatch.DrawRectangle(new RectangleF(150, 50, 128, 128), _volumeLevel.TriggerActive ? Color.Red : Color.Blue, _volumeLevel.TriggerActive ? 2 : 1);

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}