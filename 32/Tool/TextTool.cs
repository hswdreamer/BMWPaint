using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp.Views.WPF;
using System.Windows;

namespace BMWPaint;

public class TextTool : ToolBase
{
    private TextInputViewModel _textInputVM;
    public TextTool(ObservableList<IBMWObject> objects, TextInputViewModel textInputVM) : base(objects)
    {
        _textInputVM = textInputVM;
        _textInputVM.WhenAnyValue(x => x.Visibility).Subscribe(VisibleChanged);
    }
    private void VisibleChanged(Visibility visibility)
    {
        if (visibility == Visibility.Hidden && string.IsNullOrWhiteSpace(_textInputVM.Text) == false)
        {
            double margin = 3;
            var pt = new Point(_textInputVM.Left + margin, _textInputVM.Top + _textInputVM.FontSize + margin);
            BMWText text = new();
            text.Text = _textInputVM.Text;
            text.Position = pt.ToSKPoint();
            Objects.Add(text);
        }
    }
    public override bool Init()
    {
        _textInputVM.Text = string.Empty;
        _textInputVM.Visibility = Visibility.Collapsed;
        _textInputVM.FontSize = 20;
        return true;
    }
    public override void Quit()
    {
        _textInputVM.Text = string.Empty;
        _textInputVM.Visibility = Visibility.Hidden;
    }
    public override bool LeftClick(Point pt)
    {
        if (_textInputVM.Visibility == Visibility.Visible)
        {
            _textInputVM.Text = string.Empty;
            _textInputVM.Visibility = Visibility.Hidden;
        }
        else
        {
            _textInputVM.Text = string.Empty;
            _textInputVM.Left = pt.X;
            _textInputVM.Top = pt.Y;
            _textInputVM.Visibility = Visibility.Visible;
        }

        return true;
    }
}