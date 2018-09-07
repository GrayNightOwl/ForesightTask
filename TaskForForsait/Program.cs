using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskForForsait
{
    class Program
    {
        static void Main(string[] args)
        {

            StringBuilder sb = new StringBuilder("Привет, мир");
            Console.WriteLine("Длина " + sb.Length);
            Console.WriteLine("Объём " + sb.Capacity);
            sb.Append(", счастья тебе");
            Console.WriteLine("Длина " + sb.Length);
            Console.WriteLine("Объём " + sb.Capacity);
            sb.Append(", счастья тебе");
            Console.WriteLine("Длина " + sb.Length);
            Console.WriteLine("Объём " + sb.Capacity);
            Console.ReadLine();
        }
    }
}
