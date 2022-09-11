using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies.Zombie
{
    class Zombie : AI,IGameObjectWidthDamage
    {
        private float distanceTrigger;
        public Zombie(List<Animation> animations, Vector2 position) :base(animations,position)
        {
            _currentAnimation = _animations[0];
            AddBoundingBoxes(new Engine.GameObjects.BoundingBox(Position, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height));
            Health = 4;
            Damage = 1;
            distanceTrigger = 40f;
            _speed = 2.5f;
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);

            if (_attackMode)
            {
                _currentAnimation.Update(gameTime);
                var distance = playerPosition - _position;
                if (distance.Length() > distanceTrigger)
                {
                    distance.Normalize();
                    Position += distance * _speed;
                }
            }
        }
    }
}
