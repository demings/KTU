using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSortOP
{
    public class MyDataList : DataList
    {

        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;

        public MyDataList(int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(rand.NextDouble());
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(rand.NextDouble());
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = null;
        }

        public MyLinkedListNode getHeadNode()
        {
            return headNode;
        }

        public void setHeadNode(MyLinkedListNode node)
        {
            headNode = node;
        }

        public override double Head()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }

        public override double Next()
        {
            prevNode = currentNode;
            currentNode = currentNode.nextNode;
            return currentNode.data;
        }

        public override void Swap(double a, double b)
        {
            prevNode.data = a;
            currentNode.data = b;
        }

        public void SelectionSort()
        {
            MyLinkedListNode currentOuter = headNode;

            while (currentOuter != null)
            {
                MyLinkedListNode minimum = currentOuter;
                MyLinkedListNode currentInner = currentOuter.nextNode;

                while (currentInner != null)
                {
                    if (currentInner.data < minimum.data)
                    {
                        minimum = currentInner;
                    }

                    currentInner = currentInner.nextNode;
                }

                if (!Object.ReferenceEquals(minimum, currentOuter))
                {
                    var tmpData = currentOuter.data;
                    currentOuter.data = minimum.data;
                    minimum.data = tmpData;
                }

                currentOuter = currentOuter.nextNode;
            }

        }


        public class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public double data { get; set; }

            public MyLinkedListNode(double data)
            {
                this.data = data;
            }

            public MyLinkedListNode(double data, MyLinkedListNode nextNode)
            {
                this.data = data;
                this.nextNode = nextNode;
            }

            public static MyLinkedListNode Append(MyLinkedListNode xs, MyLinkedListNode ys)
            {
                if (xs == null) return ys;
                else return new MyLinkedListNode(xs.data, Append(xs.nextNode, ys));
            }

            public static MyLinkedListNode Filter(Func<double, bool> p, MyLinkedListNode xs)
            {
                if (xs == null) return null;
                else if (p(xs.data)) return new MyLinkedListNode(xs.data, Filter(p, xs.nextNode));
                else return Filter(p, xs.nextNode);
            }

            public static MyLinkedListNode QSort(MyLinkedListNode xs)
            {
                if (xs == null) return null;
                else
                {
                    double pivot = xs.data;
                    MyLinkedListNode less = QSort(Filter(x => x <= pivot, xs.nextNode));
                    MyLinkedListNode more = QSort(Filter(x => x > pivot, xs.nextNode));
                    return Append(less, new MyLinkedListNode(pivot, more));
                }
            }
        }

    }
}
