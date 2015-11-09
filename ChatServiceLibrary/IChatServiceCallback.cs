using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatServiceLibrary
{
    interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void NewMessage(string sender, string message);
    }
}
