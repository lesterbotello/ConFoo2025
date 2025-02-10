using ConFooCSharp.Services.Navigation;

namespace ConFooCSharp.Presentation;

public partial record MainModel
{
    private INavigator _navigator;
    private INavigationService _navigationService;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        INavigationService navigationService)
    {
        _navigator = navigator;
        _navigationService = navigationService;
        Title = "ConFoo 2025 Registration";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public IState<string> Email => State<string>.Value(this, () => string.Empty);

    public async Task GoToRegistration()
    {
        var name = await Name;
        var email = await Email;
        await _navigator.NavigateViewModelAsync<AddRegistrationModel>(this, data: new Attendee(name!, email!));
    }

    public async Task GoToAttendeesList()
    {
        await _navigator.NavigateViewModelAsync<AttendeesListModel>(this);
    }
}
