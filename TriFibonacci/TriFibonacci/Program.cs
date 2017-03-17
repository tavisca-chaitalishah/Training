using System;
using System.Collections.Generic;
using System.Text;

namespace Codejam
{
    class TriFibonacci
    {
        public int Complete(int[] test)
        {
            int value = 0;
            for (int iterator = 0; iterator < test.Length; iterator++)
            {
                if (iterator < 3)
                {
                    if (test[iterator] == -1)
                    {
                        if (iterator == 0)
                        {
                            value = test[iterator + 3] - (test[iterator + 2] + test[iterator + 1]);
                            test[iterator] = value;
                            if (value <= 0)
                                value = -1;
                        }
                        else if (iterator == 1)
                        {
                            value = test[iterator + 2] - (test[iterator + 1] + test[iterator - 1]);
                            test[iterator] = value;
                            if (value <= 0)
                                value = -1;
                           
                        }
                        else
                        {
                            value = test[iterator + 1] - (test[iterator - 1] + test[iterator - 2]);
                            test[iterator] = value;
                            if (value <= 0)
                                value = -1;
                        }
                    }
                }
                else if (iterator >= 3)
                {
                    int trifibonacciSeriesValue = test[iterator - 1] + test[iterator - 2] + test[iterator - 3];

                    if (test[iterator] == -1)
                    {
                        test[iterator] = trifibonacciSeriesValue;
                        value = test[iterator];
                    }
                    else if (test[iterator] != trifibonacciSeriesValue)
                    {
                        return -1;
                    }
                }
            }
            return value;
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            TriFibonacci triFibonacci = new TriFibonacci();
            do
            {
                string[] values = input.Split(',');
                int[] numbers = Array.ConvertAll<string, int>(values, delegate (string s) { return Int32.Parse(s); });
                int result = triFibonacci.Complete(numbers);
                Console.WriteLine(result);
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}