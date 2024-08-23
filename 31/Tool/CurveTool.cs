using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class CurveTool(ObservableList<IBMWObject> objects) : ToolBase(objects)
{
    private BMWCurve? _obj = null;
    private int _current = 0;
    public override bool LeftClick(Point pt)
    {
        var skPt = pt.ToSKPoint();
        if (_obj == null)
        {
            _obj = new();
            _obj.Start = skPt;
            _obj[0] = skPt;
            _current = 0;
            Objects.Add(_obj);
        }
        else
        {
            _obj[_current] = skPt;
            if (_current == 2)
            {
                _obj = null;
                _current = 0;
            }
            else
                ++_current;

            Objects.Tick();
        }

        return true;
    }
    public override bool MouseMove(Point pt)
    {
        if (_obj == null)
            return false;

        var skPt = pt.ToSKPoint();
        _obj[_current] = skPt;
        Objects.Tick();

        return true;
    }
}