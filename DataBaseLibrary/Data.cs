using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLibrary
{
    public class Data
    {
        /// <summary>
        /// Проверка на существование пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns></returns>
        public static bool Exist(string login, string password)
        {
            using (DataContext context = new DataContext())
            {
                var users = from user in context.Users
                            where (user.Login == login) && (user.Password == password)
                            select user;

                foreach (var user in users)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Проверка на существование комнаты
        /// </summary>
        /// <param name="roomName">Имя комнаты</param>
        /// <returns></returns>
        public static bool Exist(string roomName)
        {
            using (DataContext context = new DataContext())
            {
                var rooms = from room in context.Rooms
                            where room.Name == roomName
                            select room;

                foreach (var room in rooms)
                    return true;

                return false;
            }
        }

        public static void CreateUser(string login, string password)
        {
            using (DataContext context = new DataContext())
            {
                var user = context.Users
                    .Where(u => u.Login == login)
                    .Select(u => u.Login)
                    .FirstOrDefault();

                if (user != login)
                {
                    User newUser = new User();
                    newUser.Login = login;
                    newUser.Password = password;

                    context.Users.Add(newUser);

                    context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// Получить список пользователей в комнате
        /// </summary>
        /// <param name="roomName">Имя комнаты</param>
        /// <returns>Список пользователей</returns>
        public static List<string> GetUsersInRoom(string roomName)
        {
            using (DataContext context = new DataContext())
            {
                var room = (from r in context.Rooms
                            where r.Name == roomName
                            select r).First();

                List<string> result = new List<string>();
                foreach(var user in room.Users)
                {
                    result.Add(user.Login);
                }
                return result;
            }
        }

        public static void DeleteUser(string login)
        {
            using (DataContext context = new DataContext())
            {
                var users = from item in context.Users
                            where item.Login == login
                            select item;

                foreach(var user in users)
                {
                    context.Users.Remove(user);
                }

                context.SaveChanges();
            }
        }

        public static void CreateRoom(string name, string pwd)
        {
            using (DataContext context = new DataContext())
            {
                var room = context.Rooms
                    .Where(u => u.Name == name)
                    .Select(u => u.Name)
                    .FirstOrDefault();

                if (room != name)
                {
                    Room newRoom = new Room();
                    newRoom.Name = name;
                    newRoom.Password = pwd;

                    context.Rooms.Add(newRoom);

                    context.SaveChanges();
                }

            }
        }

        public static void InsideRoom(string roomName, string roomPwd, string login)
        {
            using (DataContext context = new DataContext())
            {
                Room room = (from r in context.Rooms
                              where (r.Name == roomName) && (r.Password == roomPwd)
                              select r).First();

                User user = (from u in context.Users
                             where (u.Login == login)
                             select u).First();

                room.Users.Add(user);

                context.SaveChanges();
            }
        }

        public static void LeaveRoom(string roomName, string login)
        {
            using (DataContext context = new DataContext())
            {
                Room room = (from r in context.Rooms
                             where r.Name == roomName
                             select r).First();

                User user = (from u in context.Users
                             where u.Login == login
                             select u).First();

                room.Users.Remove(user);
            }
        }

        public static void DeleteRoom(string name)
        {
            using (DataContext context = new DataContext())
            {
                var rooms = from item in context.Rooms
                            where item.Name == name
                            select item;

                foreach (var room in rooms)
                {
                    context.Rooms.Remove(room);
                }

                context.SaveChanges();
            }
        }

        public static List<string> UserToStringArray()
        {
            using (var context = new DataContext())
            {
                List<string> result = new List<string>();

                var users = context.Users.Select(u => u);

                foreach(var user in users)
                {
                    string temp = $"Login:\t{user.Login}\nPassword:\t{user.Password}";
                    result.Add(temp);
                }

                return result;
            }
        }

        public static List<string> RoomToStringArray()
        {
            using (var context = new DataContext())
            {
                List<string> result = new List<string>();

                var rooms = context.Rooms.Select(u => u);

                foreach (var room in rooms)
                {
                    string temp = $"Login:\t{room.Name}\nPassword:\t{room.Password}";
                    result.Add(temp);
                }

                return result;
            }
        }
    }
}
