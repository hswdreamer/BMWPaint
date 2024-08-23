using Microsoft.Win32;
using SkiaSharp;
using System.Windows;

namespace BMWPaint;

public class ImageTool(ObservableList<IBMWObject> objects, MatrixViewModel matrixVM) : ToolBase(objects, matrixVM)
{
    private BMWImage _obj = new();
    public override bool Init()
    {
        OpenFileDialog dlg = new();
        dlg.Filter = "Images|*.bmp;*.png;*.jpg;*.jpeg|Alll Files|*.*";
        if (dlg.ShowDialog() == false)
            return false;

        _obj.Image = SKImage.FromBitmap(SKBitmap.Decode(dlg.FileName));
        Objects.Add(_obj);
        return true;
    }
    public override void Quit()
    {
        Objects.Remove(_obj);
    }
    public override bool LeftClick(Point pt)
    {
        var skPt = MatrixVM.LogicalPoint(pt);

        BMWImage temp = new();
        temp.Image = _obj.Image;
        temp.Position = skPt;
        _obj = temp;
        Objects.Add(_obj);

        return true;
    }
    public override bool MouseMove(Point pt)
    {
        var skPt = MatrixVM.LogicalPoint(pt);

        _obj.Position = skPt;
        Objects.Tick();

        return true;
    }
}