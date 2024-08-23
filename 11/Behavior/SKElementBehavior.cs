using Microsoft.Xaml.Behaviors;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class SKElementBehavior : Behavior<SKElement>
{
    public CanvasViewModel? EventHandler
    {
        get { return (CanvasViewModel?)GetValue(EventHandlerProperty); }
        set { SetValue(EventHandlerProperty, value); }
    }
    public static readonly DependencyProperty EventHandlerProperty =
        DependencyProperty.Register("EventHandler", typeof(CanvasViewModel), typeof(SKElementBehavior), new PropertyMetadata(null));

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PaintSurface += PaintSurface;
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PaintSurface -= PaintSurface;
    }

    private void PaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        EventHandler?.PaintSurface(e.Surface.Canvas);
    }
}