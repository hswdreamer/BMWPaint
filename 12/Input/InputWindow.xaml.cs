using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BMWPaint;
public partial class InputWindow : Window
{
    public InputWindow()
    {
        InitializeComponent();
    }
    public Point PT1;
    public Point PT2;
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        PT1 = new Point(double.Parse(X1.Text), double.Parse(Y1.Text));
        PT2 = new Point(double.Parse(X2.Text), double.Parse(Y2.Text));
        this.DialogResult = true;
    }
}
