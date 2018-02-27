using System;
using System.IO;


namespace Sorts_D
{
    class MyFileList : DataList
    {
        int prevNode;
        int currentNode;
        int nextNode;
        public MyFileList(string filename, int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write(rand.NextDouble());
                        writer.Write((j + 1) * 12 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }
        public override double Head()
        {
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            return result;
        }

        public override double Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            prevNode = currentNode;
            currentNode = nextNode;
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            return result;

        }

        public override void Swap(double a, double b)
        {
            Byte[] data;
            fs.Seek(prevNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(a);
            fs.Write(data, 0, 8);
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(b);
            fs.Write(data, 0, 8);
        }

        public void SelectionSort()
        {
            Head();
            
            for (int i = 0; i < length; i++)
            {
                int innerNode = currentNode;
                int minNode = innerNode;

                for (int j = i; j < length; j++)
                {
                    int outerNode = currentNode;
                    if (data(minNode) > data(outerNode))
                    {
                        minNode = outerNode;
                    }
                    Next();
                }

                if(innerNode != minNode)
                {
                    Swap(innerNode, minNode);
                }

                Head();
                for(int u = 0; u <= i; u++)
                {
                    Next();
                }
            }
        }

        double data(int node)
        {
            Byte[] data = new Byte[8];
            fs.Seek(node, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            double result = BitConverter.ToDouble(data, 0);
            return result;
        }

        public void Swap(int aNode, int bNode)
        {
            Byte[] Idata = new Byte[8];
            Byte[] Jdata = new Byte[8];

            BitConverter.GetBytes(data(aNode)).CopyTo(Idata, 0);
            BitConverter.GetBytes(data(bNode)).CopyTo(Jdata, 0);

            fs.Seek(aNode, SeekOrigin.Begin);
            fs.Write(Jdata, 0, 8);

            fs.Seek(bNode, SeekOrigin.Begin);
            fs.Write(Idata, 0, 8);
        }

        //public int Append(int xs, int ys)
        //{
        //    return new int(data(xs), Append(nextNode, ys));
        //}

        //public int Filter(Func<double, bool> p, int xs)
        //{
        //    if (p(data(xs))) return new int(data(xs), Filter(p, nextNode));
        //    else return Filter(p, nextNode);
        //}

        //public int QSort(int xs)
        //{
        //    double pivot = data(xs);
        //    int less = QSort(Filter(x => x <= pivot, nextNode));
        //    int more = QSort(Filter(x => x > pivot, nextNode));
        //    return Append(less, new int(pivot, more));
        //}

        //int newInt(double data, )
        //{
        //    return 0;
        //}

        public void QuickSort(int left, int right)
        {
            Head();
            int i = left, j = right;
            double pivot = data(indexToNode((left + right) / 2));

            /* partition */
            while (i <= j)
            {
                while (data(indexToNode(i)) < pivot)
                    i++;
                while (data(indexToNode(j)) > pivot)
                    j--;
                if (i <= j)
                {
                    Swap(indexToNode(i), indexToNode(j));
                    i++;
                    j--;
                }
            }

            /* recursion */
            if (left < j)
                QuickSort(left, j);
            if (i < right)
                QuickSort(i, right);
        }

        int indexToNode(int index)
        {
            return 4 + index * 12;
        }
    }

}
