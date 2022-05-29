using System;
using System.IO;

namespace Clasificator
{
    class Program
    {
        static void Main(string[] args)
        {
            String line;
            int noInstances = 1728;
            double[] classifiedValues1 = new double[noInstances];
            double[] classifiedValues2 = new double[noInstances];
            double[] classifiedValues3 = new double[noInstances];
            double[] classifiedValues4 = new double[noInstances];
            

            ////////////// Unacc
            try
            {
                // Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\Support_Vector_Machine\\classifiedValuesUnacc.data");
                // Read the first line of text
                line = sr.ReadLine();

                char[] delimiterChars = { ',' };
                
                string[] words = line.Split(delimiterChars);
                

                for(int i = 0; i < noInstances; ++i)
                {
                    classifiedValues1[i] = Double.Parse(words[i]);
                }

                // Close the file
                sr.Close();
                //Console.ReadLine();

            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Reading from the classifiedValuesUnacc.data file was successful.");
            }



            //////////////////////// Acc
            try
            {
                // Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\Support_Vector_Machine\\classifiedValuesAcc.data");
                // Read the first line of text
                line = sr.ReadLine();

                char[] delimiterChars = { ',' };

                string[] words = line.Split(delimiterChars);


                for (int i = 0; i < noInstances; ++i)
                {
                    classifiedValues2[i] = Double.Parse(words[i]);
                }

                // Close the file
                sr.Close();
                // Console.ReadLine();

            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Reading from the classifiedValuesAcc.data file was successful.");
            }



            //////////////////////// Good
            try
            {
                // Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\Support_Vector_Machine\\classifiedValuesGood.data");
                // Read the first line of text
                line = sr.ReadLine();

                char[] delimiterChars = { ',' };

                string[] words = line.Split(delimiterChars);


                for (int i = 0; i < noInstances; ++i)
                {
                    classifiedValues3[i] = Double.Parse(words[i]);
                }

                // Close the file
                sr.Close();
                // Console.ReadLine();

            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Reading from the classifiedValuesGood.data file was successful.");
            }

            //////////////////////// Vgood
            try
            {
                // Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\Support_Vector_Machine\\classifiedValuesVgood.data");
                // Read the first line of text
                line = sr.ReadLine();

                char[] delimiterChars = { ',' };

                string[] words = line.Split(delimiterChars);


                for (int i = 0; i < noInstances; ++i)
                {
                    classifiedValues4[i] = Double.Parse(words[i]);
                }

                // Close the file
                sr.Close();
                // Console.ReadLine();

            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Reading from the classifiedValuesVgood.data file was successful.");
            }

            int countUnacc = 0;
            int countAcc = 0;
            int countGood = 0;
            int countVgood = 0;

            int[] classType = new int[noInstances];
            for (int i = 0; i < noInstances; ++i)
            {
                if (classifiedValues1[i] >= classifiedValues2[i] && classifiedValues1[i] >= classifiedValues3[i] && classifiedValues1[i] >= classifiedValues4[i])
                {
                    classType[i] = 1;
                    countUnacc++;
                }

                else if (classifiedValues2[i] >= classifiedValues1[i] && classifiedValues2[i] >= classifiedValues3[i] && classifiedValues2[i] >= classifiedValues4[i])
                {
                    classType[i] = 2;
                    countAcc++;
                }

                else if (classifiedValues3[i] >= classifiedValues1[i] && classifiedValues3[i] >= classifiedValues2[i] && classifiedValues3[i] >= classifiedValues4[i])
                {
                    classType[i] = 3;
                    countGood++;
                }

                else if (classifiedValues4[i] >= classifiedValues1[i] && classifiedValues4[i] >= classifiedValues2[i] && classifiedValues4[i] >= classifiedValues3[i])
                {
                    classType[i] = 4;
                    countVgood++;
                }
            }

            /*
            Console.WriteLine("The final results of the classification are: ");
            for(int i = 0; i < noInstances; ++i)
            {
                Console.WriteLine("The car with index: " + i + " from the test set is in classType:" + classType[i]);
            }
            */

            double procUnacc;
            double procAcc;
            double procGood;
            double procVgood;

            procUnacc = (double)((double)(countUnacc * 100) / (double)noInstances);
            procAcc = (double)((double)(countAcc * 100) / (double)noInstances);
            procGood = (double)((double)(countGood * 100) / (double)noInstances);
            procVgood = (double)((double)(countVgood * 100) / (double)noInstances);

            Console.WriteLine("The percentage of Unacc cars is: " + procUnacc + "%.");
            Console.WriteLine("The percentage of Acc cars is: " + procAcc + "%.");
            Console.WriteLine("The percentage of Good cars is: " + procGood + "%.");
            Console.WriteLine("The percentage of Vgood cars is: " + procVgood + "%.");
        }
    }
}
