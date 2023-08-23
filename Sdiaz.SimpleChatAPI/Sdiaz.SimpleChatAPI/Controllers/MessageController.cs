using Microsoft.AspNetCore.Mvc;
using Sdiaz.SimpleChat.Core.Business_Interface.ServiceQuery;
using Sdiaz.SimpleChat.Core.Business_Interface;
using Sdiaz.SimpleChat.Core.Model;

namespace Sdiaz.SimpleChatAPI.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMessageServiceQuery messageServiceQuery;
        private readonly IMessageService messageService;
        public MessageController(IMessageServiceQuery messageServiceQuery, IMessageService messageService)
        {
            this.messageServiceQuery = messageServiceQuery;
            this.messageService = messageService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = this.messageServiceQuery.GetAll();
            return Ok(messages);
        }


        [HttpGet("received-messages/{userId}")]
        public IActionResult GetUserReceivedMessages(string userId)
        {
            var messages = this.messageServiceQuery.GetReceivedMessages(userId);
            return Ok(messages);
        }
        [HttpPost()]
        public async Task<IActionResult> DeleteMessage([FromBody] MessageDeleteModel messageDeleteModel)
        {
            var message = await this.messageService.DeleteMessage(messageDeleteModel);
            return Ok(message);
        }
    }
}
