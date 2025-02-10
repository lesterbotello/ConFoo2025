using Windows.Foundation.Collections;

namespace ConFooCSharp.Behaviors;
public static class ScrollToBottomBehavior
{
    private static ListView? _listView;

    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
        "IsEnabled",
        typeof(bool),
        typeof(ScrollToBottomBehavior),
        new PropertyMetadata(default(bool), OnIsEnabledChanged));

    public static bool GetIsEnabled(FrameworkElement element)
        => (bool)element.GetValue(IsEnabledProperty);

    public static void SetIsEnabled(FrameworkElement element, bool value)
        => element.SetValue(IsEnabledProperty, value);

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ListView listView)
        {
            if ((bool)e.NewValue)
            {
                _listView = listView;
                listView.Loaded += ListView_Loaded;
                listView.Items.VectorChanged += ListView_VectorChanged;
            }
            else
            {
                _listView = null;
                listView.Loaded -= ListView_Loaded;
                listView.Items.VectorChanged -= ListView_VectorChanged;
            }
        }
    }

    private static void ListView_VectorChanged(IObservableVector<object> sender, IVectorChangedEventArgs @event)
    {
        if(sender is ItemCollection itemCollection)
        {
            if(_listView is not null)
            {
                ScrollToBottom(_listView);
            }
        }
    }

    private static void ListView_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is ListView listView)
        {
            ScrollToBottom(listView);
        }
    }

    private static void ScrollToBottom(ListView listView)
    {
        if (listView.Items.Count > 0)
        {
            listView.ScrollIntoView(listView.Items[listView.Items.Count - 1]);
        }
    }
}
