public class EmailService : IMessageService
{
    public void SendMessage(string message)
    {
        System.Console.WriteLine($"Email sent: {message}");
    }
}