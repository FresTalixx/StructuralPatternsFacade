using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralPatterns;

/*
У вашій системі є стара підсистема для відправки повідомлень клієнтам через різні канали. Кожен канал має свій клас і свій набір методів:
public class SmsService { 
    public void SendSms(string phone, string text) { ... }
    public bool CheckDelivery(string messageId) { ... }
}

public class EmailService { 
    public void SendEmail(string email, string subject, string body) { ... }
    public void AddAttachment(string messageId, byte[] file) { ... }
}

public class PushService { 
    public void SendPush(string deviceToken, string title, string body) { ... }
    public void SetBadge(string deviceToken, int count) { ... }
}

Створити клас NotificationFacade, який надасть зручний єдиний інтерфейс для найбільш поширених сценаріїв:

Відправити просте текстове повідомлення (вибирає канал автоматично або за пріоритетом)
Відправити повідомлення з темою (для email) або з заголовком (для push)
Відправити термінове push-повідомлення + sms дублювання
Отримати статус доставки (незалежно від каналу)
*/

public interface ISerive
{
    string Name { get; }
    bool HasDelivered(string messageId);
}



public class SmsService : ISerive
{
    string ISerive.Name => "SmsService";

    public string currentMessageId { get; set; } = "";
    private bool hasDelivered { get; set; }

    public void SendSms(string phone, string text)
    { 
        Console.WriteLine("Sending SMS to " + phone + ": " + text);
        currentMessageId = "sms123"; // Simulate message ID generation
    }

    public bool HasDelivered(string messageId)
    {
        if (messageId == "sms123")
        {
            return true;
        }
        return false;
    }
}

public class EmailService : ISerive
{
    string ISerive.Name => "EmailService";
    public string CurrentMessageId { get; set; } = "";
    public List<byte[]> Attachments { get; set; } = new List<byte[]>();

    private bool hasDelivered { get; set; }

    public void SendEmail(string email, string subject, string body)
    {
        Console.WriteLine("Sending Email to " + email + ": " + subject + " - " + body);
        CurrentMessageId = email;
    }
    public void AddAttachment(string messageId, byte[] file)
    {
        if (messageId == CurrentMessageId)
        {
            Attachments.Add(file);
        }
    }

    public bool HasDelivered(string messageId)
    {
        var random = new Random();
        var hasDelivered = random.Next(0, 2);

        return hasDelivered != 0;
    }
}

public class PushService : ISerive
{
    string ISerive.Name => "PushService";
    public string currentDeviceToken { get; set; } = "";
    public List<int> Badges { get; set; } = new();

    private bool hasDelivered { get; set; }

    public void SendPush(string deviceToken, string title, string body)
    {
        Console.WriteLine("Sending Push to " + deviceToken + ": " + title + " - " + body);
        currentDeviceToken = deviceToken;
    }
    public void SetBadge(string deviceToken, int count)
    {
        if (deviceToken == currentDeviceToken)
        {
            Badges.Add(count);
        }
    }

    public bool HasDelivered(string messageId)
    {
        var random = new Random();
        var hasDelivered = random.Next(0, 2);

        return hasDelivered != 0;
    }
}

public class NotificationFacade
{
    private readonly SmsService _smsService;
    private readonly EmailService _emailService;
    private readonly PushService _pushService;
    public NotificationFacade(SmsService smsService, EmailService emailService, PushService pushService )
    {
        _smsService = smsService;
        _emailService = emailService;
        _pushService = pushService;
    }

    public void SendSimpleMessage(string phone, string text, string prioritazedServiceName = "")
    {
        if (prioritazedServiceName == "")
        {
            var random = new Random();
            var serviceNum = random.Next(0, 3);
            ISerive randomService;

            if (serviceNum == 0)
            {
                _smsService.currentMessageId = phone;
                _smsService.SendSms(phone, text);
                randomService = _smsService;
            }
            else if (serviceNum == 1)
            {
                _emailService.CurrentMessageId = phone;
                _emailService.SendEmail(phone, "Simple message", text);
                randomService = _emailService;
            }
            else if (serviceNum == 2)
            {
                _pushService.currentDeviceToken = phone;
                _pushService.SendPush(phone, "Simple message", text);
                randomService = _pushService;
            }
            else
            {
                Console.WriteLine("Incorrect service!!!");
                throw new ArgumentException("Invalid service used!");
            }


            if (randomService.HasDelivered(phone))
            {
                Console.WriteLine("The message is delivered!");
            }
            else
            {
                Console.WriteLine("The message has not been delivered...");
            }
        }
        else
        {
            if (prioritazedServiceName == "SmsService")
            {
                _smsService.currentMessageId = phone;
                _smsService.SendSms(phone, text);
            }
            else if (prioritazedServiceName == "EmailService")
            {
                _emailService.CurrentMessageId = phone;
                _emailService.SendEmail(phone, "Simple message", text);
            }
            else if (prioritazedServiceName == "PushService")
            {
                _pushService.currentDeviceToken = phone;
                _pushService.SendPush(phone, "Simple message", text);
            }
            else
            {
                Console.WriteLine("Incorrect service!!!");
                throw new ArgumentException("Invalid service used!");
            }
        }

        
    }

    public void SendWithTitle(string email, string title, string text, bool isEmail)
    {
        if (isEmail)
        {
            _emailService.CurrentMessageId = email;
            _emailService.SendEmail(email, title, text);
        }
        else
        {
            _pushService.currentDeviceToken = email;
            _pushService.SendPush(email, "Title!", text);
        }
        
    }

    public void SendUrgentPushWithSms(string phone, string text)
    {
        _pushService.currentDeviceToken = phone;
        _emailService.CurrentMessageId = phone;

        _pushService.SendPush(phone, "Urgent!", text);
        _smsService.SendSms(phone, text);
    }

    public bool CheckDelivery(string messageId)
    {
        return _smsService.HasDelivered(messageId)
            && _pushService.HasDelivered(messageId)
            && _emailService.HasDelivered(messageId);
    }
}

