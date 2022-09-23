using System.Data;
using Dapper;
using EmailService.Models;
using EmailService.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace EmailService.Repositories;

public class EmailRepository : IEmailRepository
{
    private readonly IOptions<DbConfig> _options;

    public EmailRepository(IOptions<DbConfig> options)
    {
        _options = options;
    }
    
    public IEnumerable<Message> GetAllMessages()
    {
        using IDbConnection db = new NpgsqlConnection(_options.Value.ConnectionString);
        return db.Query<Message>($"SELECT * FROM message");
    }

    public void Insert(Message message)
    {
        using IDbConnection db = new NpgsqlConnection(_options.Value.ConnectionString);
        var query = "INSERT INTO message (sender, recipient, carbon_copy_recipients, subject, text, is_successfully_sent) "+
        "VALUES (@sender, @recipient, @carbon_copy_recipients, @subject, @text, @is_successfully_sent)";
        db.Query(query, message);
        query = "SELECT id FROM message";
        message.id = db.Query<int>(query).LastOrDefault();
    }
}