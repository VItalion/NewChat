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
            bool flag = true;

            while (flag)
            {
                //Console.Clear();
                Console.Write("DataBase>");
                command = Console.ReadLine();

                flag = Parse(command);
            }
        }

        static bool Parse(string str)
        {
            try
            {
                string[] commands = str.Split('/');
                foreach (var s in commands)
                    s.Trim();

                string[] command = commands[0].Split(' ');

                switch (command[0])
                {
                    case "create":
                        {
                            if ((commands[1] == null)&&(commands[2]== null))
                            Create(command[1], commands[1], commands[2]); return true;
                        }
                    case "add": Create(command[1], commands[1], commands[2]); return true;
                    case "print": Print(command[1]); return true;
                    case "delete": Delete(command[1], commands[1]); return true;
                    case "drop": Delete(command[1], commands[1]); return true;
                    case "remove": Delete(command[1], commands[1]); return true;
                    case "help": Help(); return true;
                    case "exit": return false;
                    default: return true;
                }
            }
            catch (NullParameterException ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }

        static void Create(string command, params string[] parameters)
        {
            switch (command)
            {
                case "user":
                    {
                        if (parameters[0] == null)
                            throw new NullParameterException("Логин не может быть пустым");
                        string login = parameters[0];

                        if (parameters[1] == null)
                            throw new NullParameterException("Пароль не может быть пустым");
                        string password = parameters[1];

                        DataBaseLibrary.Data.CreateUser(login, password);
                        break;
                    }
                case "room":
                    {
                        if (parameters[0] == null)
                            throw new NullParameterException("Имя комнаты не может быть пустым");
                        string name = parameters[0];

                        if (parameters[1] == null)
                            throw new NullParameterException("Пароль не может быть пустым");
                        string password = parameters[1];

                        DataBaseLibrary.Data.CreateRoom(name, password);
                        break;
                    }
            }
        }
        static void Delete(string command, string parameter)
        {
            switch (command)
            {
                case "user":
                    {
                        if (parameter == null)
                            throw new NullParameterException("Логин не может быть пустым");
                        string login = parameter;

                        DataBaseLibrary.Data.DeleteUser(parameter);
                        break;
                    }
                case "room":
                    {
                        if (parameter == null)
                            throw new NullParameterException("Имя комнаты не может быть пустым");
                        string name = parameter;

                        DataBaseLibrary.Data.DeleteRoom(name);
                        break;
                    }
            }
        }
        static void Print(string command)
        {
            switch (command)
            {
                case "users":
                    {
                        foreach (string user in DataBaseLibrary.Data.UserToStringArray())
                            Console.WriteLine($"**********\n{user}\n*********\n");
                        break;
                    }
                case "rooms":
                    {
                        foreach (string room in DataBaseLibrary.Data.RoomToStringArray())
                            Console.WriteLine($"**********\n{room}\n*********\n");
                        break;
                    }
            }
        }
        static void Help()
        {
            Console.WriteLine("create user/room - Создать пользователя/комнату\nkey: login/name - логин пользователя/имя комнаты\n     password - пароль пользователя/комнаты\n");
            Console.WriteLine("add user/room - Создать пользователя/комнату\nkey: login/name - логин пользователя/имя комнаты\n     password - пароль пользователя/комнаты\n");
            Console.WriteLine("delete user/room - Удалить пользователя/комнату\nkey: login/name - логин пользователя/имя комнаты\n");
            Console.WriteLine("drop user/room - Удалить пользователя/комнату\nkey: login/name - логин пользователя/имя комнаты\n");
            Console.WriteLine("remove user/room - Удалить пользователя/комнату\nkey: login/name - логин пользователя/имя комнаты\n");
            Console.WriteLine("print users/rooms - Вывести на экран пользователей/комнаты\n");
            Console.WriteLine("exit - Выход из консоли\n");
        }
    }
}
