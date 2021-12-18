using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JpgMetadataRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> _files = new List<string>();
            List<byte> _bytes = new List<byte>();


            Console.WriteLine("Jpg Metadata Remover by kgftbz");
            Console.WriteLine(string.Empty);
            Console.WriteLine("This program will remove the metadata from all .jpg and .jpeg files in this folder");
            Console.WriteLine("Press any key the continue...");
            Console.ReadKey();

            _files.AddRange(Directory.GetFiles(".", "*.jpg"));
            _files.AddRange(Directory.GetFiles(".", "*.jpeg"));

            Console.WriteLine(_files.Count + " files found");

            for (int i = 0; i < _files.Count; i++)
            {
                Console.WriteLine("Doing file " + _files[i]);

                _bytes.AddRange(File.ReadAllBytes(_files[i]));

                /*Console.WriteLine("Addr read " + _bytes[4].ToString("X") + _bytes[5].ToString("X"));
                Console.WriteLine("Parsed as " + int.Parse(_bytes[4].ToString("X") + _bytes[5].ToString("X"), System.Globalization.NumberStyles.HexNumber));*/

                int _jfifAddr = 6 + int.Parse(_bytes[4].ToString("X2") + _bytes[5].ToString("X2"), System.Globalization.NumberStyles.HexNumber);

                _bytes.RemoveRange(4, _jfifAddr - 4);

                File.WriteAllBytes(_files[i], _bytes.ToArray());
                _bytes.Clear();
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("This program had finished removing the metadata from all .jpg and .jpeg files in this folder");
            Console.WriteLine("Press any key the close...");
            Console.ReadKey();
        }
    }
}
