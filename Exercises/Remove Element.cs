using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remove_Element
{
    public class Solution
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            int[] nums = { 0, 1, 2, 2, 3, 0, 4, 2 };
            int val = 2;
            Console.WriteLine(solution.RemoveElement(nums,val));
        }

        public int RemoveElement(int[] nums, int val)
        {
            int count = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != val)
                {
                    nums[count] = nums[i];
                    count++;
                }
            }
            Array.Resize(ref nums, count);

            return count;
        }
    }
}
