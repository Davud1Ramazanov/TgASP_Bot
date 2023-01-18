using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgASP_Bot.Cache;
using TgASP_Bot.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Update = Telegram.Bot.Types.Update;

namespace TgASP_Bot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        private static gadgetstore_dbContext gadgetstore_DbContext = new gadgetstore_dbContext();
        private readonly ICacheService cacheService;

        [HttpPost]
        public async Task<IResult> Post([FromBody] Update update)
        {
            TelegramBotClient client = new TelegramBotClient("5933761202:AAExw_HPhs6a6umhO1NI-X6fP4p3oZbMP-c");

            TelegramBotClient client1 = new TelegramBotClient("5687556161:AAGchmEtxU5eAK6dR35p6FYULOj1Gt4ylns");


            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
                {
                    var buttonInline = InlineKeyboardButton.WithCallbackData(text: "Accept order", callbackData: "Order");
                    var buttonInlineMarkup = new InlineKeyboardMarkup(buttonInline);
                    await client1.SendTextMessageAsync(1291241130, $"@{update.CallbackQuery.From.Username} wants to buy \n{update.CallbackQuery.Message.Text}", replyMarkup: buttonInlineMarkup);
                    if(update.CallbackQuery == buttonInlineMarkup.InlineKeyboard)
                    {
                        await client.SendTextMessageAsync(update.CallbackQuery.From.Id, "You have successfully placed an order!");
                        await client1.SendTextMessageAsync(update.CallbackQuery.From.Id, "Order was accepted!");
                    }
                }

                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    if (client != null)
                    {
                        await client1.SendTextMessageAsync(update.Message.From.Id, $"Text: {update.Message.Text}\nID: {update.Message.Chat.Id}\nUsername: @{update.Message.Chat.Username}");
                    }

                    if (update.Message.Text == "/start")
                    {
                        await client.SendTextMessageAsync(update.Message.From.Id, $"Hello, @{update.Message.From.Username}");
                    }
                    if (update.Message.Text == "/dice")
                    {
                        await client.SendDiceAsync(update.Message.From.Id);
                    }
                    if (update.Message.Text == "/picture")
                    {
                        await client.SendPhotoAsync(update.Message.From.Id, photo: "https://upload.wikimedia.org/wikipedia/commons/4/42/Blue_sky%2C_white-gray_clouds.JPG");
                    }

                    if (update.Message.Text.Equals("/add"))
                    {
                        var cmd1 = update.Message.Text.Split(" ");
                        gadgetstore_DbContext.Add(new Gadget { IdCategory = Convert.ToInt32(cmd1[0]), Name = cmd1[1], Price = Convert.ToInt32(cmd1[2]) });
                        gadgetstore_DbContext.SaveChanges();
                    }
                    if (update.Message.Text.Equals("/delete"))
                    {
                        var cmd1 = update.Message.Text.Split(" ");
                        gadgetstore_DbContext.Remove(cmd1[0]);
                        gadgetstore_DbContext.SaveChanges();
                    }

                    if (update.Message.Text.Contains("/products"))
                    {
                        List<gadgetstore_dbContext> gadget = cacheService.GetData<List<gadgetstore_dbContext>>("Gadget");
                        if (gadget == null)
                        {
                            var gadgetsSQL = await gadgetstore_DbContext.Gadgets.ToListAsync();
                            if (gadgetsSQL.Count > 0)
                            {
                                //var cmd1 = update.Message.Text.Split(" ");
                                //var gadgets = gadgetstore_DbContext.Gadgets;
                                //var cmd2 = gadgets.Where(x => x.Id.ToString().Equals(cmd1[1])).FirstOrDefault();
                                //await client.SendTextMessageAsync(update.Message.From.Id, cmd2.Name);
                                cacheService.SetData("Gadget", gadgetsSQL, DateTimeOffset.Now.AddDays(1));
                            }
                        }
                    }

                    if (update.Message.Text.Equals("/gadgets"))
                    {
                        var buttonInline = InlineKeyboardButton.WithCallbackData(text: "Buy", callbackData: "buy");
                        var buttonInlineMarkup = new InlineKeyboardMarkup(buttonInline);
                        foreach (var item in gadgetstore_DbContext.Gadgets)
                        {
                            await client.SendTextMessageAsync(update.Message.From.Id, $"Name: {item.Name}\nPrice: {item.Price} UAH", replyMarkup: buttonInlineMarkup);
                        }
                    }
                }
            }
            return Results.Ok();
        }
    }
}