
using Game_Engine.Engine.Animation;
using Game_Engine.GameObjects.GameplayStateObjects.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Game_Engine.GameObjects.GameplayStateObjects.Enemies.Bosses
{
    public class DemonBoss : AI
    {
        private float _distanceTrigger;

        public event EventHandler<Vector2> OnBossMassShooting;
        public DemonBoss(List<Animation> animations, Vector2 position) : base(animations, position)
        {
            _currentAnimation = _animations[0];
            AddBoundingBoxes(new Engine.GameObjects.BoundingBox(Position, _currentAnimation.CurrentFrame.Width, _currentAnimation.CurrentFrame.Height));
            Health = 15;
            Damage = 1;
            _distanceTrigger = 40f;
            _speed = 2.0f;


        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);

            if (_attackMode)
            {
                _currentAnimation.Update(gameTime);
                var distance = playerPosition - _position;
                if (distance.Length() > _distanceTrigger)
                {
                    distance.Normalize();
                    Position += distance * _speed;
                }

            }
        }

        public override void JustHit(IGameObjectWidthDamage o)
        {
            Projectile projectile = (Projectile)o;
            if (projectile.IsActive)
            {
                this.Health -= o.Damage;
                OnBossMassShooting.Invoke(this, _position);
                _hitAt = 0;
            }
        }
    }
}
