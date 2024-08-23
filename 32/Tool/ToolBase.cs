using ReactiveUI;
using System.Windows;

namespace BMWPaint;

public abstract class ToolBase(ObservableList<IBMWObject> objects) : ReactiveObject
{
    protected ObservableList<IBMWObject> Objects = objects;
    public virtual bool Init() { return true; }
    public virtual void Quit() { }

    public virtual bool LeftClick(Point pt) { return false; }
    public virtual bool RightClick(Point pt) { return false; }
    public virtual bool MouseMove(Point pt) { return false; }
}