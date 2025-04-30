using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250331
{
    internal class Program
    {
        static long[] dp = new long[51];

        static void Fibo(int a)
        {
            dp[1] = 1;
            dp[2] = 1;
            for (int i = 3; i <= 50; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
        }
        static void Main(string[] args)
        {
            Fibo(50);
            Console.WriteLine(dp[50]);
        }

        
    }
}
