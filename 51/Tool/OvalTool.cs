using SkiaSharp;
using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class OvalTool(ObservableList<IBMWObject> objects, MatrixViewModel matrixVM) : ToolBase(objects, matrixVM)
{
    private BMWOval? _obj = null;
    public override bool LeftClick(Point pt)
    {
        var skPt = MatrixVM.LogicalPoint(pt);

        if (_obj == null)
        {
            _obj = new();
            _obj.Oval = new(skPt.X, skPt.Y, skPt.X, skPt.Y);
            Objects.Add(_obj);
        }
        else
        {
            _obj.Oval = new(_obj.Oval.Left, _obj.Oval.Top, skPt.X, skPt.Y);
            Objects.Tick();
            _obj = null;
        }

        return true;
    }
    public override bool MouseMove(Point pt)
    {
        if (_obj == null)
            return false;

        var skPt = MatrixVM.LogicalPoint(pt);

        _obj.Oval = new SKRect(_obj.Oval.Left, _obj.Oval.Top, skPt.X, skPt.Y);
        Objects.Tick();

        return true;
    }
}