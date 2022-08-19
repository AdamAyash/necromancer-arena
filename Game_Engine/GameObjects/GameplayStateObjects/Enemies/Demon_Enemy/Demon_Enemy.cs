using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies
{
    public class Demon_Enemy : AI
    {
        private float distanceTrigger;
        public Demon_Enemy(List<Animation> animations) : base(animations)
        {
            _speed = 3.5f;
            _currentAnimation = _animations[0];
            distanceTrigger = 50f;
        }

        public override void Update(GameTime gameTime,Vector2 playerPosition)
        {
            _currentAnimation.Update(gameTime);
            var distance = playerPosition - _position;
            if (distance.Length() > distanceTrigger)
            {
                distance.Normalize();
                _position += distance * _speed;
            }
            base.Update(gameTime, playerPosition);
        }
     
    }
}
