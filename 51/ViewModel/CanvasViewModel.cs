using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Windows;

namespace BMWPaint;

public class CanvasViewModel : ReactiveObject
{
    [Reactive] public int Update { get; set; } = 0;
    public Size ActualSize { get; set; } = new();
    public void Refresh() => ++Update;

    private ObservableCollection<IBMWObject> _objects;
    private MatrixViewModel _matrixVM;

    private SKPaint _strokePaint;
    private SKPaint _fillPaint;

    public CanvasViewModel(ObservableCollection<IBMWObject> objects, MatrixViewModel matrixVM)
    {
        _objects = objects;
        _matrixVM = matrixVM;
        _objects.CollectionChanged += (_, _) => Refresh();
        this.WhenAnyValue(x => x._matrixVM.Matrix).Subscribe((_) => Refresh());
        _strokePaint = new()
        {
            IsAntialias = true,
            StrokeWidth = 1,
            Style = SKPaintStyle.Stroke
        };

        _fillPaint = new()
        {
            IsAntialias = true,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
            Color = SKColors.Blue,
            TextSize = 20,
            Typeface = SKFontManager.Default.MatchCharacter('가')
        };

    }

    public void PaintSurface(SKCanvas canvas)
    {
        canvas.Clear(SKColors.White);
        canvas.SetMatrix(_matrixVM.Matrix);
        foreach (var obj in _objects)
        {
            switch (obj)
            {
                case BMWLine line: Draw(canvas, line); break;
                case BMWCurve curve: Draw(canvas, curve); break;
                case BMWRect rect: Draw(canvas, rect); break;
                case BMWCircle circle: Draw(canvas, circle); break;
                case BMWOval oval: Draw(canvas, oval); break;
                case BMWText text: Draw(canvas, text); break;
                case BMWImage image: Draw(canvas, image); break;
                default:
                    break;
            }
        }
    }
    private void Draw(SKCanvas canvas, BMWLine line)
    {
        SKPath path = new();
        path.MoveTo(line.Points[0]);
        for (var i = 1; i < line.Points.Count; i++)
            path.LineTo(line.Points[i]);
        canvas.DrawPath(path, _strokePaint);
    }

    private void Draw(SKCanvas canvas, BMWCurve curve)
    {
        SKPath path = new();
        path.MoveTo(curve.Start);
        path.CubicTo(curve[0], curve[1], curve[2]);
        canvas.DrawPath(path, _strokePaint);
    }
    private void Draw(SKCanvas canvas, BMWRect rect) => canvas.DrawRect(rect.Rect, _strokePaint);
    private void Draw(SKCanvas canvas, BMWCircle circle) => canvas.DrawCircle(circle.Center, circle.Radius, _fillPaint);
    private void Draw(SKCanvas canvas, BMWOval oval) => canvas.DrawOval(oval.Oval, _strokePaint);
    private void Draw(SKCanvas canvas, BMWText text) => canvas.DrawText(text.Text, text.Position, _fillPaint);
    private void Draw(SKCanvas canvas, BMWImage image) => canvas.DrawImage(image.Image, image.Position);
}