using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataBaseLibrary;

namespace ChatServiceLibrary
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class ChatService : IChatService
    {
        private static Dictionary<string, IChatServiceCallback> users = new Dictionary<string, IChatServiceCallback>();
        private static Dictionary<string, IChatServiceCallback> Users
        {
            get
            {
                lock(new object())
                {
                    return users;
                }
            }
            set
            {
                lock(new object())
                {
                    users = value;
                }
            }
        }
        
        /// <summary>
        /// Создать комнату
        /// </summary>
        /// <param name="name">Имя комнаты</param>
        /// <param name="password">Пароль комнаты</param>
        /// <returns></returns>
        public bool CreateRoom(string name, string password)
        {
            try
            {
                Data.CreateRoom(name, password);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Inside(string roomName, string roomPwd, string userLogin)
        {
            try
            {
                if (Data.Exist(roomName))
                {
                    Data.InsideRoom(roomName, roomPwd, userLogin);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Leave(string roomName, string userLogin)
        {
            Data.LeaveRoom(roomName, userLogin);
        }

        /// <summary>
        /// Выполнить выход пользователя из системы
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        public void LogOut(string login, string password)           //Destroi KeyVaulePair<userName, CallbackChannel>
        {
            try
            {
                if(Data.Exist(login, password))
                {
                    Users.Remove(login);
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void SendMessage(string sender, string recipient, string message)
        {
            try
            {
                List<string> recipients = Data.GetUsersInRoom(recipient);

                foreach(var login in recipients)
                {
                    Users[login].NewMessage(sender, message);
                }
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Выполнить вход пользователя в систему
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public bool SignIn(string login, string password)           //Create KyeValuePair<userName, CallbackChannel>
        {
            try
            {
                if(Data.Exist(login, password))
                {
                    Users.Add(login, OperationContext.Current.GetCallbackChannel<IChatServiceCallback>());
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Зарегестрировать пользователя в системе
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public bool SignUp(string login, string password)           //Create user in DataBase
        {
            try
            {
                if(!Data.Exist(login, password))
                {
                    Data.CreateUser(login, password);

                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
