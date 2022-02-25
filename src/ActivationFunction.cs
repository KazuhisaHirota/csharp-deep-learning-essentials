using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearningEssentials.src
{
    public static class ActivationFunction
    {
        public static int Step(double x)
        {
            return x < 0.0 ? -1 : 1;
        }
    }
}
