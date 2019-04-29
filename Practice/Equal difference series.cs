using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Method2
{
    class Program
    {
        public class numStream
        {
            public int Des(List<int> A)
            {
                int count = 0;

                int des = 0;

                for (int i = 0; i < A.Count() - 2; i++)
                {
                    des = A[i + 1] - A[i];

                    for (int j = i + 1; j < A.Count()-1; j++)
                    {
                        if (A[j + 1] - A[j] == des)
                        {
                            count++;
                            A.GetRange(i,j+2-i).ForEach(num=> Console.Write(num));
                            Console.WriteLine();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return count;
            }
        }

        static void Main(string[] args)
        {
            List<int> A =new List<int>() { 1,2,3,4,3,2,1,0,1,2,3,9,9,9};
            
            Console.WriteLine(new numStream().Des(A));
            
            Console.Read();
        }
    }
}
