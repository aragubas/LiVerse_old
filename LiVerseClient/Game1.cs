using Cyotek.Drawing.BitmapFont;
using LiVerseFramework;
using LiVerseFramework.AnaBanUI;
using LiVerseFramework.Character;
using LiVerseFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;

namespace LiVerseClient
{
    public class Game1 : Game, IClientInstance
    {
        public MMDevice Microphone { get; set; }
        public ICharacter Character { get; set; }
        public bool TransparentMode { get; set; }
        public ObservableCollection<ICharacterAnimation> Animations { get; set; } = new();
        public IUiRoot UIRoot { get; }
        public Game GameInstance { get; }


        public FontDescriptor CommonFont;

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        WaveInEvent _waveIn;

        VolumeLevelVisualizer _volumeLevel;
        DelayLevelVisualizer _delayLevel;

        Timer _delayResetTimer;
        float _delayValue = 0;
        float _delayValueTarget = 0;

        KeyboardState _oldKeyboardState;
        FontDescriptor _copyrightTextFont;

        // For debugging purposes
        GameTime _lastGameTime;

        public void AddCharacterAnimation(ICharacterAnimation animation)
        {
            Animations.Add(animation);
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            UIRoot = new UIRoot(this);

            GameInstance = this;
        }

        protected override void Initialize()
        {
            _waveIn = new WaveInEvent();

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
                    Microphone = device;
                    break;
                }
            }

            _volumeLevel = new VolumeLevelVisualizer(new RectangleF(32, 32, 20, 400));
            _delayLevel = new DelayLevelVisualizer(new Rectangle(58, 32, 20, 400));

            Character = new DefaultCharacter(this);

            Window.AllowUserResizing = true;

            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = false;
            _graphics.ApplyChanges();

            Window.ClientSizeChanged += Window_ClientSizeChanged;

            Fonts.CommonGraphicsDevice = GraphicsDevice;

            PluginHost.InstanceManager.LoadPlugin(this, "aragubas.tests.testplugin");
            PluginHost.InstanceManager.LoadPlugin(this, "aragubas.liverseCore.defaultAnimations");

            base.Initialize();
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            UIRoot.WindowResized();
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

            // Pre-Cache fonts
            Fonts.LoadFont("Ubuntu.ttf", 14);

            _copyrightTextFont = new FontDescriptor("Ubuntu-Light.ttf", 24);
            CommonFont = new FontDescriptor("Ubuntu.ttf", 14);

            Window.Title = "LiVerse Alpha";
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape))
                TransparentMode = !TransparentMode;
            _oldKeyboardState = Keyboard.GetState();

            _volumeLevel.CurrentValue = Microphone.AudioMeterInformation.MasterPeakValue * 100f;
            _volumeLevel.Update(gameTime, IsActive || !TransparentMode);

            _delayValue = MathHelper.LerpPrecise(_delayValue, _delayValueTarget, 0.5f);
            _delayLevel.CurrentValue = _delayValue;

            _delayLevel.Update(IsActive || !TransparentMode);

            if (_volumeLevel.TriggerActive)
            {
                _delayValueTarget = 100;
                _delayValue = 100;
            }

            Character.Update(gameTime);
            Character.Speaking = _delayLevel.TriggerActive;

            _lastGameTime = gameTime;

            UIRoot.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(TransparentMode ? Color.Transparent : Color.CornflowerBlue);

            _spriteBatch.Begin();

            Character.Draw(_spriteBatch);
            if (!TransparentMode)
            {
                _delayLevel.Draw(_spriteBatch);
                _volumeLevel.Draw(_spriteBatch);

#if DEBUG
                _spriteBatch.DrawString(Fonts.GetFont(CommonFont), $"Frametime: {gameTime.ElapsedGameTime.TotalSeconds}", new Vector2(16, GraphicsDevice.Viewport.Height - 50), Color.White);
#endif

                _spriteBatch.DrawString(Fonts.GetFont(_copyrightTextFont), "LiVerse Alpha", new Vector2(16, GraphicsDevice.Viewport.Height - 38), Color.White);
            }


            _spriteBatch.End();

            UIRoot.Draw(_spriteBatch);


            UIRoot.DrawWindows(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}