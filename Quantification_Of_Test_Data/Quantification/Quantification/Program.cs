using System;
using System.IO;

namespace Quantification
{
    class Program
    {
        static void Main(string[] args)
        {
            String line;

            string quantified = "";
            try
            {
                // Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\Support_Vector_Machine\\dataTest.data");
                // Read the first line of text
                line = sr.ReadLine();

                char[] delimiterChars = { ',' };
                int[] quantifiedValues = new int[7];
                string quantifiedValuesString = "";
                

                // Continue to read until you reach end of file
                while (line != null)
                {

                    string[] words = line.Split(delimiterChars);

                    switch (words[0])
                    {
                        case "low":
                            quantifiedValues[0] = 1;
                            break;
                        case "med":
                            quantifiedValues[0] = 2;
                            break;
                        case "high":
                            quantifiedValues[0] = 3;
                            break;
                        case "vhigh":
                            quantifiedValues[0] = 4;
                            break;
                    }

                    switch (words[1])
                    {
                        case "low":
                            quantifiedValues[1] = 1;
                            break;
                        case "med":
                            quantifiedValues[1] = 2;
                            break;
                        case "high":
                            quantifiedValues[1] = 3;
                            break;
                        case "vhigh":
                            quantifiedValues[1] = 4;
                            break;
                    }

                    switch (words[2])
                    {
                        case "2":
                            quantifiedValues[2] = 1;
                            break;
                        case "3":
                            quantifiedValues[2] = 2;
                            break;
                        case "4":
                            quantifiedValues[2] = 3;
                            break;
                        case "5more":
                            quantifiedValues[2] = 4;
                            break;
                    }

                    switch (words[3])
                    {
                        case "2":
                            quantifiedValues[3] = 1;
                            break;
                        case "4":
                            quantifiedValues[3] = 2;
                            break;
                        case "more":
                            quantifiedValues[3] = 3;
                            break;
                    }

                    switch (words[4])
                    {
                        case "small":
                            quantifiedValues[4] = 1;
                            break;
                        case "med":
                            quantifiedValues[4] = 2;
                            break;
                        case "big":
                            quantifiedValues[4] = 3;
                            break;
                    }

                    switch (words[5])
                    {
                        case "low":
                            quantifiedValues[5] = 1;
                            break;
                        case "med":
                            quantifiedValues[5] = 2;
                            break;
                        case "big":
                            quantifiedValues[5] = 3;
                            break;
                    }

                    if (words[6] == "unacc")
                    {
                        quantifiedValues[6] = 1;
                    }
                    else if (words[6] == "acc")
                    {
                        quantifiedValues[6] = 2;
                    }
                    else if (words[6] == "good")
                    {
                        quantifiedValues[6] = 3;
                    }
                    else if(words[6] == "vgood")
                    {
                        quantifiedValues[6] = 4;
                    }


                    for (int i = 0; i < 6; ++i)
                    {
                        quantifiedValuesString += quantifiedValues[i].ToString();
                        quantifiedValuesString += ",";
                    }
                    quantifiedValuesString += quantifiedValues[6].ToString();

                    quantified += quantifiedValuesString;
                    quantified += '\n';

                    //Read the next line
                    line = sr.ReadLine();
                    quantifiedValuesString = "";
                }

                using (StreamWriter sw = new StreamWriter(@"D:\\Support_Vector_Machine\\dataTestQuantified.data"))
                {
                    quantified = quantified.Trim();
                    sw.Write(quantified);
                }

                //close the file
                sr.Close();
                //Console.ReadLine();
                
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Writing to the dataTestQuantified.data file was completed successfully.");
            }
        }
    }
}