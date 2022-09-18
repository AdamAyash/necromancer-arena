using Game_Engine.Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.States.MainMenuState
{
    public class MainMenuState : BaseGameState
    {
        private const string MAIN_MENU_TEXTURE = "Assets/UI/MainMenu/mainMenu";

        private Texture2D _mainMenuTexture;
        public override void HandleInput(GameTime gameTime)
        {

        }

        public override void LoadContent()
        {
            _mainMenuTexture = LoadTexture(MAIN_MENU_TEXTURE);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_mainMenuTexture, Vector2.Zero, Color.White);
        }
    }
}
