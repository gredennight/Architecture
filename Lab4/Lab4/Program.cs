using System;

public class Program
{
    public static void Main(string[] args)
    {
        Notification emailNotification = new EmailNotification("admin@mail.com");
        emailNotification.Send("Spam message", "hi i'm arabian prince, please send me all your money");

        Notification slackNotification = new SlackNotificationAdapter("some login", "some api key", "some chat");
        slackNotification.Send("Important message", "I was scammed, what do i do now?");

        Notification smsNotification = new SmsNotificationAdapter("3141592", "Me");
        smsNotification.Send("Money transfer", "Here's $1500");
    }
}

public interface Notification
{
    void Send(string title, string message);
}

public class EmailNotification : Notification
{
    private string adminEmail;

    public EmailNotification(string adminEmail)
    {
        this.adminEmail = adminEmail;
    }

    public void Send(string title, string message)
    {
        Console.WriteLine($"Sent email with title '{title}' to '{adminEmail}' that says '{message}'.");
    }
}

public class SlackNotificationAdapter : Notification
{
    private SlackApi SlackApi;

    public SlackNotificationAdapter(string login, string apiKey, string chatId)
    {
        SlackApi = new SlackApi(login, apiKey, chatId);
    }

    public void Send(string title, string message)
    {
        SlackApi.SendMessage(title, message);
    }
}

public class SlackApi
{
    private string login;
    private string apiKey;
    private string chatId;

    public SlackApi(string login, string apiKey, string chatId)
    {
        this.login = login;
        this.apiKey = apiKey;
        this.chatId = chatId;
    }

    public void SendMessage(string title, string message)
    {
        Console.WriteLine($"Sent Slack message with title '{title}' to '{chatId}' that says '{message}'.");
    }
}

public class SmsNotificationAdapter : Notification
{
    private SmsApi smsApi;

    public SmsNotificationAdapter(string phone, string sender)
    {
        smsApi = new SmsApi(phone, sender);
    }

    public void Send(string title, string message)
    {
        smsApi.SendSms(title, message);
    }
}

public class SmsApi
{
    private string phone;
    private string sender;

    public SmsApi(string phone, string sender)
    {
        this.phone = phone;
        this.sender = sender;
    }

    public void SendSms(string title, string message)
    {
        Console.WriteLine($"Sent SMS message from {phone} ({sender}) with title '{title}' that says '{message}'.");
    }
}
