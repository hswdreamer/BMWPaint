using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows;

namespace BMWPaint;
public class TextInputViewModel : ReactiveObject
{
    [Reactive] public double Left { get; set; }
    [Reactive] public double Top { get; set; }
    [Reactive] public double FontSize { get; set; }
    [Reactive] public string Text { get; set; } = "";
    [Reactive] public Visibility Visibility { get; set; } = Visibility.Hidden;
}