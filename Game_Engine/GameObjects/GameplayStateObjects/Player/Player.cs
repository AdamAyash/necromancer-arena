using Microsoft.Xna.Framework;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Player
{
    public class Player : BaseGameObject
    {
        private const float PLAYER_SPEED = 4.5f;
        public Player(Texture2D playerTexture)
        {
            _objectTexture = playerTexture;
        }

        public void PlayerMoveLeft()
        {
            _position = new Vector2(_position.X - PLAYER_SPEED, _position.Y);
        }

        public void PlayerMoveRight()
        {
            _position = new Vector2(_position.X + PLAYER_SPEED, _position.Y);
        }
        public void PlayerMoveUp()
        {
            _position = new Vector2(_position.X, _position.Y - PLAYER_SPEED);
        }

        public void PlayerMoveDown()
        {
            _position = new Vector2(_position.X, _position.Y + PLAYER_SPEED);
        }
    }
}
