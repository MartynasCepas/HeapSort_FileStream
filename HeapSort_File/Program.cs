using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeapSort_File;

namespace HeapSort_File
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            // Antras etapas
            Test_File_Array_List(seed);
        }


        public static void Heapify(DataArray arr, int n, int i)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            // If left child is larger than root 
            if (l < n && arr[l] > arr[largest])
                largest = l;

            // If right child is larger than largest so far 
            if (r < n && arr[r] > arr[largest])
                largest = r;

            // If largest is not root 
            if (largest != i)
            {
                arr.Swap(i, largest, arr[i], arr[largest]);

                // Recursively heapify the affected sub-tree 
                Heapify(arr, n, largest);
            }
        }

        public static void Heapify(DataList arr, int n, int i) 
        { 
            int largest = i; // Initialize largest as root 
            int l = 2*i + 1; // left = 2*i + 1 
            int r = 2*i + 2; // right = 2*i + 2 
  
            // If left child is larger than root 
            if (l < n &&  (arr.IndexAt(1) != 0) && arr.IndexAt(l)  > arr.IndexAt(largest)) 
                largest = l; 
  
            // If right child is larger than largest so far 
            if (r < n && (arr.IndexAt(1) != 0) && arr.IndexAt(r) > arr.IndexAt(largest)) 
                largest = r; 
  
            // If largest is not root 
            if (largest != i) 
            {
                arr.Swap(i, largest);
  
                // Recursively heapify the affected sub-tree 
                Heapify(arr, n, largest); 
            } 
        }

        public static void HeapSort(DataArray items)
        {
            int n = items.Length;

            // Build heap (rearrange array) 
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(items, n, i);

            // One by one extract an element from heap 
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to end 

                items.Swap(0, i, items[0], items[i]);

                // call max heapify on the reduced heap 
                Heapify(items, i, 0);
            }
        }

        public static void HeapSort(DataList items)
        {
            int n = items.Length;
            
            for (int i = n / 2 - 1; i >= 0; i--) 
                Heapify(items, n, i); 

            for (int i=n-1; i>=0; i--) 
            { 
                // Move current root to end 
                items.Swap(0, i);
  
                // call max heapify on the reduced heap 
                Heapify(items, i, 0); 
            }
        }

        public static void Test_File_Array_List(int seed)
        {
            int n = 12;
            string filename;
            filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open,
           FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                myfilearray.Print(n);
                HeapSort(myfilearray);
                myfilearray.Print(n);
            }
            filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open,
                FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                HeapSort(myfilelist);
                myfilelist.Print(n);
            }

        }
    }
}
abstract class DataArray
{
    protected int length;
    public int Length { get { return length; } }
    public abstract Data this[int index] { get; }
    public abstract void Swap(int i, int j, Data a, Data b);
    public abstract Byte[] TakeFromFile(int index);
    public void Print(int n)
    {
        for (int i = 0; i < n; i++)
        {
            var k = this[i];
            Console.Write(" {0:F5} ", k.ToString());
        }
        Console.WriteLine();
    }
}

abstract class DataList
{
    protected int length;
    public int Length { get { return length; } }
    public abstract double Head();
    public abstract double Next();
    public abstract void Swap(int a, int b);
    public abstract double IndexAt(int index);
    public void Print(int n)
    {
        Console.Write(" {0:F5} ", Head());
        for (int i = 1; i < n; i++)
            Console.Write(" {0:F5} ", Next());
        Console.WriteLine();
    }
}



