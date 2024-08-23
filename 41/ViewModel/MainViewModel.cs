using Microsoft.Win32;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;
using System.IO;
using System.Reactive.Linq;
using System.Windows;

namespace BMWPaint;

public class MainViewModel : ReactiveObject
{
    public CanvasViewModel CanvasVM { get; init; }
    public MouseViewModel MouseVM { get; init; } = new();
    [Reactive] public TextInputViewModel TextInputVM { get; init; } = new();
    [Reactive] public ToolEnum? SelectTool { get; set; } = ToolEnum.Line;
    public IReactiveCommand ExportPngCommand { get; set; }
    public IReactiveCommand ExportSvgCommand { get; set; }
    public IReactiveCommand ExportPdfCommand { get; set; }

    private ToolEnum? _oldTool = ToolEnum.Line;
    private readonly ObservableList<IBMWObject> _objects = [];
    private readonly Dictionary<ToolEnum, ToolBase> _tools = [];
    public MainViewModel()
    {
        CanvasVM = new(_objects);

        _tools[ToolEnum.Line] = new LineTool(_objects);
        _tools[ToolEnum.Curve] = new CurveTool(_objects);
        _tools[ToolEnum.Rect] = new RectTool(_objects);
        _tools[ToolEnum.Circle] = new CircleTool(_objects);
        _tools[ToolEnum.Oval] = new OvalTool(_objects);
        _tools[ToolEnum.Text] = new TextTool(_objects, TextInputVM);
        _tools[ToolEnum.Image] = new ImageTool(_objects);

        ExportPngCommand = ReactiveCommand.Create(ExportPng);
        ExportSvgCommand = ReactiveCommand.Create(ExportSvg);
        ExportPdfCommand = ReactiveCommand.Create(ExportPdf);

        MouseVM.MoveEvent += MouseMove;
        MouseVM.LeftClickEvent += LeftClick;
        MouseVM.RightClickEvent += RightClick;

        this.WhenAnyValue(x => x.SelectTool).Subscribe(ToolChanged);
    }
    private bool MouseMove(Point pt)
    {
        if (SelectTool == null)
            return false;

        return _tools[SelectTool.Value].MouseMove(pt);
    }
    private bool LeftClick(Point pt)
    {
        if (SelectTool == null)
            return false;

        return _tools[SelectTool.Value].LeftClick(pt);
    }
    private bool RightClick(Point pt)
    {
        if (SelectTool == null)
            return false;

        return _tools[SelectTool.Value].RightClick(pt);
    }
    private void ToolChanged(ToolEnum? select)
    {
        if (_oldTool != null)
            _tools[_oldTool.Value].Quit();

        if (select == null)
        {
            _oldTool = select;
            return;
        }

        if (_tools[select.Value].Init() == false)
            SelectTool = _oldTool;
        else
            _oldTool = select;
    }
    private void ExportPng()
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "PNG Image (*.png)|*.png",
            FileName = "output.png"
        };

        if (saveFileDialog.ShowDialog() == false)
            return;

        CanvasViewModel vm = new(_objects);

        var info = new SKImageInfo((int)CanvasVM.ActualSize.Width, (int)CanvasVM.ActualSize.Height);
        using var bitmap = new SKBitmap(info);
        using var canvas = new SKCanvas(bitmap);

        vm.PaintSurface(canvas);

        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);

        File.WriteAllBytes(saveFileDialog.FileName, data.ToArray());
    }
    private void ExportSvg()
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "Scalable Vector Graphic (*.svg)|*.svg",
            FileName = "output.svg"
        };

        if (saveFileDialog.ShowDialog() == false)
            return;
        using var fs = File.OpenWrite(saveFileDialog.FileName);

        CanvasViewModel vm = new(_objects);

        using var svgCanvas = SKSvgCanvas.Create(new SKRect(0, 0, (float)CanvasVM.ActualSize.Width, (float)CanvasVM.ActualSize.Height), fs);

        vm.PaintSurface(svgCanvas);

        svgCanvas.Save();
    }
    private void ExportPdf()
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "Portable Document Format (*.pdf)|*.pdf",
            FileName = "output.pdf"
        };

        if (saveFileDialog.ShowDialog() == false)
            return;

        CanvasViewModel vm = new(_objects);

        using var pdfCanvas = SKDocument.CreatePdf(saveFileDialog.FileName);
        using var page = pdfCanvas.BeginPage((float)CanvasVM.ActualSize.Width, (float)CanvasVM.ActualSize.Height);

        vm.PaintSurface(page);

        pdfCanvas.EndPage();
        pdfCanvas.Close();
    }
}