using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int l = 2*i + 1; // left = 2*i + 1 
            int r = 2*i + 2; // right = 2*i + 2 
  
            // If left child is larger than root 
            if (l < n && arr[l] > arr[largest]) 
                largest = l; 
  
            // If right child is larger than largest so far 
            if (r < n && arr[r] > arr[largest]) 
                largest = r; 
  
            // If largest is not root 
            if (largest != i) 
            {
                arr.Swap(i, largest, arr[i],arr[largest]);
  
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
            for (int i=n-1; i>=0; i--) 
            { 
                // Move current root to end 

                items.Swap(0, i, items[0], items[i]);
  
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
        
    }
    }
}
abstract class DataArray
{
    protected int length;
    public int Length { get { return length; } }
    public abstract double this[int index] { get; }
    public abstract void Swap(int i, int j, double a, double b);
    public void Print(int n)
    {
        for (int i = 0; i < n; i++)
            Console.Write(" {0:F5} ", this[i]);
        Console.WriteLine();
    }
}


