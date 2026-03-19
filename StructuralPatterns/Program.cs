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
using System.Text;

Console.OutputEncoding = Encoding.UTF8;


var sender = new NotificationFacade(new SmsService(), new EmailService(), new PushService());

sender.SendSimpleMessage("+1234324325", "SIMPLE TEXT1");
sender.SendWithTitle("ttt@grfgfg.com", "Title", "Text with title", true);
sender.SendUrgentPushWithSms("+1234324325", "URGENT MESSAGE!!!");



Kava kava = new Kava();

Action<IKava> printKavaInfo = p => Console.WriteLine(p.GetDescription() + " costs " + p.GetPrice());

var myKava = new KavaDecorator(
    new KavaDecorator(
        new KavaDecorator(
            new KavaDecorator(
                new KavaDecorator(
                    new KavaDecorator(
                        kava,
                        "Coffee",
                        20),
                    "with milk", 12),
                "with milk", 12),
            "with chocolate", 18),
        "with vannila syrop", 15),
    "with milk", 2);


printKavaInfo(myKava);


var menu = new Section("Головне меню");

var soups = new Section("Супи");
soups.AddChild(new Dish("Борщ", 95));
soups.AddChild(new Dish("Солянка", 110));


var baseline = new Section("Основні страви");
baseline.AddChild(new Dish("Піца 4 сири", 220));
baseline.AddChild(new Dish("Стейк Рібай", 380));

var discountOffer = new Section("Акційні пропозиції");
discountOffer.AddChild(new Dish("Піца 4 сири зі знижкою 20%", 176));
discountOffer.AddChild(new Dish("Стейк Рібай зі знижкою 20%", 304));


menu.AddChild(soups);
menu.AddChild(baseline);
menu.AddChild(discountOffer);

menu.Display(new TextPrinter());
menu.Display(new HTMLprinter());
menu.Display(new JsonPrinter());

