using ReactiveUI;
using SkiaSharp.Views.WPF;

namespace BMWPaint;

public class MainViewModel
{
    public CanvasViewModel CanvasVM { get; init; }
    public IReactiveCommand InputCommand { get; init; }
    private List<IBMWObject> _objects = [];
    public MainViewModel()
    {
        InputCommand = ReactiveCommand.Create(Input);
        CanvasVM = new(_objects);
    }

    private void Input()
    {
        InputWindow input = new();
        if (input.ShowDialog() == false)
            return;

        BMWLine line = new();
        line.Points.Add(input.PT1.ToSKPoint());
        line.Points.Add(input.PT2.ToSKPoint());

        _objects.Add(line);
        CanvasVM.Refresh();
    }
}