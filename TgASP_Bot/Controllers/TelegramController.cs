using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
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

                    {
                        var cmd1 = update.Message.Text.Split(" ");
                        var gadgets = gadgetstore_DbContext.Gadgets;
                        var cmd2 = gadgets.Where(x => x.Id.ToString().Equals(cmd1[1])).FirstOrDefault();
                        await client.SendTextMessageAsync(update.Message.From.Id, cmd2.Name);
                    }
                }
            }
            return Results.Ok();
        }
    }
}