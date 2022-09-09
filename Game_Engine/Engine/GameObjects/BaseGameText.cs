using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.GameObjects
{
    public class BaseGameText : BaseGameObject
    {
        private SpriteFont _font;

        public string Text { get; set; }

        public SpriteFont Font
        {
            get
            {
                return _font;
            }
        }

        public BaseGameText(SpriteFont font)
        {
            _font = font;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(_font, Text, _position,Color.White);
        }
    }
}
