using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.GameObjects
{
    public class BaseGameObject
    {
        protected Vector2 _position;
        protected Vector2 _direction;
        protected Vector2 _origin;
        protected Texture2D _objectTexture;

        protected float _scale;
        protected float _angle;

        public int zIndex { get; set; }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }
        }

        public virtual void Update(GameTime gameTime) { }

        protected Vector2 CalculateDirection(float offsetAngle = 0.0f)
        {
            _direction = new Vector2((int)Math.Cos(_angle - offsetAngle),(int)Math.Sin(_angle - offsetAngle));
            _direction.Normalize();

            return _direction;
        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_objectTexture, new Rectangle((int)_position.X, (int)_position.Y,_objectTexture.Width, _objectTexture.Height),null, Color.White,_angle, _origin, SpriteEffects.None,0f);
        }

    }
}
