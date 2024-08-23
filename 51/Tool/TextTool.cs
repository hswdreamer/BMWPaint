using ReactiveUI;
using System.Windows;

namespace BMWPaint;

public class TextTool : ToolBase
{
    private TextInputViewModel _textInputVM;
    public TextTool(ObservableList<IBMWObject> objects, MatrixViewModel matrixVM, TextInputViewModel textInputVM) : base(objects, matrixVM)
    {
        _textInputVM = textInputVM;
        _textInputVM.WhenAnyValue(x => x.Visibility).Subscribe(VisibleChanged);
    }
    private void VisibleChanged(Visibility visibility)
    {
        if (visibility == Visibility.Hidden && string.IsNullOrWhiteSpace(_textInputVM.Text) == false)
        {
            var margin = 3 * MatrixVM.Scale;
            var fontSize = _textInputVM.FontSize * MatrixVM.Scale;
            
            var pt = new Point(_textInputVM.Left + margin, _textInputVM.Top + _textInputVM.FontSize + margin);
            BMWText text = new();
            text.Text = _textInputVM.Text;
            text.Position = MatrixVM.LogicalPoint(pt);
            Objects.Add(text);
        }
    }
    public override bool Init()
    {
        _textInputVM.Text = string.Empty;
        _textInputVM.Visibility = Visibility.Collapsed;
        
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
            _textInputVM.FontSize = 20 * MatrixVM.Scale;
            _textInputVM.Text = string.Empty;
            _textInputVM.Left = pt.X;
            _textInputVM.Top = pt.Y;
            _textInputVM.Visibility = Visibility.Visible;
        }

        return true;
    }
}