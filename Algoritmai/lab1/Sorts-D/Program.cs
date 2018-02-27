using System;
using System.IO;
using System.Diagnostics;


namespace Sorts_D
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            // Antras etapas
            Test_File_Array_List(seed);
        }


        public static void SelectionSort(MyFileArray items)
        {
            for (int j = 0; j < items.Length - 1; j++)
            {
                int iMin = j;
                for (int i = j + 1; i < items.Length; i++)
                {
                    if (items[i] < items[iMin])
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

        public static void QuickSort(MyFileArray arr, int left, int right)
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

        public static void Test_File_Array_List(int seed)
        {
            int n = 12;
            string filename;

            Console.WriteLine("\n SELECTION SORT ");
            filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open,
                FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                myfilearray.Print(n);
                SelectionSort(myfilearray);
                myfilearray.Print(n);
            }
            filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open,
                FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                myfilelist.SelectionSort();
                myfilelist.Print(n);
            }

            Console.WriteLine("\n\n QUICKSORT ");

            filename = @"mydataarray.dat";
            myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open,
                FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                myfilearray.Print(n);
                QuickSort(myfilearray, 0, myfilearray.Length - 1);
                myfilearray.Print(n);
            }
            filename = @"mydatalist.dat";
            myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open,
                FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                myfilelist.QuickSort(0, myfilelist.Length - 1);
                myfilelist.Print(n);
            }
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

    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(double a, double b);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for
           (int i = 1; i < n; i++)
                Console.Write(" {0:F5} ", Next());
            Console.WriteLine();

        }

    }

}
