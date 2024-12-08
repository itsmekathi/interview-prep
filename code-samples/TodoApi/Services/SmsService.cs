public class SmsService : IMessageService
{
    public void SendMessage(string message)
    {
        System.Console.WriteLine($"SMS Sent: {message}");
    }
}