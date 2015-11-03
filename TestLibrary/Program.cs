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
            string command = String.Empty;

            while (true)
            {
                //Console.Clear();
                Console.Write("DataBase>");
                command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "create": Create(); break;
                    case "add": Create(); break;
                    case "print": Print(); break;
                    case "delete": Delete(); break;
                    case "remove": Delete(); break;
                    case "exit": return;
                }
            }
        }

        static void Create()
        {
            Console.Write("Login:\t");
            string login = Console.ReadLine();

            Console.Write("Password:\t");
            string password = Console.ReadLine();

            DataBaseLibrary.Data.CreateUser(login, password);
        }
        static void Delete()
        {
            Console.Write("Login:\t");
            string login = Console.ReadLine();

            DataBaseLibrary.Data.DeleteUser(login);
        }
        static void Print()
        {
            foreach (string user in DataBaseLibrary.Data.UserToStringArray())
                Console.WriteLine($"**********\n{user}\n*********\n");
        }
    }
}
