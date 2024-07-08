using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleAppTelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient bot = new TelegramBotClient("7398864374:AAF5cNhbtFnvNWDbpb4-orRBsXjTCcSSbKg");
            bot.StartReceiving(Update, Error);
             

            Console.WriteLine("Bot is working...\n\n\n");
            Console.ReadLine();
        }






        private static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Message message = update.Message;
            CallbackQuery backQuery = update.CallbackQuery;





             

            //Текстовый запрос
            if (message != null)
                GetAnswer(botClient, message);
           

             



            //Нажатие кнопки
            if (update.CallbackQuery != null)
            {
                string answer = String.Empty;

                switch (update.CallbackQuery?.Data)
                {
                    case "11":
                        answer = "Your cart is empty";
                        break;
                    case "21":
                        answer = "🏡";
                        break;
                    default:
                        return;
                }


                await botClient.SendTextMessageAsync(backQuery.Message.Chat.Id, answer, protectContent: true);

            }



            return;
        }


        private static async void GetAnswer(ITelegramBotClient botClient, Message? message)
        {
            switch (message.Text.ToLower())
            {
                case "/start":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Welcome to my bot!");
                    break;
                case "hi":
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Hi, {message.Chat.FirstName}!");
                    break;
                case "inline":
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Here is menu:", replyMarkup: GetInlineMenu());
                    break;
                case "info":
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Here is info...", replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("Сайт", "https://aliexpress.ru/")));
                    break;
                case "slash":
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Here is slash menu:", replyMarkup: GetSlashMenu());
                    break;
                default:
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"The message is invalid!");
                    break;
            }
        }



        //Answer



        //Inline menu.  Меню в самом сообщении
        private static IReplyMarkup? GetInlineMenu()
        {
            return new InlineKeyboardMarkup(new[]
                 {
                    //first row
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("🛒 Заказы","11"),
                       InlineKeyboardButton.WithCallbackData("💬  Отзывы","12"),
                    },
                    //second row
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("🏴󠁧󠁢󠁷󠁬󠁳󠁿 Адреса","21"),
                       InlineKeyboardButton.WithCallbackData("☎️ Телефоны","22"),
                       InlineKeyboardButton.WithCallbackData("👨🏻‍🎓 Пользователи","23"),
                    }
                });
        }




        //Slash menu.   Меню под сообщением
        private static IReplyMarkup? GetSlashMenu()
        {
            return new ReplyKeyboardMarkup(
                new List<List<KeyboardButton>>
                {
                    //Первый ряд Smash
                    new List<KeyboardButton>()
                    {
                        new KeyboardButton("😐 Один"),
                        new KeyboardButton("⚽️ Два"),
                        new KeyboardButton("👗 Три"),
                    },
                    //Второй ряд Smash
                    new List<KeyboardButton>()
                    {
                        new KeyboardButton("🥊 Четыре"),
                        new KeyboardButton("🥅 Пять"),
                        new KeyboardButton("👟 Шесть"),
                    }
                })
            { ResizeKeyboard = true }; //Чтобы кнопки не были огромными

        }


        


        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

    }
}
