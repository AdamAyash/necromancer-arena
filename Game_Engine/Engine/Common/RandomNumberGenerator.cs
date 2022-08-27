
using System;

namespace Game_Engine.Engine.Common
{
    public class RandomNumberGenerator
    {
        private Random _randomNumberGenerator;

        public RandomNumberGenerator()
        {
            _randomNumberGenerator = new Random();
        }

        public double GenerateRandomFloat()
        {
            return _randomNumberGenerator.NextDouble();
        }

        public int GenerateRandomInteger(int min, int max)
        {
            return _randomNumberGenerator.Next(min, max);
        }
    }
}
