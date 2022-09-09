using Microsoft.Xna.Framework;

namespace Game_Engine.Engine.ParticleSystem.EmiiterTypes
{
    public interface IEmitterType
    {
        Vector2 GetParticleDirection();
        Vector2 GetParticlePosition(Vector2 emitterPosition);

    }
}
