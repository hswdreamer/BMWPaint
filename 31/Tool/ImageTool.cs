using Microsoft.Win32;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class ImageTool(ObservableList<IBMWObject> objects) : ToolBase(objects)
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
        var skPt = pt.ToSKPoint();

        BMWImage temp = new();
        temp.Image = _obj.Image;
        temp.Position = skPt;
        _obj = temp;
        Objects.Add(_obj);
        
        return true;
    }
    public override bool MouseMove(Point pt)
    {
        var skPt = pt.ToSKPoint();

        _obj.Position = skPt;
        Objects.Tick();
        
        return true;
    }
}