using Sdiaz.SimpleChat.Core.Entities;
using Sdiaz.SimpleChat.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.Core.Business_Interface
{
    public interface IMessageService
    {
        void Add(Message message);
        Task<Message> DeleteMessage(MessageDeleteModel messageDeleteModel);
    }
}
