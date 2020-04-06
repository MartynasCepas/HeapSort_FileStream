using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort_File
{
    class MyFileList : DataList
    {
        Data prevNode;
        Data currentNode;
        Data nextNode;
        private int prevNodeIndex;
        private int currentNodeIndex;
        private int nextNodeIndex;
        public MyFileList(string filename, int n, int seed)
        {
            length = n;
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
                    FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        var k = new Data(seed * j);
                        writer.Write(k.ToString());
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
        public override Data Head()
        {
            prevNode = null;
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNodeIndex = BitConverter.ToInt32(data, 0);

            var res = Encoding.UTF8.GetString(data);
            var temp = new Data(res.ToCharArray());

            currentNode = temp;
            prevNodeIndex = -1;
            fs.Seek(currentNodeIndex, SeekOrigin.Begin);
            fs.Read(data, 0, 12);

            res = Encoding.UTF8.GetString(data,0,8);

            nextNodeIndex= BitConverter.ToInt32(data, 8);
            temp = new Data(res.ToCharArray());

            nextNode = temp;
            return temp;
        }
        public override Data Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(nextNodeIndex, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            prevNode = currentNode;
            prevNodeIndex = currentNodeIndex;
            currentNode = nextNode;
            currentNodeIndex = nextNodeIndex;
            var res = Encoding.UTF8.GetString(data,0,8);
            var temp = new Data(res.ToCharArray());
            nextNodeIndex = BitConverter.ToInt32(data, 8);
            return temp;
        }
        public override void Swap(int i, int j)
        {
            Byte[] data;
            Data a = IndexAt(i);
            Data b = IndexAt(j); 

            var temp = IndexAt(j);
            data = Data.GetEncoding(a.myData);
            
            fs.Seek(currentNodeIndex, SeekOrigin.Begin);
            fs.Write(data, 0,8);

            temp = IndexAt(i);
            data = Data.GetEncoding(b.myData);
            fs.Seek(currentNodeIndex, SeekOrigin.Begin);
            fs.Write(data, 0, 8);
        }

        public override Data IndexAt(int index)
        {
            //Index was negative or larger then the amount of Nodes in the list
            if (index < 0 || index > length)
                return null;
            var tmp = Head();
            // Move to index
            for (int i = 0; i < index; i++)
            {
                tmp = Next();
            }
            // return the node at the index position
            return tmp;
        }
    }
}
