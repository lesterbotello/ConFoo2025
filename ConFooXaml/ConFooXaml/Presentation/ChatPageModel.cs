using ConFooXaml.Services;

namespace ConFooXaml.Presentation;

public partial record ChatPageModel
{
    IMessageService _messageService;

    public ChatPageModel(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public IListState<Message> Messages => ListState.Async(this, _messageService.GetMessages);

    public IState<string> NewMessageString => State<string>.Value(this, () => string.Empty);

    public async ValueTask AddMessage(CancellationToken ct)
    {
        var messageString = await NewMessageString;
        var newMessage = new Message(messageString!);
        await _messageService.AddMessage(newMessage, ct);

        await NewMessageString.Update(old => string.Empty, ct);

        await Messages.AddAsync(newMessage, ct);
    }
}
