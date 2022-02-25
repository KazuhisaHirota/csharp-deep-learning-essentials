using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearningEssentials.src
{
    public class Perceptrons
    {
        public Perceptrons(int nIn)
        {
            nIn_ = nIn;
            w_ = new double[nIn];
        }

        public int Train(double[] x, int t, double learningRate)
        {
            // check if the data is classified correctly
            double c = 0.0;
            for (int i = 0; i < nIn_; ++i)
                c += w_[i] * x[i] * t;

            // apply steepest descent method if the data is wrongly classified
            int classified = 0;
            if (c > 0.0) // correct
                classified = 1;
            else // wrong
                for (int i = 0; i < nIn_; ++i)
                    w_[i] += learningRate * x[i] * t;
            
            return classified;
        }

        public int Predict(double[] x)
        {
            double preActivation = 0.0;
            for (int i = 0; i < nIn_; ++i)
                preActivation += w_[i] * x[i];

            return ActivationFunction.Step(preActivation);
        }

        private readonly int nIn_; // dimensions of input data
        private readonly double[] w_; // weight vector of perceptrons
    }
}
