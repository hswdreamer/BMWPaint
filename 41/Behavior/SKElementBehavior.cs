using Microsoft.Xaml.Behaviors;
using ReactiveUI;
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
        DependencyProperty.Register("EventHandler", typeof(CanvasViewModel), typeof(SKElementBehavior), new PropertyMetadata(null, EventHandlerChanged));

    private IDisposable? _updateSubscribe = null;
    private static void EventHandlerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        if (obj is not SKElementBehavior behavior || e.NewValue is not CanvasViewModel eventHandler)
            return;

        behavior._updateSubscribe?.Dispose();
        behavior._updateSubscribe = eventHandler.WhenAnyValue(x => x.Update).Subscribe(_ => behavior.AssociatedObject?.InvalidateVisual());
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PaintSurface += PaintSurface;
        AssociatedObject.SizeChanged += SizeChanged;
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PaintSurface -= PaintSurface;
        AssociatedObject.SizeChanged -= SizeChanged;
    }

    private void PaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        EventHandler?.PaintSurface(e.Surface.Canvas);
    }
    private void SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (EventHandler == null)
            return;
        EventHandler.ActualSize = e.NewSize;
    }

}