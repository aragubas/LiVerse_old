using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Diagnostics;

namespace LiVerseClient
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        int _mouseY = 0;
        int value = 0;
        float peak = 0;

        WaveInEvent _waveIn;
        MMDevice _microphone;

        public static Game1 Instance;

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
            _waveIn.BufferMilliseconds = 16;
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

            // TODO: Add your update logic here
            _mouseY = MathHelper.Clamp(Mouse.GetState().Y, 0, 100);

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            float height = 400;
            float maxLevel = 100;
            float ceira = Math.Clamp(_microphone.AudioMeterInformation.MasterPeakValue * 100.0f, 0, maxLevel);
            float ratio = ceira / maxLevel;

            if (ceira < peak)
            {
                peak = ceira;
            }

            // Draw level
            _spriteBatch.FillRectangle(new RectangleF(32, 32 + height - (height * ratio), 20, (height * ratio)), Color.LightBlue);
            _spriteBatch.DrawRectangle(new RectangleF(32, 32, 20, 400), Color.Blue);

            _spriteBatch.DrawString(Fonts.GetFont("Ubuntu.ttf", 14), $"Level: {ceira}", new Vector2(16, 16), Color.White);


            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}