using System.Windows;

namespace BMWPaint;

public class MouseViewModel
{
    public delegate bool MouseEventHandler(Point pt);
    public delegate bool MouseWheelEventHandler(Point pt, bool isUp);

    public event MouseEventHandler? LeftDownEvent = null;
    public event MouseEventHandler? LeftClickEvent = null;
    public event MouseEventHandler? LeftDragEvent = null;

    public event MouseEventHandler? RightDownEvent = null;
    public event MouseEventHandler? RightClickEvent = null;
    public event MouseEventHandler? RightDragEvent = null;

    public event MouseEventHandler? MiddleDownEvent = null;
    public event MouseEventHandler? MiddleClickEvent = null;
    public event MouseEventHandler? MiddleDragEvent = null;

    public event MouseEventHandler? MoveEvent = null;
    public event MouseEventHandler? EnterEvent = null;
    public event MouseEventHandler? LeaveEvent = null;

    public event MouseWheelEventHandler? WheelEvent = null;

    public bool RaisLeftDown(Point pt) => LeftDownEvent?.Invoke(pt) ?? false;
    public bool RaisLeftClick(Point pt) => LeftClickEvent?.Invoke(pt) ?? false;
    public bool RaisLeftDrag(Point pt) => LeftDragEvent?.Invoke(pt) ?? false;

    public bool RaisRightDown(Point pt) => RightDownEvent?.Invoke(pt) ?? false;
    public bool RaisRightClick(Point pt) => RightClickEvent?.Invoke(pt) ?? false;
    public bool RaisRightDrag(Point pt) => RightDragEvent?.Invoke(pt) ?? false;

    public bool RaisMiddleDown(Point pt) => MiddleDownEvent?.Invoke(pt) ?? false;
    public bool RaisMiddleClick(Point pt) => MiddleClickEvent?.Invoke(pt) ?? false;
    public bool RaisMiddleDrag(Point pt) => MiddleDragEvent?.Invoke(pt) ?? false;

    public bool RaisMove(Point pt) => MoveEvent?.Invoke(pt) ?? false;
    public bool RaisEnter(Point pt) => EnterEvent?.Invoke(pt) ?? false;
    public bool RaisLeave(Point pt) => LeaveEvent?.Invoke(pt) ?? false;

    public bool RaisWheel(Point pt, bool isUp) => WheelEvent?.Invoke(pt, isUp) ?? false;
}