
namespace ConFooCSharp.Services.Registration;

public interface IRegistrationService
{
    Task RegisterAsync(Attendee entity);

    ValueTask<ImmutableList<Attendee>> LoadEntitiesAsync(CancellationToken ct);
}

