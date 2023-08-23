using Sdiaz.SimpleChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.Core.Business_Interface.ServiceQuery
{
    public interface IMessageServiceQuery
    {
        IEnumerable<Message> GetAll();
        IEnumerable<Message> GetReceivedMessages(string userId);
    }
}
