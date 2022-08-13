using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.GameObjects
{
    public class BaseGameObject
    {
        protected Vector2 _position;
        protected Texture2D _objectTexture;

        public int ZIndex { get; set; }

        protected virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_objectTexture, _position, Color.White);
        }

    }
}
