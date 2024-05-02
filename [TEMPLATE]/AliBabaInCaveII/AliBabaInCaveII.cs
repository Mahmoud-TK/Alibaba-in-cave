using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class AliBabaInCaveII
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given the Camels possible load and N items, each with its weight, profit and number of instances, 
        /// Calculate the max total profit that can be carried within the given camels' load
        /// </summary>
        /// <param name="camelsLoad">max load that can be carried by camels</param>
        /// <param name="itemsCount">number of items</param>
        /// <param name="weights">weight of each item [ONE-BASED array]</param>
        /// <param name="profits">profit of each item [ONE-BASED array]</param>
        /// <param name="instances">number of instances for each item [ONE-BASED array]</param>
        /// <returns>Max total profit</returns>
        /// 
        static int[,] dp; 
        static public int SolveValue(int camelsLoad, int itemsCount, int[] weights, int[] profits, int[] instances)
        {
            dp = new int[camelsLoad + 1, itemsCount + 1];
            for (int j = 0; j < itemsCount + 1; j++)
            {
                for (int i = 0; i < camelsLoad + 1; i++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else
                    {
                        int solve = 0;
                        dp[i, j] = dp[i, j - 1];
                        for (int z = 1; z <= instances[j]; z++)
                        {
                            if (weights[j] * z > i)
                            {
                                break;
                            }
                             solve = dp[i - (weights[j] * z), j - 1] + profits[j] * z;
                           
                        }
                        dp[i, j] = Math.Max(solve, dp[i, j]);
                    }
                }
            }
            return dp[camelsLoad, itemsCount];
        }

        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>Tuple array of the selected items to get MAX profit (stored in Tuple.Item1) together with the number of instances taken from each item (stored in Tuple.Item2)
        /// OR NULL if no items can be selected</returns>
        static public Tuple<int, int>[] ConstructSolution(int camelsLoad, int itemsCount, int[] weights, int[] profits, int[] instances)
        {
            
            int w = camelsLoad, n = itemsCount;
            List<Tuple<int, int>> tuple = new List<Tuple<int, int>>();
            while (!(n == 0 || w == 0))
            {
                if (dp[w, n] <= dp[w, n - 1])
                {
                    n--;
                    continue;
                }
                int numOfInstance = 0;
                for (int z = 1; z <= instances[n]; z++)
                {
                    if (weights[n] * z > w)
                    {
                        break;
                    }
                    int solve = dp[w - (weights[n] * z), n - 1] + profits[n] * z;
                    if (solve == dp[w, n])
                    {
                        numOfInstance = z;
                    }
                }
                tuple.Add(Tuple.Create(n, numOfInstance));
                w -= weights[n] * numOfInstance;
                n--;
            }
            tuple.Reverse();
            Tuple<int, int>[] finalSol;
            finalSol = tuple.ToArray();
            return finalSol;
        }
        #endregion

        #endregion
    }
}
