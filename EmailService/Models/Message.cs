using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.Models;

public class Message
{
    public int id { get; set; }
    public string subject { get; set; }
    public string text { get; set; }
    public bool is_successfully_sent { get; set; }
    public string sender { get; set; }
    public string recipient { get; set; }
    public string[]? carbon_copy_recipients { get; set; }
}