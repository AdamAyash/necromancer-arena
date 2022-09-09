using Game_Engine.Engine.GameObjects;
using Game_Engine.Engine.ParticleSystem.EmiiterTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game_Engine.Engine.ParticleSystem
{
    public class Emitter : BaseGameObject
    {
        private LinkedList<Particle> _activeParticles = new LinkedList<Particle>();
        private LinkedList<Particle> _inactiveParticles = new LinkedList<Particle>();
        private EmitterParticleState _emitterParticleState;
        private IEmitterType _emitterType;
        private int _nbOfPartcilesEmmitedPerUpdate = 0;
        private int _maxNbParticles = 0;

        public Emitter(Texture2D texture, Vector2 position, EmitterParticleState particleState,
            IEmitterType emiiterType, int nbParticlePerUpdate, int maxParticles)
        {
            _emitterParticleState = particleState;
            _emitterType = emiiterType;
            _objectTexture = texture;
            _nbOfPartcilesEmmitedPerUpdate = nbParticlePerUpdate;
            _maxNbParticles = maxParticles;
            Position = position;
        }

        private void EmitNewParticle(Particle particle)
        {
            var lifespan = _emitterParticleState.GenerateLifeSpan();
            var velocity = _emitterParticleState.GenerateVelocity();
            var scale = _emitterParticleState.GenerateScale();
            var rotation = _emitterParticleState.GenerateRotation();
            var opacity = 1f;
            var gravity = _emitterParticleState.Gravity;
            var acceleration = _emitterParticleState.Acceleration;
            var opacityFadingRate = 100f;

            var direction = _emitterType.GetParticleDirection();
            var position = _emitterType.GetParticlePosition(_position);

            particle.Activate(lifespan,position,direction,gravity,acceleration,rotation,opacity,scale,opacityFadingRate);
            _activeParticles.AddLast(particle);
        }

        private void EmitParticles()
        {
            if (_activeParticles.Count >= _maxNbParticles)
            {
                return;
            }
            var maxAmountThatCanBeCreated = _maxNbParticles - _activeParticles.Count;
            var neededParticles = Math.Min(maxAmountThatCanBeCreated, _nbOfPartcilesEmmitedPerUpdate);

            var nbToReause = Math.Min(_inactiveParticles.Count, neededParticles);

            var nbToCreate = neededParticles - nbToReause;

            for (int i = 0; i < nbToReause; i++)
            {
                var particleNode = _inactiveParticles.First;

                EmitNewParticle(particleNode.Value);
                _inactiveParticles.Remove(particleNode);
            }

            for (int i = 0; i < nbToCreate; i++)
            {
                EmitNewParticle(new Particle());
            }
        }

        public override void Update(GameTime gameTime)
        {
            EmitParticles();

            var particleNode = _activeParticles.First;
            while (particleNode != null)
            {
                var nextNode = particleNode.Next;
                var stillAlive = particleNode.Value.Update();

                if (!stillAlive)
                {
                    _activeParticles.Remove(particleNode);
                    _inactiveParticles.AddLast(particleNode.Value);
                }
                particleNode = nextNode;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            var sourceRectangle = new Rectangle(0, 0, _objectTexture.Width, _objectTexture.Height);

            foreach (var particle in _activeParticles)
            {
                _spriteBatch.Draw(_objectTexture, particle.Position, sourceRectangle, Color.White * particle.Opacity, 0.0f, Vector2.Zero, particle.Scale, SpriteEffects.None, zIndex);
            }
        }
    }
}
