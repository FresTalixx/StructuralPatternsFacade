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



using StructuralPatterns;


var sender = new NotificationFacade(new SmsService(), new EmailService(), new PushService());

sender.SendSimpleMessage("+1234324325", "SIMPLE TEXT1");
sender.SendWithTitle("ttt@grfgfg.com", "Title", "Text with title", true);
sender.SendUrgentPushWithSms("+1234324325", "URGENT MESSAGE!!!");