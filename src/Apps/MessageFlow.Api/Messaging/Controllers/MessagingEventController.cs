using MessageFlow.Core.Messaging.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessageFlow.Api.Controllers;

[ApiController]
[Route("expose")]
public class MessagingEventController : ControllerBase
{
    private readonly ILogger<MessageTriggerController> _logger;
    private readonly IMessageEventWatcher _messageEventWatcher;

    public MessagingEventController(ILogger<MessageTriggerController> logger, IMessageEventWatcher messageEventWatcher)
    {
        _logger = logger;
        _messageEventWatcher = messageEventWatcher;
    }

    [HttpGet]
    public ActionResult<string> Expose()
        => Ok(_messageEventWatcher.ExposeConnectionString());
}