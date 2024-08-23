using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BMWPaint;

public class TextInputBehavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.KeyDown += AssociatedObject_KeyDown;
        AssociatedObject.IsVisibleChanged += AssociatedObject_IsVisibleChanged;
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
        AssociatedObject.IsVisibleChanged -= AssociatedObject_IsVisibleChanged;
    }

    private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            AssociatedObject.Visibility = Visibility.Hidden;
    }
    private void AssociatedObject_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
            AssociatedObject.Focus();
    }

}