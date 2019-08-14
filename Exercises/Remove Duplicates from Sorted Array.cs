using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remove_Duplicates_from_Sorted_Array
{
    public class Solution
    {
        static void Main(string[] args)
        {
            int[] nums = { 0, 0, 1, 1, 1, 1, 2, 2, 3, 3, 4 };
            Solution solution = new Solution();
            Console.WriteLine(solution.RemoveDuplicates(nums));
            Console.ReadLine();
        }

        public int RemoveDuplicates(int[] nums)
        {
            int count = 0;
            int? duplicate = null;
            for (int i = 0; i < nums.Length; i++)
            {
                if (duplicate != nums[i])
                {
                    duplicate = nums[count] = nums[i];
                    count++;
                }
            }
            Array.Resize(ref nums, count);
            return count;
        }
    }
}
