using CSharpDotNetDemo.Library;
using Newtonsoft.Json;
using System;

namespace CSharpDotNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Linq linq = new Linq();
            linq.Examples();
        }
    }
}
