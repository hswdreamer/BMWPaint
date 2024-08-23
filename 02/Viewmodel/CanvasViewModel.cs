using SkiaSharp;

namespace BMWPaint;

public class CanvasViewModel
{
    private SKPaint _strokePaint;
    private SKPaint _fillPaint;

    public CanvasViewModel()
    {
        _strokePaint = new()
        {
            IsAntialias = true,
            StrokeWidth = 1,
            TextSize = 100,
            Style = SKPaintStyle.Stroke
        };

        _fillPaint = new()
        {
            IsAntialias = true,
            StrokeWidth = 1,
            TextSize = 100,
            Style = SKPaintStyle.Fill,
            Color = SKColors.Blue
        };
    }

    public void PaintSurface(SKCanvas canvas)
    {
        canvas.Clear(SKColors.White);

        SKPath line = new();
        line.MoveTo(new(100, 100));
        line.LineTo(new(200, 200));
        line.LineTo(new(300, 200));
        line.LineTo(new(400, 100));
        canvas.DrawPath(line, _strokePaint);

        SKPath curve = new();
        curve.MoveTo(new(100, 100));
        curve.CubicTo(new(200, 200), new(300, 200), new(400, 100));
        canvas.DrawPath(curve, _strokePaint);

        canvas.DrawRect(new SKRect(400, 100, 200, 200), _strokePaint);

        canvas.DrawText("Hello, SkiaSharp!", new(0, 100), _fillPaint);

        canvas.DrawCircle(new(500, 200), 100, _fillPaint);
    }
}