using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoCore.Helpers
{
    public static class RandomHelper
    {
        private static object _syncObj = new();


        /// <summary>
        /// Generate two-dimensional array with unique values beatween minumum and maximum possible values with fixed size.
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        /// <param name="minVal">Minimal random value</param>
        /// <param name="maxVal">Maximal random value</param>
        /// <returns>Returns two-dimensional array </returns>
        public static int[,] GenerateRandomUniqueArray(int rows, int columns, int minVal, int maxVal)
        {
            int[,] resultArray = new int[rows, columns];
            ConcurrentBag<int> generatedItems = new ConcurrentBag<int>();
            Parallel.For(0, columns, (i) => {
                Parallel.For(0, rows, (j) => {
                    lock (_syncObj)
                    {
                        resultArray[i, j] = GenerateNextWithSkip(minVal, maxVal, generatedItems);
                            generatedItems.Add(resultArray[i, j]);
                    }
                });
            });

            return resultArray;
        }

        /// <summary>
        /// Generates random value excluding <paramref name="skippedValues"/>
        /// </summary>
        /// <param name="rand"><see cref="System.Random"/></param>
        /// <param name="minVal">Minimal random value </param>
        /// <param name="maxVal">Maximal random value</param>
        /// <param name="skippedValues"> int array with values which need to skip from result</param>
        /// <returns>a random value beatween minimum and maximum value</returns>
        public static int GenerateNextWithSkip(int minVal, int maxVal, ConcurrentBag<int> skippedValues)
        {
            int result;
            bool needContinue;
            do
            {
                lock (_syncObj)
                {
                    //random didn`t include max value for int then we doing increment here
                    result = Random.Shared.Next(minVal, maxVal+1);
                    needContinue = skippedValues.Contains(result) && skippedValues.Count< int.MaxValue;
                }
            } while (needContinue);

            return result;
        }
    }
}