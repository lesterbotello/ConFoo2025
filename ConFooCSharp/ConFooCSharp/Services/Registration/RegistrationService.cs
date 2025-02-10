using System;
using System.Collections.Generic;
using ConFooCSharp.Services.File;

namespace ConFooCSharp.Services.Registration;

public class RegistrationService : IRegistrationService
{
    private readonly IFileService _fileService;

    public RegistrationService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task RegisterAsync(Attendee atendee)
    {
        try
        {
            var entities = await _fileService.LoadEntitiesAsync(CancellationToken.None);
            var newList = entities?.ToList();
            newList?.Add(atendee);
            await _fileService.SaveEntitiesAsync(newList.ToImmutableList() ?? []);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public ValueTask<ImmutableList<Attendee>> LoadEntitiesAsync(CancellationToken ct) =>
        new(_fileService.LoadEntitiesAsync(ct));
}
