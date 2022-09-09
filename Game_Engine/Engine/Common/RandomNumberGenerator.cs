
using System;

namespace Game_Engine.Engine.Common
{
    public class RandomNumberGenerator
    {
        private Random _rnd;

        public RandomNumberGenerator()
        {
            _rnd = new Random();
        }

        public double GenerateRandomFloat()
        {
            return _rnd.NextDouble();
        }

        public int GenerateRandomInteger(int min, int max)
        {
            return _rnd.Next(min, max);
        }

        public int NextRandom() => _rnd.Next();
        public int NextRandom(int max) => _rnd.Next(max);

        public int NextRandom(int min, int max) => _rnd.Next(min, max);

        public float NextDouble(float max) => (float)_rnd.NextDouble() * max;

        public float NextRandom(float min, float max) => ((float)_rnd.NextDouble() * (max - min)) + min;
    }
}
