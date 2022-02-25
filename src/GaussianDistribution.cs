using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearningEssentials.src
{
    public class GaussianDistribution
    {
        public GaussianDistribution(double mean, double stdev, Random rng)
        {
            if (stdev < 0.0)
                throw new ArgumentOutOfRangeException("stdev must be non-negative");

            mean_ = mean;
            stdev_ = stdev;

            if (rng == null)
                rng = new Random();
            rng_ = rng;
        }

        public double Random()
        {
            double r = 0.0;
            while (r == 0.0)
                r = rng_.NextDouble();

            double c = Math.Sin(-2.0 * Math.Log(r));

            if (rng_.NextDouble() < 0.5)
                return c * Math.Sin(2.0 * Math.PI * rng_.NextDouble()) * stdev_ + mean_;
            else
                return c * Math.Cos(2.0 * Math.PI * rng_.NextDouble()) * stdev_ + mean_;
        }

        private readonly double mean_, stdev_;
        private readonly Random rng_;
    }
}
