using Discord;
using Discord.WebSocket;
using Discord.Interactions;

using Bot.Interactions;

namespace Bot
{
	public class NewGenerationBotOfDiscord
	{
		const string BotToken = "";
		readonly DiscordSocketClient client;
		InteractionService interactionService;

		public NewGenerationBotOfDiscord()
		{
			var config = new DiscordSocketConfig()
			{
				LogLevel = LogSeverity.Verbose,
				GatewayIntents = GatewayIntents.All
			};
			client = new DiscordSocketClient(config);
			interactionService = new InteractionService(client);
		}

		public async Task StartAsync()
		{
			var interactionsHandler = new Handler(client, interactionService);
			await interactionsHandler.InstallModulesAsync();

			await client.LoginAsync(Discord.TokenType.Bot, BotToken, false); // TODO: валидацию токена
			await client.StartAsync();

			client.Ready += OnReady;

		}
		public async Task StopAsync()
		{
			await client.StopAsync();
		}

		async Task OnReady()
		{
#if DEBUG
			await interactionService.RegisterCommandsToGuildAsync(0); // TODO: из конфига вытягивать айди дебаг-гильдии
#else
			await interactionService.RegisterCommandsGloballyAsync(); // TODO: проверка необходимости регестрации команд
#endif
		}
	}
}