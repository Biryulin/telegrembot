using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.WebRequestMethods;

class Program
{
    // переменные для подсчёта зп
    static int a = 0;
    static int b = 0;
    // переменные для заполнения адресов
    static string z = "Данная заявка пуста";
    static string x = "Данная заявка пуста";
    static string c = "Данная заявка пуста";
    static string v = "Данная заявка пуста";
    static string marsh = "Маршрут пока не составлен";
    static async Task Main(string[] args)
    {
        var client = new TelegramBotClient("6930277112:AAFC3v0j7YkMEGoQzWfnbpoI4MjQcn3_IM8");
        client.StartReceiving(Update, Error);
        Console.ReadLine();

    }

    private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }
    // место где все действия
    async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken arg3)
    {
        var mes = update.Message;
        //получение информации о выполнении заказа
        if (mes.Text != null)
        {
            if (mes.Text.ToLower().Contains("+"))
            {
                a = a + 1;
                await botClient.SendTextMessageAsync(mes.Chat.Id, "Заказ выполнен");
                Console.WriteLine("Заказ № " + a + " выполнели в " + mes.Date);
                return;
            }
            // заполнение адреса 1
            if (mes.Text.ToLower().Contains("!1"))
            {
                z = mes.Text;
                Console.WriteLine(z);
                return;
            }
            // заполнение адреса 2
            if (mes.Text.ToLower().Contains("!2"))
            {
                x = mes.Text;
                Console.WriteLine(x);
                return;
            }
            // заполнение адреса 3
            if (mes.Text.ToLower().Contains("!3"))
            {
                c = mes.Text;
                Console.WriteLine(c);
                return;
            }
            // заполнение адреса 4
            if (mes.Text.ToLower().Contains("!4"))
            {
                v = mes.Text;
                Console.WriteLine(v);
                return;
            }
            if (mes.Text.ToLower().Contains("!карта"))
            {
                marsh = mes.Text;
                Console.WriteLine(marsh);
                return;
            }
        }
        // создание кнопок для вывода адресов/заявок
        ReplyKeyboardMarkup rKbM = new(new[]
            {
                new KeyboardButton[] {"Маршрут на карте"},
                new KeyboardButton[] {"Заявка №1","Заявка №2"},
                new KeyboardButton[] { "Заявка №3", "Заявка №4"},
                new KeyboardButton[] {"Завершение смены"}
            });
        await botClient.SendTextMessageAsync(mes.Chat.Id, "!", replyMarkup: rKbM);
        // действия кнопок 
        switch (mes.Text)
        {
            case "Заявка №1":
                await botClient.SendTextMessageAsync(mes.Chat.Id, z);
                if (a >= 1)
                {
                    await botClient.SendTextMessageAsync(mes.Chat.Id, "Этот заказ уже выполнен");
                }
                break;
            case "Заявка №2":
                await botClient.SendTextMessageAsync(mes.Chat.Id, x);
                if (a >= 2)
                {
                    await botClient.SendTextMessageAsync(mes.Chat.Id, "Этот заказ уже выполнен");
                }
                break;
            case "Заявка №3":
                await botClient.SendTextMessageAsync(mes.Chat.Id, c);
                if (a >= 3)
                {
                    await botClient.SendTextMessageAsync(mes.Chat.Id, "Этот заказ уже выполнен");
                }
                break;
            case "Заявка №4":
                await botClient.SendTextMessageAsync(mes.Chat.Id, v);
                if (a >= 4)
                {
                    await botClient.SendTextMessageAsync(mes.Chat.Id, "Все заказы на сегодня уже выполнены");
                }
                break;
            case ("Завершение смены"):
                b = a * 300;
                await botClient.SendTextMessageAsync(mes.Chat.Id, "За сеголня вы заработали: " + b);
                break;
            case ("Маршрут на карте"):
                await botClient.SendTextMessageAsync(mes.Chat.Id, marsh);
                break;
        }
    }
}
