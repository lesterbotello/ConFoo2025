using Windows.UI.Text;

namespace ConFooCSharp.Presentation;

public sealed partial class AttendeesListPage : Page
{
    public AttendeesListPage()
    {
        this.DataContext<AttendeesListViewModel>((page, vm) => page
            .Background(Theme.Brushes.Background.Default)
            .Content(new Grid()
                .SafeArea(SafeArea.InsetMask.All)
                .RowDefinitions("Auto,*")
                .Children(
                    new NavigationBar()
                        .Content("Attendees"),
                    new AutoLayout()
                        .Grid(row: 1)
                        .HorizontalAlignment(HorizontalAlignment.Center)
                        .VerticalAlignment(VerticalAlignment.Center)
                        .Spacing(16)
                        .Children(
                            new TextBlock()
                                .Text("Registered Attendees:")
                                .HorizontalAlignment(HorizontalAlignment.Center)
                                .VerticalAlignment(VerticalAlignment.Center)
                                .FontSize(24)
                                .FontWeight(new FontWeight(28)),
                            new FeedView()
                                .Name("EntitiesFeedView")
                                .AutoLayout(primaryAlignment: AutoLayoutPrimaryAlignment.Stretch)
                                .VerticalAlignment(VerticalAlignment.Stretch)
                                .VerticalContentAlignment(VerticalAlignment.Stretch)
                                .Source(() => vm.Entities)
                                .ErrorTemplate(GetErrorTemplate)
                                .NoneTemplate(GetNoneTemplate)
                                .ValueTemplate<FeedViewState>(feedViewState =>
                                    new ListView()
                                        .IsItemClickEnabled(true)
                                        .Background(Theme.Brushes.Background.Default)
                                        .ItemsSource(() => feedViewState.Data)
                                        .Padding(12, 8)
                                        .AutoLayout(primaryAlignment: AutoLayoutPrimaryAlignment.Stretch)
                                        .ItemTemplate<Attendee>(GetItemTemplate)
                                )
                        )
                    )
                )
            );
    }

    private static UIElement GetItemTemplate(Attendee entity) =>
        new TextBlock()
            .Text(x => x
                .Binding(() => entity.Name)
                .Convert(name => $"Registered attendee: {name}")
                .Mode(BindingMode.OneTime)
            )
            .FontSize(16)
            .Foreground(Theme.Brushes.Primary.Default)
            .Padding(8);

    private static UIElement GetErrorTemplate() =>
        new TextBlock().Text("An error occurred while loading the attendees. Please try again later.");

    private static UIElement GetNoneTemplate() =>
        new TextBlock().Text("No attendees have been registered yet.");
}
