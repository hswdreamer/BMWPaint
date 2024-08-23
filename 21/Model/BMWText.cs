using SkiaSharp;

namespace BMWPaint;

public class BMWText : IBMWObject
{
    public string Text { get; set; } = string.Empty;
    public SKPoint Position { get; set; }
}