using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Engine.Engine.ParticleSystem
{
    public class Particle
    {
        private int _lifespan;
        private int _age;

        private float _velocity;
        private float _acceleration;
        private float _opacityFadingRate;
        private float _rotation;

        private Vector2 _direction;
        private Vector2 _gravity;

        public Vector2 Position { get; set; }
        public float Opacity { get; set; }
        public float Scale { get; set; }
        public Particle()
        {

        }
         
        public bool Update()
        {
            _velocity += _acceleration;
            Position += _direction * _velocity;
            Opacity *= _opacityFadingRate;
            _age++;
            return _age < _lifespan;

        }

        public void Activate(int lifespan, Vector2 position, Vector2 direction,
            Vector2 gravity, float acceleration, float rotation, float opacity,
            float scale, float opacityfadingRate)
        {
            _lifespan = lifespan;
            _direction = direction;
            _gravity = gravity;
            _acceleration = acceleration;
            _rotation = rotation;
            _opacityFadingRate = opacityfadingRate;
            Position = position;
            Scale = scale;
            Opacity = opacity;
        }
    }
}
