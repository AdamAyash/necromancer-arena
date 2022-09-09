using Game_Engine.Engine.ParticleSystem;
using Game_Engine.Engine.ParticleSystem.EmiiterTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Engine.ParticleSystem
{
    public class BloodParticleState : EmitterParticleState
    {
        public override int MinLifeSpan => 60;

        public override int MaxLifeSpan => 500;

        public override float Velocity => 1.0f;

        public override float VelocityDeviation => 0f;

        public override float Acceleration => 0.1f;

        public override float Opacity => 100;

        public override float OpacityDeviation => 0f;

        public override float OpacityFadingRate => 1f;

        public override float Rotation => 0.0f;

        public override float RotationDeviation => 0.0f;

        public override float Scale => 0.2f;

        public override float ScaleDeviation => 0.05f;

        public override Vector2 Gravity => new Vector2(0, 1);
    }

    public class BloodEmitter : Emitter
    {
        private const int NbParticles = 10;
        private const int MaxParticles = 10;

        private static Vector2 Direction = new Vector2(0.0f, -1.0f);
        private const float Spread = 3.1f;

        public BloodEmitter(Texture2D texture, Vector2 position)
            :base(texture,position,new BloodParticleState(),
                 new ConeEmitterType(Direction,Spread),NbParticles,MaxParticles)
        {

        }
    }

}
