using Microsoft.Xna.Framework;
using Game_Engine.Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Projectiles
{
    public class Projectile : BaseGameObject
    {
        private const float PROJECTILE_SPEED = 3.2f;
        public Projectile(Texture2D texture,Vector2 originPosition)
        {
            _objectTexture = texture;
            _position = originPosition;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
