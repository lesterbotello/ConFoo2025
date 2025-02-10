namespace ConFooXaml.Models;

public class Message(string text, string contactName = "", DateTimeOffset timestamp = default, bool isMyMessage = true)
{
    public string Text { get; init; } = text;

    public string ContactName { get; init; } = contactName == "" ? "Me" : contactName;

    public DateTimeOffset Timestamp { get; init; } = timestamp == default ? DateTimeOffset.Now : timestamp;

    public bool IsMyMessage { get; init; } = isMyMessage;

    public string UserFriendlyTimestamp => Timestamp.LocalDateTime.ToString("t");

    public static Message Empty => new Message(string.Empty, string.Empty, DateTimeOffset.MinValue);
}
