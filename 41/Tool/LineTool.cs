using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class LineTool(ObservableList<IBMWObject> objects) : ToolBase(objects)
{
    private BMWLine? _obj = null;
    public override bool LeftClick(Point pt)
    {
        var skPt = pt.ToSKPoint();

        if (_obj == null)
        {
            _obj = new();
            _obj.Points.Add(skPt);
            _obj.Points.Add(skPt);
            Objects.Add(_obj);
        }
        else
        {
            _obj.Points.Add(skPt);
            Objects.Tick();
        }

        return true;
    }
    public override bool MouseMove(Point pt)
    {
        if (_obj == null)
            return false;

        var skPt = pt.ToSKPoint();

        _obj.Points[^1] = skPt;
        Objects.Tick();

        return true;
    }
    public override bool RightClick(Point pt)
    {
        if (_obj == null)
            return false;

        var skPt = pt.ToSKPoint();

        _obj.Points[^1] = skPt;
        Objects.Tick();
        _obj = null;

        return true;
    }
}