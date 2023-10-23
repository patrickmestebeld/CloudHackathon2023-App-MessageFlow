using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageFlow.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageTriggerController : ControllerBase
{
    private readonly ILogger<MessageTriggerController> _logger;
    private readonly IMessageAssembler _messageAssembler;

    public MessageTriggerController(ILogger<MessageTriggerController> logger, IMessageAssembler messageAssembler)
    {
        _logger = logger;
        _messageAssembler = messageAssembler;
    }

    // Todo: make use of DTO with validations later on.
    [HttpPost]
    public ActionResult<Message> Post(MessageTrigger trigger)
    {

        var message = _messageAssembler.Assemble(trigger);
        return message;
    }
}