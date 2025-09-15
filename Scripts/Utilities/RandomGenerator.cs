using System;

namespace Pong_1.Scripts.Utilities
{
    public static class RandomGenerator<T>
    {
        private static readonly Random _random = new Random();

        public static T PickRandom(params T[] values)
        {
            if (values is null || values.Length == 0)
            {
                return default(T);
            }

            return values[_random.Next(values.Length)];
        }
    }
}