﻿using SkiaSharp;
using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class RectTool(ObservableList<IBMWObject> objects) : ToolBase(objects)
{
    private BMWRect? _obj = null;
    public override bool LeftClick(Point pt)
    {
        var skPt = pt.ToSKPoint();

        if (_obj == null)
        {
            _obj = new();
            _obj.Rect = new(skPt.X, skPt.Y, skPt.X, skPt.Y);
            Objects.Add(_obj);
        }
        else
        {
            _obj!.Rect = new(_obj.Rect.Left, _obj.Rect.Top, skPt.X, skPt.Y);
            Objects.Tick();
            _obj = null;
        }

        return true;
    }
    public override bool MouseMove(Point pt)
    {
        if (_obj == null)
            return false;

        var skPt = pt.ToSKPoint();

        _obj!.Rect = new SKRect(_obj.Rect.Left, _obj.Rect.Top, skPt.X, skPt.Y);
        Objects.Tick();

        return true;
    }
}