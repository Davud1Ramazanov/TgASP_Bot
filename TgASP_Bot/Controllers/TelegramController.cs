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
        private static Category category = new Category();

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
                    if (update.Message.Text == "/dice")
                    {
                        await client.SendDiceAsync(update.Message.From.Id);
                    }
                }
            }


            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    if (update.Message.Text == "/picture")
                    {
                        await client.SendPhotoAsync(update.Message.From.Id, photo: "https://upload.wikimedia.org/wikipedia/commons/4/42/Blue_sky%2C_white-gray_clouds.JPG");
                    }
                }
            }

            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    var command1 = "/product 1".ToString().Split(" ");
                    if (update.Message.Text == $"{command1}" && category.Id == command1.Length)
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


//var header = Request.Headers["forecast"].ToString().Split(",");