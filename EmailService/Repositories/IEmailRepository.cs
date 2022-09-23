using EmailService.Models;

namespace EmailService.Repositories;

public interface IEmailRepository
{
   public IEnumerable<Message> GetAllMessages();
   public void Insert(Message message);
}