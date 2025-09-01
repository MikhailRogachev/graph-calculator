using System.Windows;
using System.Windows.Input;

namespace ruby_plotter.app.Contracts.Handlers;

public static class _EnterKeyHandler
{
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(EnterKeyHandler),
            new PropertyMetadata(false, OnIsEnabledChanged));

    public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
    public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            if ((bool)e.NewValue)
            {
                element.KeyDown += OnKeyDown;
            }
            else
            {
                element.KeyDown -= OnKeyDown;
            }
        }
    }

    private static void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var request = new TraversalRequest(FocusNavigationDirection.Next);

            (Keyboard.FocusedElement as UIElement)?.MoveFocus(request);
            e.Handled = true;
        }
    }
}