using Sdiaz.SimpleChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.Core.Model
{
    public class MessageDeleteModel
    {
        public string DeleteType { get; set; }
        public Message Message { get; set; }
        public string DeletedUserId { get; set; }
    }
}
