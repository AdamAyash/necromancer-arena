using System;
using Microsoft.Xna.Framework;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Projectiles
{
    public class Projectile : BaseGameObject
    {
        private const float PROJECTILE_SPEED = 4.5f;
        private const int  PLAYER_POSITION_OFFSET = 16;

        private Vector2 _mousePosition;

        public Projectile(Texture2D texture,Vector2 playerPosition,Vector2 mousePosition)
        {
            _objectTexture = texture;
            _position = playerPosition;
            _position.X += PLAYER_POSITION_OFFSET;
            _position.Y += PLAYER_POSITION_OFFSET;
            _mousePosition = mousePosition;
            _origin = new Vector2(TextureWidth/2,TextureHeight/2);
            _direction = mousePosition - _position;
            _direction.Normalize();
            _angle = (float)Math.Atan2(_direction.Y, _direction.X);
        }

        public override void Update(GameTime gameTime)
        {
            _position += _direction * PROJECTILE_SPEED;
        }
    }
}
