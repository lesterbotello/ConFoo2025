using ConFooCSharp.Services.Registration;
using ConFooCSharp.Services.Validation;

namespace ConFooCSharp.Presentation;

public partial record AddRegistrationModel
{
    private readonly INavigator _navigator;
    private readonly IRegistrationService _registrationService;
    private readonly Attendee _attendee;
    private readonly IValidationService _validationService;

    public AddRegistrationModel(Attendee attendee,
        IRegistrationService registrationService,
        INavigator navigator,
        IValidationService validationService)
    {
        _attendee = attendee;
        _registrationService = registrationService;
        _navigator = navigator;
        _validationService = validationService;
    }

    public string ConfirmationLabel => "Please confirm the information of your registration:";

    public string NameLabel => $"Your name: {_attendee.Name}";

    public string EmailLabel => $"Your email: {_attendee.Email}";

    public async Task FinishRegistration()
    {
        if (!_validationService.IsEmailValid(_attendee.Email))
        {
            await _navigator.ShowMessageDialogAsync(this, title: "Invalid email", content: "The email you entered is not valid.");

            return;
        }

        await _registrationService.RegisterAsync(_attendee);

        await _navigator.ShowMessageDialogAsync(this, title: "Registration confirmed", content: "Your registration has been confirmed, tickets on the way.");
    }
}
