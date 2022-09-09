using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game_Engine.Engine.Animation;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies
{
    public class OldWizard_Enemy : AI
    {
        private TimeSpan enemyShootCooldown;
        private float lastEnemyShotAt;

        public event EventHandler<Vector2> OnEnemyShoot;
        public OldWizard_Enemy(List<Animation> animations, Vector2 position) :
            base(animations,position)
        {
            _currentAnimation = _animations[0];
            AddBoundingBoxes(new Engine.GameObjects.BoundingBox(Position, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height));
            Health = 1;
            enemyShootCooldown = TimeSpan.FromSeconds(3);
            lastEnemyShotAt = 0f;
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);
            if (_attackMode)
            {
                _currentAnimation.Update(gameTime);
                if ((float)gameTime.TotalGameTime.TotalSeconds - lastEnemyShotAt >= enemyShootCooldown.TotalSeconds)
                {
                    lastEnemyShotAt = (float)gameTime.TotalGameTime.TotalSeconds;
                    OnEnemyShoot?.Invoke(this, Position);
                }
            }
        }
    }
}
