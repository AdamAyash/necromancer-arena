using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies
{
    public class Demon_Enemy : AI
    {
        private float distanceTrigger;
        public Demon_Enemy(List<Animation> animations, Vector2 position) : base(animations, position)
        {
            _speed = 3.5f;
            _currentAnimation = _animations[0];
            AddBoundingBoxes(new Engine.GameObjects.BoundingBox(Position, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height));
            distanceTrigger = 40f;
            Health = 100;
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
