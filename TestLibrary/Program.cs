using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            int command = 1;

            while (command != 0)
            {
                Console.Clear();
                Console.Write("Input commdand:\t");
                command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        {
                            Console.Write("Login:\t");
                            string login = Console.ReadLine();

                            Console.Write("Password:\t");
                            string password = Console.ReadLine();

                            DataBaseLibrary.Data.CreateUser(login, password);

                            Console.WriteLine("User Created....");
                            Console.ReadKey();

                            break;
                        }
                    case 2:
                        {
                            foreach (string user in DataBaseLibrary.Data.PrintAllUser())
                                Console.WriteLine($"**********\n{user}\n*********\n\n");

                            Console.ReadKey();

                            break;
                        }
                }
            }
        }
    }
}
