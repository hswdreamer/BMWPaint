using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;
public class MatrixViewModel : ReactiveObject
{
    public Point PanStart { get; set; }
    public float Scale { get; private set; } = 1.0f; 
    [Reactive] public SKMatrix Matrix { get; private set; } = SKMatrix.Identity;
    
    private SKPoint _translat = new();

    public void Pan(Point Target)
    {
        var temp = (Target - PanStart);
        _translat += new Point(temp.X, temp.Y).ToSKPoint();
        Matrix = SKMatrix.CreateScaleTranslation(Scale, Scale, _translat.X, _translat.Y);
        PanStart = Target;
    }
    public bool Zoom(Point pt, bool isUp)
    {
        var old = Scale;
        if (isUp)
            Scale += 0.1f;
        else
            Scale -= 0.1f;
        if (Scale > 2f)
            Scale = 2f;
        else if (Scale < 0.5f)
            Scale = 0.5f;
        if (old == Scale)
            return false;

        var before = LogicalPoint(pt);
        Matrix = SKMatrix.CreateScaleTranslation(Scale, Scale, _translat.X, _translat.Y);
        PanStart = DevicePoint(before);
        Pan(pt);
        return true;
    }
    public Point DevicePoint(SKPoint pt) => Matrix.MapPoint(pt).ToPoint();
    public SKPoint LogicalPoint(Point pt)
    {
        if (Matrix.TryInvert(out var mat) == false)
            return pt.ToSKPoint();
        return mat.MapPoint(pt.ToSKPoint());
    }
}