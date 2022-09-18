using Microsoft.Xna.Framework;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.GameObjects.GameplayStateObjects.Player;

namespace Game_Engine.GameObjects.UI
{
    public class PlayerHealth : BaseGameObject
    {
        private const int MAX_HEALTH = 6;

        private Texture2D[] _playerHealthTextures;
        public int PlayerHealthIndex 
        {
            set
            {
                if (value >= 6)
                {
                    _objectTexture = _playerHealthTextures[MAX_HEALTH];
                }
                else if(value <= 0)
                {
                    _objectTexture = _playerHealthTextures[0];
                }
                else if(value > 0 || value < MAX_HEALTH)
                {
                    _objectTexture = _playerHealthTextures[value];
                }
            }
        }
        public PlayerHealth(Texture2D[] playerHealthTextures)
        {
            _playerHealthTextures = playerHealthTextures;
            Position = new Vector2(0,-5);
            PlayerHealthIndex = 6;
        }
    }
}
