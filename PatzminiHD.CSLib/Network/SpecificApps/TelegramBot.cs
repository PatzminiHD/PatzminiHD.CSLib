using System.Threading.Tasks;
using PatzminiHD.CSLib.Output;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PatzminiHD.CSLib.Network.SpecificApps;

/// <summary>
/// Represents a Telegram Bot
/// </summary>
public class TelegramBot
{
    private TelegramBotClient _bot;
    private ReplyKeyboardMarkup _keyboardMarkup;
    private CancellationTokenSource _cancelToken = new();
    private List<(string command, Func<string, long, TelegramBotClient, Task<(string? message, ParseMode parseMode)>> commandAction)> _commandActions;
    private Func<string, long, TelegramBotClient, Task<(string? message, ParseMode parseMode)>>? _unknownCommandAction;
    private List<long>? _allowedUsers;
    private List<long>? _informUsers;

    /// <summary> Initialise a new Telegram Bot object </summary>
    /// <param name="token">The token of the bot</param>
    /// <param name="replyKeyboardMarkup">The main Keyboard of the Bot<br/>Is sent to the user when they send the '/start' command</param>
    /** <param name="commandActions">Actions to be taken when the bot receives commands (messages starting with a '/')<br/>
        The specified command is given as a string starting with '/'
        Each command action is am async method with the parameters:<br/>
        - string: the full message of the user<br/>
        - long: the user ID<br/>
        - <see cref="TelegramBotClient"/>: the bot object<br/>
        The command action can optionally return a string that is sent to the user as a message</param>*/        
    /// <param name="allowedUsers">List of user IDs that are allowed to use the bot. null to allow every user</param>
    /// <param name="unknownCommandAction">Is called when the received command has no action defined in <paramref name="commandActions"/></param>
    /// <param name="informUsers">List of users that should be informed of events (bot starting, invalid users trying to access the bot...)</param>
    public TelegramBot(
        string token,
        ReplyKeyboardMarkup replyKeyboardMarkup,
        List<(string command, Func<string, long, TelegramBotClient, Task<(string?, ParseMode)>> commandAction)> commandActions,
        Func<string, long, TelegramBotClient, Task<(string?, ParseMode)>>? unknownCommandAction,
        List<long>? allowedUsers,
        List<long>? informUsers
        )
    {
        _bot = new TelegramBotClient(token);
        _keyboardMarkup = replyKeyboardMarkup;
        _commandActions = commandActions;
        _allowedUsers = allowedUsers;
        _unknownCommandAction = unknownCommandAction;
        _informUsers = informUsers;
    }

    /// <summary>
    /// Start the bot
    /// </summary>
    /// <param name="informUsers">True to send a message to the informUsers, otherwise false</param>
    public async Task Start(bool informUsers = true)
    {
        _cancelToken = new();
        _bot.StartReceiving(
            updateHandler: HandleUpdate,
            errorHandler: HandleError,
            cancellationToken: _cancelToken.Token
        );
        Logging.LogInfo("Bot started!", "TelegramBot");
        if(informUsers)
            await InformUsers("Bot started!");
    }
    /// <summary>
    /// Stop the bot
    /// </summary>
    /// <param name="informUsers">True to send a message to the informUsers, otherwise false</param>
    public async Task Stop(bool informUsers = true)
    {
        if(!_cancelToken.IsCancellationRequested)
        {
            if(informUsers)
                await InformUsers("Bot stopped!");
            _cancelToken.Cancel();
            Logging.LogInfo("Bot stopped!", "TelegramBot");
        }
        else
            Logging.LogInfo("Bot stop requested, but was already requested before", "TelegramBot");
    }

    /// <summary>
    /// Restart the bot
    /// </summary>
    /// <param name="informUsers">True to send a message to the informUsers, otherwise false</param>
    /// <returns></returns>
    public async Task Restart(bool informUsers = false)
    {
        Logging.LogInfo("Restart requested", "TelegramBot");
        await Stop(informUsers);
        await Task.Delay(500);
        await Start(informUsers);
    }

    /// <summary>
    /// Is called each time the bot receives a message
    /// </summary>
    /// <param name="_"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task HandleUpdate(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
    {
        Logging.LogVerbose($"Received update: {update.Type}", "TelegramBot");
        switch (update.Type)
        {
            // A message was received
            case UpdateType.Message:
                await HandleMessage(update.Message!);
                break;
            
            default:
                Logging.LogWarning($"Received unsupported update type: {Enum.GetName(update.Type)}", "TelegramBot");
                break;
        }
    }
    private async Task HandleMessage(Message msg)
    {
        var user = msg.From;
        var text = msg.Text ?? string.Empty;
        
        if (user is null)
            return;

        if(_allowedUsers != null && !_allowedUsers.Contains(user.Id))
        {
            Logging.LogWarning($"Invalid User '{user.Username}' (ID: '{user.Id}')");
            return;
        }

        // Print to console
        Logging.LogVerbose($"'{user.Username}' wrote '{text}'", "TelegramBot");

        // When we get a command, we react accordingly
        if (text.StartsWith("/"))
        {
            await HandleCommand(user.Id, text);
        }
    }
    private async Task HandleCommand(long userId, string command)
    {
        Logging.LogVerbose($"Received command: '{command}'", "TelegramBot");
        if(command == "/start") //Special Command
        {
            await SendKeyboard(userId);
            return;
        }
        string mainCommand = command.Split(' ').First();
        foreach(var commandAction in _commandActions)
        {
            if(commandAction.command == mainCommand)
            {
                var output = await commandAction.commandAction(command, userId, _bot);
                if(output.message != null)
                    await _bot.SendMessage(
                        userId,
                        output.message,
                        output.parseMode
                    );
                return;
            }
        }
        if(_unknownCommandAction != null)
        {
            var output = await _unknownCommandAction(command, userId, _bot);
            if(output.message != null)
                await _bot.SendMessage(
                    userId,
                    output.message,
                    output.parseMode
                );
        }

    }
    private async Task InformUsers(string message, ParseMode parseMode = ParseMode.None)
    {
        if(InformUsers == null)
            return;

        foreach(var user in _informUsers!)
        {
            await _bot.SendMessage(
                user,
                message,
                parseMode
            );
        }
    }
    /// <summary>
    /// Is called when an error occures in the bot
    /// </summary>
    /// <param name="_"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task HandleError(ITelegramBotClient _, Exception exception, CancellationToken cancellationToken)
    {
        Logging.LogError(exception.ToString(), "TelegramBot");

        //Wait 60 seconds
        await Task.Delay(60000);
        await Restart(false);

        // await InformUsers($"Exception occured:\n{exception.ToString()}");
    }

    private async Task SendKeyboard(long userId)
    {
        await _bot.SendMessage(
            userId,
            "Sending Keyboard...",
            ParseMode.Html,
            replyMarkup: _keyboardMarkup
        );
    }
}
