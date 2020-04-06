using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort_File
{
    class Data
    {
        public char[] myData { get; set; }

        public Data(int seed)
        {
            myData = new char[6];
            Generator(seed);
        }

        public Data(Char[] array)
        {
            myData = array;
        }

        public void Generator(int seed)
        {
            
            char random;

            Random rnd = new Random(seed);
            for (int i = 0; i < 3; i++)
            {
                random = (char)rnd.Next('A', '[');
                myData[i] = random;
            }

            var number = rnd.Next(100, 999).ToString();
            for (int i = 0, k=3; i < 3; i++, k++)
            {
                myData[k] = number[i];
            }

        }

        public static Byte[] GetBytes(char[] data)
        {
            Encoding ascii = Encoding.ASCII;
            var bytes = new byte[6];
            var k = data;
            bytes = ascii.GetBytes(k);
            return bytes;
        }

        public static bool operator >(Data first, Data second)
        {
            var a = GetBytes(first.myData);
            var b = GetBytes(second.myData);
            if (a[0] > b[0])
                return true;
            else if (a[1] > b[1] && a[0] == b[0])
                return true;
            else if (a[2] > b[2] && a[1] == b[1] && a[0] == b[0])
                return true;
            else if (a[2] == b[2] && a[1] == b[1] && a[0] == b[0])
            {
                string s1 = "";
                string s2 = "";
                s1 = s1 + first.myData[3] + first.myData[4] + first.myData[5];
                s2 = s2 + second.myData[3] + second.myData[4] + second.myData[5];
                var n1 = int.Parse(s1);
                var n2 = int.Parse(s2);
                if (n1 > n2)
                    return true;
            }
            return false;
        }

        public static bool operator <(Data first, Data second)
        {
            var a = GetBytes(first.myData);
            var b = GetBytes(second.myData);
            if (a[0] < b[0])
                return true;
            else if (a[1] < b[1] && a[0] == b[0])
                return true;
            else if (a[2] < b[2] && a[1] == b[1] && a[0] == b[0])
                return true;
            else if (a[2] == b[2] && a[1] == b[1] && a[0] == b[0])
            {
                string s1 = "";
                string s2 = "";
                s1 = s1 + first.myData[3] + first.myData[4] + first.myData[5];
                s2 = s2 + second.myData[3] + second.myData[4] + second.myData[5];
                var n1 = int.Parse(s1);
                var n2 = int.Parse(s2);
                if (n1 < n2)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            string s = "";
            foreach (var c in myData)
            {
                s = s + c;
            }

            return s;
        }
    }
}
