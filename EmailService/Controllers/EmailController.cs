using System.Data;
using Dapper;
using EmailService.Options;
using EmailService.Repositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using Npgsql;

namespace EmailService.Controllers;

[ApiController]
[Route("v1/api/")]
public class EmailController : ControllerBase
{
    private readonly IOptions<EmailConfig> _options;
    private readonly IEmailRepository _repository;

    public EmailController(IOptions<EmailConfig> options, IEmailRepository repository)
    {
        _options = options;
        _repository = repository;
    }
    
    [HttpGet("email")]
    public IEnumerable<Models.Message> GetAllMessages()
    {
        return _repository.GetAllMessages();
    }

    [HttpPost("email")]
    public ActionResult<Models.Message> SendMessage([FromBody]RequestBodyModels.Message message)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_options.Value.Name, _options.Value.Address));
        mimeMessage.To.Add(MailboxAddress.Parse(message.Recipient));
        mimeMessage.Subject = message.Subject;
        mimeMessage.Body = new TextPart("plain") { Text = message.Text };
        if (message.CarbonCopyRecipients != null && message.CarbonCopyRecipients.Count != 0)
        {
            foreach (var address in message.CarbonCopyRecipients)
            {
                mimeMessage.Cc.Add(MailboxAddress.Parse(address));
            }
        }
        
        bool isSuccessfullySent = true;
        
        var cli = new SmtpClient();
        try
        {
            cli.Connect("smtp.gmail.com", 465, true);
            cli.Authenticate(_options.Value.Address, _options.Value.Password);
            cli.Send(mimeMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            isSuccessfullySent = false;
        }
        finally
        {
            cli.Disconnect(true);
            cli.Dispose();
        }
        var messageModel = new Models.Message()
        {
            text = message.Text,
            is_successfully_sent = isSuccessfullySent,
            recipient = message.Recipient,
            sender = _options.Value.Address,
            subject = message.Subject,
            carbon_copy_recipients = message.CarbonCopyRecipients?.ToArray(),
        };
        _repository.Insert(messageModel);
        return messageModel;
    }
}