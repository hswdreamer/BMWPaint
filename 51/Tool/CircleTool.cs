using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class CircleTool(ObservableList<IBMWObject> objects, MatrixViewModel matrixVM) : ToolBase(objects, matrixVM)
{
    private BMWCircle? _obj = null;
    public override bool LeftClick(Point pt)
    {
        var skPt = MatrixVM.LogicalPoint(pt);

        if (_obj == null)
        {
            _obj = new();
            _obj.Center = skPt;
            Objects.Add(_obj);
        }
        else
        {
            _obj.Radius = (skPt - _obj.Center).Length;
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
        _obj.Radius = (skPt - _obj.Center).Length;
        Objects.Tick();

        return true;
    }
}