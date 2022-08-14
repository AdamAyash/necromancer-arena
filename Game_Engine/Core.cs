using Game_Engine.States;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.States;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine
{
    public class Core : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BaseGameState _currentGameState;

        public Core(BaseGameState beginingState)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = @"C:\Users\Adam Ayash\Desktop\Game_Engine\Game_Engine\Content\bin\Windows\";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 400;
            _graphics.ApplyChanges();
            SwichToNewState(beginingState);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        private void SwichToNewState(BaseGameState newState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateSwitched -= _currentGameState_OnStateSwitched;
            }
            _currentGameState?.UnloadContent();
            _currentGameState = newState;
            _currentGameState.Intitialize(Content);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _currentGameState.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
