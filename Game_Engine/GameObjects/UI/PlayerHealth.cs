using Microsoft.Xna.Framework;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Game_Engine.GameObjects.GameplayStateObjects.Player;

namespace Game_Engine.GameObjects.UI
{
    public class PlayerHealth : BaseGameObject
    {
        private Texture2D[] _playerHealthTextures;
        public int PlayerHealthIndex 
        {
            set
            {
                _objectTexture = _playerHealthTextures[value];
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
