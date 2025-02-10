using System.Text.Json;

namespace ConFooCSharp.Services.File;

public class FileService : IFileService
{
    public async Task<ImmutableList<Attendee>> LoadEntitiesAsync(CancellationToken ct)
    {
        try
        {
            var file = await GetFile(CreationCollisionOption.OpenIfExists);

            // This is for deleting the database easily for the demo.
            // Don't uncomment unless you want to reset the database.
            // Comment back afterwards...
            // await file.DeleteAsync();

            var json = await FileIO.ReadTextAsync(file);

            var list = string.IsNullOrWhiteSpace(json) ?
                [] :
                JsonSerializer.Deserialize<List<Attendee>>(json)?
                    .ToImmutableList();

            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task SaveEntitiesAsync(IList<Attendee> entities)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(entities, options);
        var file = await GetFile(CreationCollisionOption.ReplaceExisting);
        await FileIO.WriteTextAsync(file, json);
    }

    private static async ValueTask<StorageFile> GetFile(CreationCollisionOption option)
    {
        try
        {
            return await ApplicationData.Current.LocalFolder.CreateFileAsync("registration.json", option);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
