using ConFooCSharp.Behaviors;
using ConFooCSharp.Presentation.Extensions;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation;
using Windows.UI.Text;

namespace ConFooCSharp.Presentation;
public partial class ChatPage : Page
{
    static Brush deepPink = new SolidColorBrush(Colors.DeepPink);
    static Brush deepBlue = new SolidColorBrush(Colors.DeepSkyBlue);

    public ChatPage()
    {
        this.DataContext<ChatPageViewModel>((page, vm) => page
            .NavigationCacheMode(NavigationCacheMode.Required)
            .Content(
                new Grid()
                    .Name(out var mainGrid)
                    .SafeArea(SafeArea.InsetMask.All)
                    .RowDefinitions(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) })
                    .RowDefinitions(new RowDefinition { Height = GridLength.Auto })
                    .Children(
                        new FeedView()
                            .Grid(row: 0)
                            .Source(() => vm.Messages)
                            .ValueTemplate<FeedViewState>(feedViewState =>
                                new ListView()
                                    .ItemsSource(() => feedViewState.Data)
                                    .Style(GetListViewStyle())
                                    .ItemTemplateSelector<Message>((item, selector) =>
                                    {
                                        selector
                                            .Default(() => new TextBlock().Text("<Invalid message>"))
                                            .Case(m => m.IsMyMessage, () => GetMyMessageTemplate(item))
                                            .Case(m => !m.IsMyMessage, () => GetOtherMessageTemplate(item));
                                    })
                            )
                            .ProgressTemplate<FeedViewState>(_ =>
                                new ProgressRing()
                            )
                            .ErrorTemplate<FeedViewState>(_ =>
                                new TextBlock()
                                    .Text("An error has occurred retrieving messages")
                            )
                            .NoneTemplate<FeedViewState>(_ =>
                                new TextBlock()
                                    .Text("No Results")
                            ),
                        new Grid()
                            .Grid(row: 1)
                            .ColumnDefinitions(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) })
                            .ColumnDefinitions(new ColumnDefinition { Width = GridLength.Auto })
                            //.DataContext(() => vm.NewMessage)
                            .Name(out var messageGrid)
                            .Children(
                                new TextBox()
                                    // This leverages the C# markup API to build our own markup extension method
                                    // and we use it to assign an attached property easily
                                    .TextBoxCommand(() => vm.AddMessage)
                                    .Name("MessageTextBox")
                                    .Grid(column: 0)
                                    .Margin(10, 0, 0, 10)
                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                    .Style(new Style<TextBox>()
                                        .Setters(setters => setters
                                            .Add(ReversedPointerWheel.IsEnabledProperty, true)
                                        )
                                    )
                                    .Text(x =>
                                        x.Binding(() => vm.NewMessageString) // You can bind directly to the property name
                                            .Mode(BindingMode.TwoWay)
                                            .UpdateSourceTrigger(UpdateSourceTrigger.PropertyChanged)
                                ),
                                new Button()
                                    .Name("SendButton")
                                    .Grid(column: 1)
                                    .Margin(10, 0, 10, 10)
                                    .Command(() => vm.AddMessage)
                                    .Content("Send")
                                )
                        )
                )
            );
#pragma warning restore Uno0007 // An assembly required for a component is missing: Not really missing
    }

    private static Style<ListView> GetListViewStyle()
    {
        return new Style<ListView>()
            .Setters(setters => setters
                .Add(ScrollToBottomBehavior.IsEnabledProperty, true)
                .Add(ReversedPointerWheel.IsEnabledProperty, true));
    }

    private static UIElement GetMyMessageTemplate(Message message)
    {
        return new StackPanel()
            .Margin(20)
            .Children(new Border()
                .MinWidth(300)
                .MinHeight(100)
                .Background(deepPink)
                .CornerRadius(10)
                .Child(new Grid()
                    .RowDefinitions(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) })
                    .RowDefinitions(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) })
                    .RowDefinitions(new RowDefinition { Height = GridLength.Auto })
                    .Children(
                        new TextBlock()
                            .Margin(15, 10, 0, 0)
                            .FontWeight(new FontWeight(700)) // This is "Bold" in Uno but it's marked as internal
                            .Text(x => x.Binding(() => message.ContactName)),
                        new TextBlock()
                            .Margin(15, 0, 0, 0)
                            .Grid(row: 1)
                            .TextWrapping(TextWrapping.WrapWholeWords)
                            .Text(x => x.Binding(() => message.Text)),
                       new TextBlock()
                            .Margin(15, 0, 15, 15)
                            .Grid(row: 2)
                            .HorizontalAlignment(HorizontalAlignment.Right)
                            .Text(x => x.Binding(() => message.UserFriendlyTimestamp))
                    )
                ),
                new Polygon()
                    .Margin(0, 0, 10, 0)
                    .HorizontalAlignment(HorizontalAlignment.Right)
                    .Fill(deepPink)
                    .Points(new Point(0, 0), new Point(10, 0), new Point(10, 10))
            );
    }

    private static UIElement GetOtherMessageTemplate(Message message)
    {
        return new StackPanel()
            .Margin(20)
            .Children(new Border()
                .MinWidth(300)
                .MinHeight(100)
                .Background(deepBlue)
                .CornerRadius(10)
                .Child(new Grid()
                    .RowDefinitions(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) })
                    .RowDefinitions(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) })
                    .RowDefinitions(new RowDefinition { Height = GridLength.Auto })
                    .Children(
                        new TextBlock()
                            .Margin(15, 10, 0, 0)
                            .FontWeight(new FontWeight(700)) // This is "Bold" in Uno but it's marked as internal
                            .Text(x => x.Binding(() => message.ContactName)),
                       new TextBlock()
                            .Margin(15, 0, 0, 0)
                            .Grid(row: 1)
                            .TextWrapping(TextWrapping.WrapWholeWords)
                            .Text(x => x.Binding(() => message.Text)),
                       new TextBlock()
                            .Margin(15, 0, 15, 15)
                            .Grid(row: 2)
                            .HorizontalAlignment(HorizontalAlignment.Right)
                            .Text(x => x.Binding(() => message.UserFriendlyTimestamp))
                    )
                ),
                new Polygon()
                    .Margin(10, 0, 0, 0)
                    .HorizontalAlignment(HorizontalAlignment.Left)
                    .Fill(deepBlue)
                    .Points(new Point(0, 0), new Point(10, 0), new Point(0, 10))
            );
    }

}
