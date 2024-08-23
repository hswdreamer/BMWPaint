using ReactiveUI;
using SkiaSharp.Views.WPF;
using System.Collections.ObjectModel;

namespace BMWPaint;

public class MainViewModel
{
    public CanvasViewModel CanvasVM { get; init; }
    public IReactiveCommand InputCommand { get; init; }

    private ObservableCollection<IBMWObject> _objects = [];
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
    }
}