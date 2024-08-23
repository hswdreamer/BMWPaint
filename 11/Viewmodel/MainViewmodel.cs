namespace BMWPaint;

public class MainViewModel
{
    public CanvasViewModel CanvasVM { get; init; }

    private List<IBMWObject> _objects = [];

    public MainViewModel()
    {
        BMWLine line = new();
        line.Points.Add(new(100, 100));
        line.Points.Add(new(200, 200));
        line.Points.Add(new(300, 200));
        line.Points.Add(new(400, 100));
        _objects.Add(line);

        BMWCurve curve = new();
        curve.Start = new(100, 100);
        curve[0] = new(200, 200);
        curve[1] = new(300, 200);
        curve[2] = new(400, 100);
        _objects.Add(curve);

        BMWRect rect = new();
        rect.Rect = new(400, 100, 600, 300);
        _objects.Add(rect);

        BMWCircle circle = new();
        circle.Center = new(500, 200);
        circle.Radius = 100;
        _objects.Add(circle);

        BMWOval oval1 = new();
        oval1.Oval = new(0, 200, 200, 300);
        _objects.Add(oval1);

        BMWOval oval2 = new();
        oval2.Oval = new(50, 150, 150, 350);
        _objects.Add(oval2);

        BMWText text = new();
        text.Text = "Hello, SkiaSharp!";
        text.Position = new(0, 100);
        _objects.Add(text);

        CanvasVM = new(_objects);
    }
}