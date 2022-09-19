using Game_Engine.States;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine
{
    public class Core : Game
    {
        private const int DESIGNED_REOLUTION_WIDTH = 1200;
        private const int DESIGNED_REOLUTION_HEIGHT = 720;

        private const float DESIGNED_RESOLUTION_ASPECT_RATIO =
            DESIGNED_REOLUTION_WIDTH / (float)DESIGNED_REOLUTION_HEIGHT;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BaseGameState _currentGameState;

        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;

        public Core(BaseGameState beginingState)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 720;
            Window.AllowUserResizing = false;
            _graphics.ApplyChanges();
            SwichToNewState(beginingState);

        }

        protected override void Initialize()
        {
            _renderTarget = new RenderTarget2D(GraphicsDevice, DESIGNED_REOLUTION_WIDTH, DESIGNED_REOLUTION_HEIGHT, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            RescaleWindow();
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            base.Initialize();
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            RescaleWindow();
        }

        private void RescaleWindow()
        {
            _renderScaleRectangle = GetScaleRectangle();
        }

        private Rectangle GetScaleRectangle()
        {
            var variance = 0.5f;
            var actualAspectRatio = Window.ClientBounds.Width /
                (float)Window.ClientBounds.Height;
            Rectangle scaleRectangle;

            if (actualAspectRatio <= DESIGNED_RESOLUTION_ASPECT_RATIO)
            {
                var presentHeight = (int)(Window.ClientBounds.Width / DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                var presentWidth = (int)(Window.ClientBounds.Height * DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }
            return scaleRectangle;
        }
        private void SwichToNewState(BaseGameState newState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateSwitched -= _currentGameState_OnStateSwitched;
            }
            _currentGameState?.UnloadContent();
            _currentGameState = newState;
            _currentGameState.Intitialize(Content, GraphicsDevice);
            _currentGameState.LoadContent();
            _currentGameState.OnStateSwitched += _currentGameState_OnStateSwitched;

        }

        private void _currentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwichToNewState(e);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            _currentGameState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _currentGameState.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
