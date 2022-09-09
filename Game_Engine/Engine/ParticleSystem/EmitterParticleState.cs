using Game_Engine.Engine.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Engine.Engine.ParticleSystem
{
    public abstract class EmitterParticleState
    {
        private RandomNumberGenerator _rnd = new RandomNumberGenerator();

        public abstract int MinLifeSpan { get; }
        public abstract int MaxLifeSpan { get; }
        public abstract float Velocity { get; }
        public abstract float VelocityDeviation { get; }
        public abstract float Acceleration { get; }
        public abstract float Opacity { get; }
        public abstract float OpacityDeviation { get; }
        public abstract float OpacityFadingRate { get; }
        public abstract float Rotation { get; }
        public abstract float RotationDeviation { get; }
        public abstract float Scale { get; }
        public abstract float ScaleDeviation { get; }


        public abstract Vector2 Gravity { get; }

        public int GenerateLifeSpan()
        {
            return _rnd.NextRandom(MinLifeSpan, MaxLifeSpan);
        }

        public float GenerateVelocity()
        {
            return GenerateFloat(Velocity, VelocityDeviation);
        }

        public float GenerateOpacity()
        {
            return GenerateFloat(Opacity, OpacityDeviation);
        }

        public float GenerateRotation()
        {
            return GenerateFloat(Rotation, RotationDeviation);
        }

        public float GenerateScale()
        {
            return GenerateFloat(Scale, ScaleDeviation);
        }
        private float GenerateFloat(float startN, float deviation)
        {
            var halfDeviation = deviation / 2.0f;
            return _rnd.NextRandom(startN - halfDeviation, startN + halfDeviation);
        }

    }
}
