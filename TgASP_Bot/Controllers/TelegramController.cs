using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using TgASP_Bot.Models;

namespace TgASP_Bot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        private static gadgetstore_dbContext gadgetstore_DbContext = new gadgetstore_dbContext();

        [HttpPost]
        public async Task<IResult> Post([FromBody] Update update)
        {
            TelegramBotClient client = new TelegramBotClient("5933761202:AAExw_HPhs6a6umhO1NI-X6fP4p3oZbMP-c");

            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    if (update.Message.Text == "/start")
                    {
                        await client.SendTextMessageAsync(update.Message.From.Id, $"Hello @{update.Message.From.Username}");
                    }
                }
            }


            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    if (update.Message.Text == "/start")
                    {
                        await client.SendDiceAsync(update.Message.From.Id);
                    }
                }
            }


            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    if (update.Message.Text == "Picture")
                    {
                        await client.SendPhotoAsync(update.Message.From.Id, photo: "https://upload.wikimedia.org/wikipedia/commons/4/42/Blue_sky%2C_white-gray_clouds.JPG");
                    }
                }
            }

            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    if (update.Message.Text == "Gadgets")
                    {
                        foreach (var item in gadgetstore_DbContext.Gadgets)
                        {
                            await client.SendTextMessageAsync(update.Message.From.Id, $"{item.Name}");
                        }
                    }
                }
            }

            return Results.Ok();
        }
    }
}