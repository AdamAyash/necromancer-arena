using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateTextObjects
{
    public class GameOverText : BaseGameText
    {
        public GameOverText(SpriteFont font,Vector2 position) : base(font)
        {
            Text = "Game Over";
            _position = position;
        }
    }
}
