using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearningEssentials.src
{
    public static class Dataset
    {
        public static void MakeDataset(
            int start, int end, double mu1, double mu2, int answer,
            double[][] x, int[] t, Random rng)
        {
            var gaussian = new GaussianDistribution(0.0, 1.0, rng);

            for (int i = start; i < end; ++i)
            {
                x[i][0] = gaussian.Random() + mu1;
                x[i][1] = gaussian.Random() + mu2;
                t[i] = answer;
            }
        }
    }
}
