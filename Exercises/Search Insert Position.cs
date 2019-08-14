using System;

namespace Search_Insert_Position
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            int[] nums = { 1, 3, 5, 6 };
            int target = 0;
            Console.WriteLine(solution.SearchInsert(nums,target));
            Console.ReadLine();
        }

        public class Solution
        {
            public int SearchInsert(int[] nums, int target)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] >= target)
                    {
                        return i;
                    }
                }
                return nums.Length;
            }
        }
    }
}
