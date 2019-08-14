using System;

namespace Valid_Parentheses
{
    public class Solution
    {
        static void Main(string[] args)
        {
            string s = "([)]";
            Solution solution = new Solution();
            Console.WriteLine(solution.IsValid(s));
        }
        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            else
            {
               
                char[] store = new char[] { 'f' };

                if (s.Length % 2 == 1 &&
                    (s[0] != '{' || s[0] != '[' || s[0] != '('))
                    return false;

                for (int i = 0; i < s.Length; i++)
                {
                    switch (store[store.Length - 1])
                    {
                        case '{':
                            if (s[i] == '}')
                                Array.Resize(ref store, store.Length-1);
                            break;
                        case '[':
                            if (s[i] == ']')
                                Array.Resize(ref store, store.Length-1);
                            break;
                        case '(':
                            if (s[i] == ')')
                                Array.Resize(ref store, store.Length-1);
                            break;
                        default:
                            break;
                    }
                    if (s[i] == '{' || s[i] == '[' || s[i] == '(')
                    {
                        Array.Resize(ref store, store.Length + 1);
                        store[store.Length - 1] = s[i];
                    }
                }

                if (store.Length > 1)
                    return false;
                else
                    return true;
            }
        }
    }
}
