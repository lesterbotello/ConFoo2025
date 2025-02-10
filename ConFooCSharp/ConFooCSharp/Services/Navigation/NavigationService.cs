namespace ConFooCSharp.Services.Navigation;

public class NavigationService : INavigationService
{
    public Task NavigateViewModelAsync<TViewModel>(INavigator navigator, object sender, object? data = null) where TViewModel : class
    {
        return navigator.NavigateViewModelAsync<TViewModel>(sender, data: data);
    }

    public Task ShowMessageDialogAsync(INavigator navigator, object sender, string title, string content)
    {
        return navigator.ShowMessageDialogAsync(sender, null, content, title);
    }
}
