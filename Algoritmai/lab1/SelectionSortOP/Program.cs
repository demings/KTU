using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSortOP
{
    class Bubble_Sort
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            // Pirmas etapas
            Test_Array_List(seed);

        }
        public static void SelectionSort(MyDataArray items)
        {
            for(int j = 0; j < items.Length - 1; j++)
            {
                int iMin = j;
                for (int i = j + 1; i < items.Length; i++)
                {
                    if(items[i] < items[iMin])
                    {
                        iMin = i;
                    }
                }

                if (iMin != j)
                {
                    items.Swap(j, iMin);
                }
            }
        }

        public static void QuickSort(MyDataArray arr, int left, int right)
        {
            int i = left, j = right;
            var pivot = arr[(left + right) / 2];

            /* partition */
            while (i <= j)
            {
                while (arr[i] < pivot)
                    i++;
                while (arr[j] > pivot)
                    j--;
                if (i <= j)
                {
                    arr.Swap(i, j);
                    i++;
                    j--;
                }
            }

            /* recursion */
            if (left < j)
                QuickSort(arr, left, j);
            if (i < right)
                QuickSort(arr, i, right);
        }

        public static void Test_Array_List(int seed)
        {
            Console.WriteLine("\n Selection sort ");
            int n = 12;
            MyDataArray myarray = new MyDataArray(n, seed);
            Console.WriteLine("\n ARRAY \n");
            myarray.Print(n);
            SelectionSort(myarray);
            myarray.Print(n);
            MyDataList mylist = new MyDataList(n, seed);
            Console.WriteLine("\n LIST \n");
            mylist.Print(n);
            mylist.SelectionSort();
            mylist.Print(n);


            Console.WriteLine("\n\n\n Quick sort ");
            myarray = new MyDataArray(n, seed);
            Console.WriteLine("\n ARRAY \n");
            myarray.Print(n);
            QuickSort(myarray, 0, myarray.Length - 1);
            myarray.Print(n);
            mylist = new MyDataList(n, seed);
            Console.WriteLine("\n LIST \n");
            mylist.Print(n);
            mylist.setHeadNode(MyDataList.MyLinkedListNode.QSort(mylist.getHeadNode()));
            mylist.Print(n);
            
        }
    }

    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double this[int index] { get; }
        public abstract void Swap(int j, double a, double b);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(" {0:F5} ", this[i]);
            Console.WriteLine();
        }
    }
    abstract public class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(double a, double b);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0:F5} ", Next());
            Console.WriteLine();
        }
    }
}
