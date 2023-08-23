using Microsoft.EntityFrameworkCore;
using Sdiaz.SimpleChat.Core.Business_Interface;
using Sdiaz.SimpleChat.Core.Entities;
using Sdiaz.SimpleChat.Core.Enums;
using Sdiaz.SimpleChat.Core.Model;
using Sdiaz.SimpleChat.Core.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.Business
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Add(Message message)
        {
            this.unitOfWork.Repository<Message>().Add(message);
            this.unitOfWork.SaveChanges();
        }
        async Task<Message> IMessageService.DeleteMessage(MessageDeleteModel messageDeleteModel)
        {
            // var message = messageDeleteModel.Message;
            var messageRepo = this.unitOfWork.Repository<Message>();
            var message = await messageRepo.Get().Where(x => x.Id == messageDeleteModel.Message.Id).FirstOrDefaultAsync();
            if (messageDeleteModel.DeleteType == DeleteTypeEnum.DeleteForEveryone.ToString())
            {
                message.IsReceiverDeleted = true;
                message.IsSenderDeleted = true;
            }
            else
            {
                message.IsReceiverDeleted = message.IsReceiverDeleted || (message.Receiver == messageDeleteModel.DeletedUserId);
                message.IsSenderDeleted = message.IsSenderDeleted || (message.Sender == messageDeleteModel.DeletedUserId);
            }
            messageRepo.Update(message);
            await this.unitOfWork.SaveChangesAsync();
            return message;
        }
    }
}
