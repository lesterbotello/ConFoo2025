
namespace ConFooCSharp.Services.File;

public interface IFileService
{
    Task<ImmutableList<Attendee>> LoadEntitiesAsync(CancellationToken ct);

    Task SaveEntitiesAsync(IList<Attendee> entities);
}
