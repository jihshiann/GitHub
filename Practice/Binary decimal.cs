using System;


namespace ConsoleApp1
{
    class Solution
    {
        public int[] solution(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int x = 0;
            int y = 0;
            int[] Ans = new int[] { };
            for (int i = 0; i < A.Length; i++)
            {

                if (A[i] != 0)
                {
                    int temp = 1;
                    for (int j = 0; j < i; j++)
                    {
                        temp = temp * -2;
                    }
                    x += temp;
                }
            }
            if (y % 2 != 0)
            {
                y += 1;
            }
            y = x / 2;
            if (y < 0)
            {
                y = y * -1;
            }
            while (y > 1)
            {
                int i = 0;
                Ans[i] = y % 2;
                y = y / 2;
            }
            return Ans;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 1, 0, 0, 1, 1 };
            int[] B = new int[] { 1, 0, 0, 1, 1, 1 };
            int[] C = new int[] { 0, 0, 1, 0, 1, 1 };
            int[] D = new int[] { 1, 0, 0, 1, 1 };
            int[] E = new int[] { 0, 0, };

            Solution solution = new Solution();
            Console.WriteLine(solution.solution(A));
            Console.WriteLine(solution.solution(B));
            Console.WriteLine(solution.solution(C));
            Console.WriteLine(solution.solution(D));
            Console.WriteLine(solution.solution(E));
            Console.ReadLine();
        }
    }
}
