using System.Windows.Controls.Ribbon;

namespace BMWPaint;

public partial class MainWindow : RibbonWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Log.Buffer.CanRead += LogCanRead;
    }

    private void LogCanRead(object? sender, string log)
    {
        LogViewer.AppendText(log + Environment.NewLine);
        LogViewer.ScrollToEnd();
    }
}