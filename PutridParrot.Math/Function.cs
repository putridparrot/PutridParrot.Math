using System;
using System.Numerics;

namespace PutridParrot.Math
{
    public static partial class Function
    {
        /// <summary>
        /// Determine's whether a number is triangular
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsTriangular(double x)
        {
            var n = (System.Math.Sqrt(8 * x + 1) - 1) / 2;
            return System.Math.Abs(System.Math.Floor(n) - n) < double.Epsilon * 100;
        }

        public static BigInteger Factorial(BigInteger x)
        {
            if (x >= 0)
            {
                var result = new BigInteger(1);
                for (var i = 2; i <= x; i++)
                {
                    result *= i;
                }
                return result;
            }
            else
            {
                var result = new BigInteger(-1);
                for (var i = -2; i >= x; i--)
                {
                    result *= i;
                }
                return result;
            }
        }

        public static double Factorial(int x)
        {
            if (x >= 0)
            {
                var result = 1d;
                for (var i = 2; i <= x; i++)
                {
                    result *= i;
                }
                return result;
            }
            else
            {
                var result = -1d;
                for (var i = -2; i >= x; i--)
                {
                    result *= i;
                }
                return result;
            }
        }

        /// <summary>
        /// Calculates the binomial coefficient
        /// given n & k where n >= k >= 0
        /// See https://en.wikipedia.org/wiki/Binomial_coefficient
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double BinomialCoefficient(int n, int k)
        {
            if (n >= k && k >= 0)
            {
                return Factorial(n) / (Factorial(k) * Factorial(n - k));
            }
            throw new ArgumentException("n must be >= k and k must be >= 0");
        }
    }
}
