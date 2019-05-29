using System;


namespace ConsoleApp1
{
    class Solution
    {
        //This is a demo task.
        //Write a function:
        //class Solution { public int solution(int[] A); }
        //that, given an array A of N integers, returns the smallest positive integer(greater than 0) that does not occur in A.
        //For example, given A = [1, 3, 6, 4, 1, 2], the function should return 5.
        //Given A = [1, 2, 3], the function should return 4.
        //Given A = [−1, −3], the function should return 1.
        //Write an efficient algorithm for the following assumptions:
        //N is an integer within the range[1..100, 000];
        //each element of array A is an integer within the range[−1, 000, 000..1, 000, 000].
        public int solution(int[] A)
        {
            Array.Sort(A);
            //Array.Reverse(A);

            int min = A[0];
            int max = A[A.Length - 1];
            int ans = 0;

            if (Array.IndexOf(A, 1) < 0)
            {
                ans = 1;
                return ans;
            }
            else
            {
                for (int i = 1; i < A.Length; i++)
                {
                    if (min>0 && min - A[i] < -1)
                    {
                        ans = min + 1;
                        return ans; 
                    }
                    min = A[i];
                }
                return ans = max + 1;
            }
        }
    }
        class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 1, 3, 6, 4, 1, 2 };
            int[] B = new int[] { 1, 2, 3 };
            int[] C = new int[] { -1, -3};

            Solution solution = new Solution();
            Console.WriteLine(solution.solution(A));
            Console.WriteLine(solution.solution(B));
            Console.WriteLine(solution.solution(C));
            Console.ReadLine();
        }
    }
}
