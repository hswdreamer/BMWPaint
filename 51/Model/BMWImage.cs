using SkiaSharp;

namespace BMWPaint;

public class BMWImage : IBMWObject
{
    public SKImage? Image { get; set; } = null;
    public SKPoint Position { get; set; }
}