using System.Configuration;

using Bot;

const string botTokenConfigKey = "token";
var botToken = ConfigurationManager.AppSettings[botTokenConfigKey];
if (string.IsNullOrWhiteSpace(botToken))
{
	throw new Exception(); // TOOD: исключение
}

var bot = new NewGenerationBotOfDiscord(botToken);

#if DEBUG
const string debugGuildIdConfigToken = "debugGuildId";
var debugGuildIdRaw = ConfigurationManager.AppSettings[debugGuildIdConfigToken];
if(!ulong.TryParse(debugGuildIdRaw, out ulong debugGuildId))
{
	throw new Exception(); // TODO: исключение
}
else if (debugGuildId == 0)
{
	throw new Exception(); // TODO: исключение
}

bot.DebugGuildId = debugGuildId;
#endif

await bot.StartAsync();
await Task.Delay(-1);
