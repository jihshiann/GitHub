using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement_strStr
{
    public class Solution
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            string haystack = "aaaaa";
            string needle = "bba";
            Console.WriteLine(solution.StrStr(haystack, needle));
            Console.ReadLine();
        }

        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle))
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(haystack))
            {
                return -1;
            }

            List<char> haystackArray = haystack.ToArray().ToList();
            List<char> needleArray = needle.ToArray().ToList();
            int index = 0;
            for (int i = 0; i < haystackArray.Count - needleArray.Count + 1; i++)
            {
                int matchCount = 0;
                for (int j = 0; j < needleArray.Count; j++)
                {
                    if (haystackArray[i + j] == needleArray[j])
                    {
                        matchCount++;
                        if (matchCount == needleArray.Count)
                        {
                            index = i;
                            return index;
                        }
                    }
                }
            }

            return -1;
        }
    }
}
