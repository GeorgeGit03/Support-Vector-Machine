/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2016-2020, Florin Leon                               *
 *  E-mail:      florin.leon@academic.tuiasi.ro                           *
 *  Website:     http://florinleon.byethost24.com/lab_ia.html             *
 *  Description: Evolutionary Algorithms                                  *
 *               (Artificial Intelligence lab 8)                          *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace EvolutionaryAlgorithm
{
    /// <summary>
    /// The class that represents the selection operation
    /// </summary>
    public class Selection
    {
        private static Random _rand = new Random();

        public static Chromosome Tournament(Chromosome[] population)
        {
            // Randomly choose k members of the population -> in this case, k = 2
            int index1 = _rand.Next(0, population.Length);
            int index2 = _rand.Next(0, population.Length);

            while (index1 == index2)
            {
                index2 = _rand.Next(0, population.Length);
            }

            Chromosome chromosome1 = new Chromosome(population[index1]);
            Chromosome chromosome2 = new Chromosome(population[index2]);

            Chromosome bestChromosome;
            if (chromosome1.Fitness > chromosome2.Fitness)
            {
                bestChromosome = new Chromosome(chromosome1);
            }
            else
            {
                bestChromosome = new Chromosome(chromosome2);
            }
            return bestChromosome;
        }

        public static Chromosome GetBest(Chromosome[] population)
        {
            Chromosome bestChromosome = new Chromosome(population[0]);

            double max = bestChromosome.Fitness;
            for (int i = 1; i < population.Length; ++i)
            {
                if (population[i].Fitness > max)
                {
                    max = population[i].Fitness;
                    bestChromosome = new Chromosome(population[i]);
                }
            }
            return bestChromosome;
        }
    }

    //==================================================================================

    /// <summary>
    /// The class that represents the crossing operation
    /// </summary>
    public class Crossover
    {
        private static Random _rand = new Random();

        public static Chromosome Arithmetic(Chromosome mother, Chromosome father, double rate)
        {
            Chromosome chromosome;

            double r = _rand.NextDouble();

            if (r < rate)
            {
                chromosome = new Chromosome(mother);

                double a = _rand.NextDouble();
                while (a == 0)
                {
                    a = _rand.NextDouble();
                }

                for (int i = 0; i < chromosome.Genes.Length; ++i)
                {
                    chromosome.Genes[i] = a * (mother.Genes[i]) + (1 - a) * (father.Genes[i]);
                }
            }
            else
            {
                r = _rand.NextDouble();
                if (r < 0.5)
                {
                    chromosome = new Chromosome(mother);
                }
                else
                {
                    chromosome = new Chromosome(father);
                }
            }
            return chromosome;
        }
    }

    //==================================================================================

    /// <summary>
    /// The class that represents the mutation operation
    /// </summary>
    public class Mutation
    {
        private static Random _rand = new Random();

        public static void Reset(Chromosome child, double rate)
        {
            for (int i = 0; i < child.Genes.Length; ++i)
            {
                double r = _rand.NextDouble();

                if (r < rate)
                {
                    child.Genes[i] = child.MinValues[i] + r * (child.MaxValues[i] - child.MinValues[i]);
                }
            }
        }
    }

    //==================================================================================

    /// <summary>
    /// The class that implements the evolutionary algorithm for optimization
    /// </summary>
    public class EvolutionaryAlgorithm
    {
        /// <summary>
        /// The optimization method that finds the solution to the problem
        /// </summary>
        public Chromosome Solve(IOptimizationProblem p, int populationSize, int maxGenerations, double crossoverRate, double mutationRate, int[,] data)
        {   
            Chromosome[] population = new Chromosome[populationSize];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = p.MakeChromosome();
                p.ComputeFitness(population[i], data);
            }

            for (int gen = 0; gen < maxGenerations; gen++)
            {
                Chromosome[] newPopulation = new Chromosome[populationSize];
                newPopulation[0] = Selection.GetBest(population); // Elitism

                for (int i = 1; i < populationSize; i++)
                {
                    // Select 2 parents: Selection.Tournament
                    Chromosome p1 = new Chromosome(Selection.Tournament(population));
                    Chromosome p2 = new Chromosome(Selection.Tournament(population));

                    // Generating a child by crossover application: Crossover.Arithmetic
                    Chromosome c = new Chromosome(Crossover.Arithmetic(p1, p2, crossoverRate));

                    // Applying mutation to the child: Mutation.Reset
                    Mutation.Reset(c, mutationRate);

                    // Calculating the value of the fitness function for the child: ComputeFitness from the problem p
                    p.ComputeFitness(c, data);

                    // Introduction of child in newPopulation
                    newPopulation[i] = c;
                }

                for (int i = 0; i < populationSize; i++)
                    population[i] = newPopulation[i];

                Console.Write("{0}% \r", (gen + 1));
            }
            Console.WriteLine();
            Console.WriteLine("Complete processing.");
            return Selection.GetBest(population);
        }
    }

    //==================================================================================

    /// <summary>
    /// The class that represents the problem in the first application: solving the equation
    /// </summary>
    public class Equation : IOptimizationProblem
    {
        public Chromosome MakeChromosome()
        {
            int NoInstances = 100;
            double C = 1e-5;
            // A chromosome has 100 genes(alpha1, alpha2, ... alpha100) that can take values ​​in the range(0, C)
            return new Chromosome(100, Enumerable.Repeat<double>(0.0, NoInstances).ToArray(),
                Enumerable.Repeat<double>(C, NoInstances).ToArray());
        }

        Kernel kernel1 = new Kernel();

        public void ComputeFitness(Chromosome c, int[,] data)
        {
            double sumAlfa = 0;

            int[] v1 = new int[6];
            int[] v2 = new int[6];

            for (int i = 0; i < c.NoGenes; ++i)
            {
                sumAlfa += c.Genes[i];
            }

            double sumAlfaiAlfaj = 0;
            for (int i = 0; i < c.NoGenes; ++i)
            {
                for (int j = 0; j < c.NoGenes; ++j)
                {
                    for (int ind1 = 0; ind1 < 6; ++ind1)
                    {
                        v1[ind1] = data[i, ind1];
                    }

                    for (int ind2 = 0; ind2 < 6; ++ind2)
                    {
                        v2[ind2] = data[j, ind2];
                    }
                    sumAlfaiAlfaj += c.Genes[i] * c.Genes[j] * data[i, 6] * data[j, 6] * kernel1.PolinomialKernel(v1, v2);
                }
            }
            c.Fitness = sumAlfa - 0.5 * sumAlfaiAlfaj;
        }
    }

    //==================================================================================
    public class Kernel
    {
        public int PolinomialKernel(int[] v1, int[] v2)
        {
            int result = 0;
            for (int i = 0; i < v1.Length; ++i)
            {
                result += (v1[i] * v2[i]);
            }
            result += 1;
            result *= result;

            return result;
        }
    }

    /// <summary>
    /// The main program that calls the algorithm
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            int trainInstances = 100;
            double cost = 1e-5;
            double sumLimit = 1000000000e-1000000000;
            ////////// Unacc
            Console.WriteLine("Unacc class processing");
            Kernel kernel = new Kernel();

            int[,] data = new int[trainInstances, 7];
            String line;
            int index = 0;

            try
            {
                // Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\Support_Vector_Machine\\dataSetQuantifiedUnacc.data");
                // Read the first line of text
                line = sr.ReadLine();

                char[] delimiterChars = { ',' };
                int[] vector = new int[7];

                // Continue to read until you reach end of file
                while (line != null)
                {

                    string[] words = line.Split(delimiterChars);

                    for (int i = 0; i < 7; ++i)
                    {
                        vector[i] = Int32.Parse(words[i]);
                    }

                    for (int j = 0; j < 7; ++j)
                    {
                        data[index, j] = vector[j];
                    }
                    index++;
                    // Read the next line
                    line = sr.ReadLine();
                }

                // Close the file
                sr.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("The training data set was read successfully.");
            }

            EvolutionaryAlgorithm ea = new EvolutionaryAlgorithm();



            Console.WriteLine("Alpha coefficients are calculated.");

            Chromosome solution = ea.Solve(new Equation(), 100, 100, 0.95, 0.15, data);

            double[] alfa = new double[trainInstances];
            int zeroCounter = 0;
            for (int i = 0; i < trainInstances; ++i)
            {
                alfa[i] = solution.Genes[i];
            }

            Console.WriteLine();
            Console.Write("Lagrangian multipliers are: alpha = [");
            for (int i = 0; i < trainInstances - 1; ++i)
            {
                Console.Write(alfa[i] + " ");
            }
            Console.WriteLine(alfa[trainInstances - 1] + " ]");
            Console.WriteLine();

            var rand = new Random();

            double s = 0;
            double[] newAlfa = new double[trainInstances];
            for (int i = 0; i < trainInstances; ++i)
            {
                newAlfa[i] = alfa[i];
            }

            for (int i = 0; i < trainInstances; ++i)
            {
                s += alfa[i] * data[i, 6];
            }
            Console.WriteLine("The initial value of the initial weighted sum of the coefficients is: " + s);

            int k;
            double sPlus;
            double sMinus;


            // Repair algorithm
            while (Math.Abs(s) > sumLimit)
            {
                sPlus = 0.0;
                sMinus = 0.0;

                for (int i = 0; i < trainInstances; ++i)
                {
                    if (i < 62)
                    {
                        sPlus += newAlfa[i] * data[i, 6];
                    }
                    else
                    {
                        sMinus += newAlfa[i] * data[i, 6];
                    }
                }
                sMinus = Math.Abs(sMinus);

                if (Math.Abs(sPlus) > Math.Abs(sMinus))
                {
                    k = rand.Next(0, 62);
                }
                else
                {
                    k = rand.Next(62, trainInstances);
                }

                if (newAlfa[k] > Math.Abs(s))
                {
                    newAlfa[k] = newAlfa[k] - Math.Abs(s);
                }
                else
                {
                    newAlfa[k] = 0;
                }

                s = 0;
                for (int i = 0; i < trainInstances; ++i)
                {
                    s += newAlfa[i] * data[i, 6];
                }
            }

            for(int i = 0; i < trainInstances; ++i)
            {
                if (newAlfa[i] == 0)
                {
                    zeroCounter++;
                }
            }
            Console.WriteLine("The number of zeros is: " + zeroCounter);


            // Checking the first condition of the dual problem
            bool flag = false;
            for (int i = 0; i < trainInstances; ++i)
            {
                if (newAlfa[i] < 0 || newAlfa[i] > cost)
                {
                    flag = true;
                }
            }

            if (flag == false)
            {
                Console.WriteLine("All Lagrangian coefficients fall within the limits imposed by the first condition of the dual problem, respectively fall within the range[0, {0}].", 1e-5);
            }

            else if (flag == true)
            {
                Console.WriteLine("Not all Lagrangian coefficients fall within the limits imposed by the first condition of the dual problem, respectively fall within the range[0, {0}].", 1e-5);
            }

            if (flag == false)
            {
                Console.WriteLine("The first condition imposed by the dual problem is fulfilled.");
            }
            else
            {
                Console.WriteLine("The first condition imposed by the dual problem is not fulfilled.");
            }

            // Checking the second condition of the dual problem
            Console.WriteLine();
            Console.WriteLine("The final value of the weighted sum of the coefficients is: " + s);
            if (Math.Abs(s) <= sumLimit)
            {
                Console.WriteLine("The second condition imposed by the dual problem is fulfilled.");
            }
            else
            {
                Console.WriteLine("The second condition imposed by the dual problem is not fulfilled.");
            }

            Console.Write("The Lagrangian multipliers obtained after running the repair algorithm are: alpha = [");
            for (int i = 0; i < trainInstances - 1; ++i)
            {
                Console.Write(newAlfa[i] + " ");
            }
            Console.WriteLine(newAlfa[trainInstances - 1] + " ]");

            // Bias calculation
            double bias = 0;
            double biasSum;

            int[] v1 = new int[6];
            int[] v2 = new int[6];

            for (int i = 0; i < trainInstances; ++i)
            {
                biasSum = 0;
                for (int j = 0; j < trainInstances; ++j)
                {
                    for (int ind1 = 0; ind1 < 6; ++ind1)
                    {
                        v1[ind1] = data[i, ind1];
                    }

                    for (int ind2 = 0; ind2 < 6; ++ind2)
                    { 
                        v2[ind2] = data[j, ind2];
                    }

                    biasSum += data[j, 6] * newAlfa[j] * kernel.PolinomialKernel(v1, v2);
                }
                bias += data[i, 6] - biasSum;
            }
            bias /= trainInstances;
            Console.WriteLine();
            Console.WriteLine("Bias value is: " + bias);
            Console.WriteLine("//////////////////////////////////////////////////   Processing completed for Unacc class   //////////////////////////////////////////////////");

            Console.WriteLine();

            int noInstances = 1728;
            int[,] dataTest = new int[noInstances, 6];
            String lineTest;
            int indexTest = 0;
            try
            {
                // Pass the file path and file name to the StreamReader constructor
                StreamReader srTest = new StreamReader("D:\\Support_Vector_Machine\\dataTestQuantified.data");
                // Read the first line of text
                lineTest = srTest.ReadLine();

                char[] delimiterCharsTest = { ',' };
                int[] vectorTest = new int[6];

                // Continue to read until you reach end of file
                while (lineTest != null)
                {
                    string[] words = lineTest.Split(delimiterCharsTest);

                    for (int i = 0; i < 6; ++i)
                    {
                        vectorTest[i] = Int32.Parse(words[i]);
                    }

                    for (int j = 0; j < 6; ++j)
                    {
                        dataTest[indexTest, j] = vectorTest[j];
                    }
                    indexTest++;
                    // Read the next line
                    lineTest = srTest.ReadLine();
                }

                // Close the file
                srTest.Close();
                // Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("The test dataset was read successfully.");
            }
            
            Console.WriteLine();


            Console.WriteLine();
            Console.WriteLine("Calculation of classification value: ");
            double[] classificationValue = new double[noInstances];

            int[] trainV = new int[6];
            int[] testV = new int[6];

            double val;
            for (int j = 0; j < noInstances; ++j)
            {
                val = 0;
                for (int i = 0; i < trainInstances; ++i)
                {
                    for (int ind1 = 0; ind1 < 6; ++ind1)
                    {
                        trainV[ind1] = data[i, ind1];
                    }

                    for (int ind2 = 0; ind2 < 6; ++ind2)
                    {
                        testV[ind2] = dataTest[j, ind2];
                    }
                    val += newAlfa[i] * data[i, 6] * kernel.PolinomialKernel(trainV, testV);
                }
                classificationValue[j] = val;
            }
            Console.WriteLine();

            string values = "";
            Console.WriteLine("The values in the vector with classification value are: ");
            for (int i = 0; i < noInstances - 1; ++i)
            {
                values += classificationValue[i].ToString();
                values += ",";
            }
            values += classificationValue[noInstances - 1].ToString();

            Console.WriteLine("The string obtained is: ");
            Console.WriteLine(values);

            using (StreamWriter sw = new StreamWriter(@"D:\\Support_Vector_Machine\\classifiedValuesUnacc.data"))
            {
                sw.WriteLine(values);
            }
        }
    }
}