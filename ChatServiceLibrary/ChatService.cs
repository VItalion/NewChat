using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatServiceLibrary
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class ChatService : IChatService
    {
        public bool CreateRoom(string name, string password)
        {
            throw new NotImplementedException();
        }

        public bool Inside(string roomName, string roomPwd, string userLogin)
        {
            throw new NotImplementedException();
        }

        public void Leave(string roomName, string userLogin)
        {
            throw new NotImplementedException();
        }

        public void LogOut(string login)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string sender, string recipient, string message)
        {
            throw new NotImplementedException();
        }

        public bool SignIn(string login, string password)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
