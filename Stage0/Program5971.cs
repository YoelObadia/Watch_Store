using System;

namespace Stage0
{
    partial class program
    {
        static void Main(string[] args)
        {   
            Welcome5971();
            Welcome3422();
            Console.ReadKey();
        }
        static partial void Welcome3422();
        private static void Welcome5971()
        {
            Console.WriteLine(" ENTER YOUR NAME: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my frist console application", userName);
        }
    }
}