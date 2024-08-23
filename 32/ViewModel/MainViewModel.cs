using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using System.Windows;

namespace BMWPaint;

public class MainViewModel : ReactiveObject
{
    public CanvasViewModel CanvasVM { get; init; }
    public MouseViewModel MouseVM { get; init; } = new();
    [Reactive] public TextInputViewModel TextInputVM { get; init; } = new();
    [Reactive] public ToolEnum? SelectTool { get; set; } = ToolEnum.Line;
    
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
}