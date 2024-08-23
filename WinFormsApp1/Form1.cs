namespace WinFormsApp1;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
        var pen = new Pen(new SolidBrush(Color.Black));
        var g = e.Graphics;
        g.Clear(Color.White);
        g.DrawLine(pen, 100, 100, 200, 200);
    }
}
