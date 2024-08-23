using SkiaSharp;

namespace BMWPaint;
public class BMWCurve : IBMWObject
{
    public SKPoint Start { get; set; }
    private SKPoint[] _points = new SKPoint[3];
    public SKPoint this[int n]
    {
        get
        {
            if (n < 0 || n > 2)
                return new();
            return _points[n];
        }
        set
        {
            if (n < 0 || n > 2)
                return;
            if (n <= 2)
                _points[2] = value;
            if (n <= 1)
                _points[1] = value;
            if (n <= 0)
                _points[0] = value;

            _points[n] = value;
        }
    }
}