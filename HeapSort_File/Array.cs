using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort_File
{
    class MyFileArray : DataArray
    {
        public MyFileArray(string filename, int n, int seed)
        {
            Data[] data = new Data[n];
            length = n;
            for (int i = 0; i < length; i++)
            {
                data[i] = new Data(seed*i);
            }

            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
                    FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                        writer.Write(data[j].ToString());
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public FileStream fs { get; set; }

        public override Data this[int index]
        {
            get
            {
                Byte[] data = new Byte[8];
                fs.Seek(8 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 8);

                var result = Encoding.UTF8.GetString(data);
                var temp = new Data(result.ToCharArray());
                return temp;
            }
        }

        public override Byte[] TakeFromFile(int index)
        {
            Byte[] data = new Byte[8];
            fs.Seek(8 * index, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            return data;
        }


        public override void Swap(int i, int j, Data a, Data b)
        {
            Byte[] data1 = new Byte[8];
            Byte[] data2 = new Byte[8];
            Data.GetEncoding(a.myData).CopyTo(data1, 0);
            Data.GetEncoding(b.myData).CopyTo(data2, 0);

            fs.Seek(8 * (j), SeekOrigin.Begin);
            fs.Write(data1, 0, 8);
            fs.Seek(8 * (i), SeekOrigin.Begin);
            fs.Write(data2, 0, 8);
        }
    }
}
