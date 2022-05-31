using Discord;
using Discord.WebSocket;
using Discord.Interactions;

using Bot.Interactions;

namespace Bot
{
	public class NewGenerationBotOfDiscord
	{
		readonly string botToken;
		readonly DiscordSocketClient client;
		readonly InteractionService interactionService;

		public ulong DebugGuildId = default;

		public NewGenerationBotOfDiscord(string token)
		{
			var config = new DiscordSocketConfig()
			{
				LogLevel = LogSeverity.Verbose,
				GatewayIntents = GatewayIntents.All
			};
			client = new DiscordSocketClient(config);
			interactionService = new InteractionService(client);
			
			botToken = token;
		}

		public async Task StartAsync()
		{
			var interactionsHandler = new Handler(client, interactionService);
			await interactionsHandler.InstallModulesAsync();

			await client.LoginAsync(Discord.TokenType.Bot, botToken, false); // TODO: валидацию токена
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
			await interactionService.RegisterCommandsToGuildAsync(DebugGuildId); //TODO: проверка на вхождение бота в target-гильдию?
#else
			await interactionService.RegisterCommandsGloballyAsync(); // TODO: проверка необходимости регестрации команд
#endif
		}
	}
}