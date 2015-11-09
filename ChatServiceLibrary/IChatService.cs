using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatServiceLibrary
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        bool SignUp(string login, string password);

        [OperationContract]
        bool SignIn(string login, string password);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string sender, string recipient, string message);

        [OperationContract]
        void LogOut(string login, string password);

        [OperationContract]
        bool CreateRoom(string name, string password);

        [OperationContract]
        bool Inside(string roomName, string roomPwd, string userLogin);

        [OperationContract]
        void Leave(string roomName, string userLogin);
    }
}
