using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.GameObjects
{
    public class BaseGameObject
    {
        protected Vector2 _position;
        protected Texture2D _objectTexture;
        protected float _angle;

        public int zIndex { get; set; }

        public virtual void Update(GameTime gameTime) { }
        

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_objectTexture, _position, Color.White);
        }

    }
}
