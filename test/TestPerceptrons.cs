using System;
using System.Collections.Generic;
using System.Text;

using DeepLearningEssentials.src;

namespace DeepLearningEssentials.test
{
    public static class TestPerceptrons
    {
        public static void Run()
        {
            Console.WriteLine("set configs");

            var rng = new Random(1234);

            const int trainN = 1000; // number of training data
            const int trainSize = (int)(trainN / 2); // TODO rename

            const int testN = 1000; // number of test data
            const int testSize = (int)(testN / 2); // TODO rename

            const int nIn = 2; // dim of input data
            //const int nOut = 1;

            const int epochs = 100;
            const double learningRate = 1.0; // learning rate can be 1 in perceptrons

            Console.WriteLine("initialize tensors");

            double[][] trainX = new double[trainN][];
            for (int i = 0; i < trainN; ++i) trainX[i] = new double[nIn];
            int[] trainT = new int[trainN]; // answers (labels) for training
            
            double[][] testX = new double[testN][];
            for (int i = 0; i < testN; ++i) testX[i] = new double[nIn];
            int[] testT = new int[testN];

            Console.WriteLine("make dataset");

            // class1 inputs x11 and x12: x11 ~ N(-2.0, 1.0), x12 ~ N(+2.0, 1.0)
            const double mu11 = -2.0;
            const double mu12 = 2.0;
            const int answer1 = 1;
            Dataset.MakeDataset(0, trainSize, mu11, mu12, answer1, trainX, trainT, rng);
            Dataset.MakeDataset(0, testSize, mu11, mu12, answer1, testX, testT, rng);

            // class2 inputs x21 and x22: x21 ~ N(+2.0, 1.0), x22 ~ N(-2.0, 1.0)
            const double mu21 = 2.0;
            const double mu22 = -2.0;
            const int answer2 = -1;
            Dataset.MakeDataset(trainSize, trainN, mu21, mu22, answer2, trainX, trainT, rng);
            Dataset.MakeDataset(testSize, testN, mu21, mu22, answer2, testX, testT, rng);

            // build the model

            // construct
            var classifer = new Perceptrons(nIn);

            // train
            Console.WriteLine("train");
            int epoch = 0; // training epoch counter
            while (true)
            {
                Console.WriteLine("epoch=" + epoch.ToString());

                int classified = 0;
                for (int i = 0; i < trainN; ++i)
                    classified += classifer.Train(trainX[i], trainT[i], learningRate);

                if (classified == trainN) // when all data are classified correctly
                    break;

                epoch += 1;
                if (epoch > epochs)
                    break;
            }

            // test
            Console.WriteLine("test");
            int[] predictedT = new int[testN];
            for (int i = 0; i < testN; ++i)
                predictedT[i] = classifer.Predict(testX[i]);

            // evaluate the model
            Console.WriteLine("evaluate the model");
            int[][] confusionMatrix = new int[2][] { new int[2], new int[2] };
            double accuracy = 0.0;
            double precision = 0.0;
            double recall = 0.0;
            for (int i = 0; i < testN; ++i)
            {
                if (predictedT[i] > 0) // positive
                {
                    if (testT[i] > 0) // TP
                    {
                        accuracy += 1.0;
                        precision += 1.0;
                        recall += 1.0;
                        confusionMatrix[0][0] += 1;
                    } else // FP
                    {
                        confusionMatrix[1][0] += 1;
                    }
                } else // negative
                {
                    if (testT[i] > 0) // FN
                    {
                        confusionMatrix[0][1] += 1;
                    } else // TN
                    {
                        accuracy += 1.0;
                        confusionMatrix[1][1] += 1;
                    }
                }
            }

            accuracy /= testN;

            int nPredictedPositive = confusionMatrix[0][0] + confusionMatrix[1][0];
            precision /= (double)nPredictedPositive;

            int nRealPositive = confusionMatrix[0][0] + confusionMatrix[0][1];
            recall /= (double)nRealPositive;

            Console.WriteLine("Perceptrons model evaluation");
            Console.WriteLine("Accuracy: " + (accuracy * 100.0).ToString());
            Console.WriteLine("Precision: " + (precision * 100.0).ToString());
            Console.WriteLine("Recall: " + (recall * 100.0).ToString());
        }
    }
}
