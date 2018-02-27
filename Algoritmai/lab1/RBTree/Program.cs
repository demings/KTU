using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBTree
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            List<string> nameList = new List<string>();
            RedBlackTree<string, string> strTree = new RedBlackTree<string, string>();
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            random = new Random(seed);
            int n = 15;

            for (int i = 1; i <= n; i++)
            {
                string s = RandomName(10);
                nameList.Add(s);
                strTree.RBInsert(s, s);
            }
            nameList.Add(RandomName(10));
            Console.WriteLine(" Binary Search Tree \n");
            strTree.Print();
            Console.WriteLine("\n Search Test \n");
            foreach (var s in nameList) 
            {
                //if(strTree.TreeSearch(s) == null)
                //{
                //    Console.Write(false + " ");
                //}
                //else
                //{
                //    Console.Write(true + " ");
                //}
                Console.Write(strTree.Contains(s).ToString() + " ");

            }
            Console.WriteLine("\n");
        }

        static string RandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
            random.NextDouble() + 65)));
            builder.Append(ch);
            for (int i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
                random.NextDouble() + 97)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
