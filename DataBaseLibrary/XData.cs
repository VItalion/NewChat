using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataBaseLibrary
{
    static class XData
    {
        private static XDocument DataBase { get; set; }
        private static string Path { get; set; }
        public static void Init(string path)
        {
            Path = path;
            if (System.IO.File.Exists(Path))
                DataBase = XDocument.Load(path);
            else
                DataBase = new XDocument(new XElement("root", new XElement("users"), new XElement("rooms")));
        }
        public static void CreateRoom(string roomName, string pwd)
        {
            if (!Exist(roomName))
            {
                XElement room = new XElement("room");
                room.Add(new XElement("name", roomName));
                room.Add(new XElement("password", pwd));

                DataBase.Root.Element("rooms").Add(room);
                DataBase.Save(Path);
            }
        }

        public static void CreateUser(string login, string password)
        {
            if (!Exist(login, password))
            {
                XElement room = new XElement("user");
                room.Add(new XElement("login", login));
                room.Add(new XElement("password", password));

                DataBase.Root.Element("users").Add(room);
                DataBase.Save(Path);
            }
        }

        public static void DeleteRoom(string name)
        {
            try
            {
                (from r in DataBase.Root.Element("rooms").Elements("room")
                 where r.Element("name").Value == name
                 select r).Single().Remove();
            }
            catch (Exception ex)
            {

            }
        }

        public static void DeleteUser(string login)
        {
            try
            {
                (from u in DataBase.Root.Element("users").Elements("user")
                 where u.Element("login").Value == login
                 select u).Single().Remove();
            }
            catch (Exception ex)
            {

            }
        }

        public static bool Exist(string roomName)
        {
            var room = (from element in DataBase.Root.Element("rooms").Elements("room")
                       where element.Element("name").Value == roomName
                       select element).First();

            if (room != null)
                return true;
            else
                return false;
        }

        public static bool Exist(string login, string password)
        {
            var user = (from element in DataBase.Root.Element("users").Elements("user")
                        where (element.Element("login").Value == login) && (element.Element("password").Value == password)
                        select element).First();

            if (user != null)
                return true;
            else
                return false;
        }

        public static List<string> GetUsersInRoom(string roomName)
        {
            List<string> result = new List<string>();
            var users = from u in DataBase.Root.Element("users").Elements("user")
                        
        }

        public static void InsideRoom(string roomName, string roomPwd, string login)
        {
            throw new NotImplementedException();
        }

        public static void LeaveRoom(string roomName, string login)
        {
            throw new NotImplementedException();
        }
    }
}
