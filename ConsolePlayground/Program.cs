using System.Configuration;

using Bot;

var bot = new NewGenerationBotOfDiscord();
await bot.StartAsync();
await Task.Delay(-1);
