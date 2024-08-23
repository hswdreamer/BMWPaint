using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace BMWPaint;

public class MouseEventBehavior : Behavior<UIElement>
{
    public MouseViewModel? EventHandler
    {
        get { return (MouseViewModel?)GetValue(EventHandlerProperty); }
        set { SetValue(EventHandlerProperty, value); }
    }
    public static readonly DependencyProperty EventHandlerProperty =
        DependencyProperty.Register("EventHandler", typeof(MouseViewModel), typeof(MouseEventBehavior), new PropertyMetadata(null));

    private bool IsCaptured = false;
    private readonly HashSet<MouseButton> DragButton = [];
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseDown += MouseDown;
        AssociatedObject.MouseUp += MouseUp;
        AssociatedObject.MouseMove += MouseMove;

        AssociatedObject.MouseEnter += MouseEnter;
        AssociatedObject.MouseLeave += MouseLeave;
        AssociatedObject.MouseWheel += MouseWheel;
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.MouseDown -= MouseDown;
        AssociatedObject.MouseUp -= MouseUp;
        AssociatedObject.MouseMove -= MouseMove;

        AssociatedObject.MouseEnter -= MouseEnter;
        AssociatedObject.MouseLeave -= MouseLeave;
        AssociatedObject.MouseWheel -= MouseWheel;
    }

    private void MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (IsCaptured == false)
        {
            Mouse.Capture((IInputElement)sender);
            IsCaptured = true;
        }
        DragButton.Add(e.ChangedButton);

        e.Handled = e.ChangedButton switch
        {
            MouseButton.Left => EventHandler?.RaisLeftDown(e.GetPosition(AssociatedObject)) ?? false,
            MouseButton.Right => EventHandler?.RaisRightDown(e.GetPosition(AssociatedObject)) ?? false,
            MouseButton.Middle => EventHandler?.RaisMiddleDown(e.GetPosition(AssociatedObject)) ?? false,
            _ => false
        };
    }
    private void MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (IsCaptured == false)
            return;

        if (DragButton.Contains(e.ChangedButton) && DragButton.Count == 1)
        {
            IsCaptured = false;
            Mouse.Capture(null);
        }
        DragButton.Remove(e.ChangedButton);

        e.Handled = e.ChangedButton switch
        {
            MouseButton.Left => EventHandler?.RaisLeftClick(e.GetPosition(AssociatedObject)) ?? false,
            MouseButton.Right => EventHandler?.RaisRightClick(e.GetPosition(AssociatedObject)) ?? false,
            MouseButton.Middle => EventHandler?.RaisMiddleClick(e.GetPosition(AssociatedObject)) ?? false,
            _ => false
        };
    }
    private void MouseMove(object sender, MouseEventArgs e)
    {
        if (IsCaptured)
        {
            foreach (var btn in DragButton)
            {
                e.Handled = btn switch
                {
                    MouseButton.Left => EventHandler?.RaisLeftDrag(e.GetPosition(AssociatedObject)) ?? false,
                    MouseButton.Right => EventHandler?.RaisRightDrag(e.GetPosition(AssociatedObject)) ?? false,
                    MouseButton.Middle => EventHandler?.RaisMiddleDrag(e.GetPosition(AssociatedObject)) ?? false,
                    _ => false
                };
                if (e.Handled)
                    break;
            }
        }
        else
        {
            e.Handled = EventHandler?.RaisMove(e.GetPosition(AssociatedObject)) ?? false;
        }
    }
    private void MouseEnter(object sender, MouseEventArgs e)
    {
        e.Handled = EventHandler?.RaisEnter(e.GetPosition(AssociatedObject)) ?? false;
    }

    private void MouseLeave(object sender, MouseEventArgs e)
    {
        e.Handled = EventHandler?.RaisLeave(e.GetPosition(AssociatedObject)) ?? false;
    }

    private void MouseWheel(object sender, MouseWheelEventArgs e)
    {
        e.Handled = EventHandler?.RaisWheel(e.GetPosition(AssociatedObject), e.Delta > 0) ?? false;
    }
}