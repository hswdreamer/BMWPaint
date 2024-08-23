using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Windows;

namespace BMWPaint;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Canvas.PaintSurface += Canvas_PaintSurface;
    }

    private void Canvas_PaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        SKPaint paint = new()
        {
            IsAntialias = true,
            StrokeWidth = 1,
            TextSize = 100,
            Style = SKPaintStyle.Stroke
        };

        canvas.Clear(SKColors.White);

        canvas.DrawText("Hello, SkiaSharp!", 0, 100, paint);

        canvas.DrawCircle(new(100, 100), 100, paint);

        SKPath line = new();
        line.MoveTo(100, 100);
        line.LineTo(200, 200);
        line.LineTo(300, 200);
        line.LineTo(400, 100);
        canvas.DrawPath(line, paint);

        SKPath curve = new();
        curve.MoveTo(100, 100);
        curve.CubicTo(new(200, 200), new(300, 200), new(400, 100));
        canvas.DrawPath(curve, paint);
    }
}