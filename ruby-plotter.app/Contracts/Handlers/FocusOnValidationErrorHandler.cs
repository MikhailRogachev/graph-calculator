using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ruby_plotter.app.Contracts.Handlers;

public static class FocusOnValidationErrorHandler
{
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(FocusOnValidationErrorHandler),
            new PropertyMetadata(false, OnEnabledChanged)
            );

    public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
    public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

    private static void OnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            if ((bool)e.NewValue)
            {
                element.PreviewLostKeyboardFocus += OnPreviewLostKeyboardFocus;
            }
            else
            {
                element.PreviewLostKeyboardFocus -= OnPreviewLostKeyboardFocus;
            }
        }
    }

    private static void OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            if (Validation.GetHasError(textBox))
            {
                e.Handled = true;
                textBox.Dispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        Keyboard.Focus(textBox);
                    }),
                    System.Windows.Threading.DispatcherPriority.Input);
            }
        }
    }
}