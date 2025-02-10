namespace ConFooCSharp.Services.Navigation;

public interface INavigationService
{
    Task NavigateViewModelAsync<TViewModel>(INavigator navigator, object sender, object? data = null) where TViewModel : class;

    Task ShowMessageDialogAsync(INavigator navigator, object sender, string title, string content);
}
