using ConFooCSharp.Services.Registration;

namespace ConFooCSharp.Presentation;
public partial class AttendeesListModel
{
    private readonly IRegistrationService _registrationService;

    public AttendeesListModel(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    public IListFeed<Attendee> Entities => ListFeed.Async(_registrationService.LoadEntitiesAsync);
}
