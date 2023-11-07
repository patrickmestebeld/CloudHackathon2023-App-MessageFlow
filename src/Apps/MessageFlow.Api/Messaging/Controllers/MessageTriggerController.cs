using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageFlow.Api.Controllers;

[ApiController]
[Route("trigger")]
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
    public async Task<ActionResult<Message>> PostAsync(MessageContext trigger)
    {
        try
        {
            return Ok(await _messageAssembler.AssembleAsync(trigger));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while assembling message.");
            return StatusCode(500);
        }
    }
}