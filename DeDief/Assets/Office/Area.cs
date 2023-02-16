using System.Collections.Generic;
using UnityEngine;

public class Area : System.IComparable<Area>
{
    public float Left { get; }
    public float Top { get; }
    public float Right { get; }
    public float Bottom { get; }
    public List<Area> Doors;
    public RoomType roomType;

    public Area(float left, float top, float right, float bottom)
    {
        this.Doors = new List<Area>();
        if (right >= left)
        {
            this.Left = left;
            this.Right = right;
        }
        else
        {
            throw new System.Exception();
        }
        if (bottom >= top)
        {
            this.Top = top;
            this.Bottom = bottom;
        }
        else
        {
            throw new System.Exception();
        }
    }

    public double GetArea()
    {
        double area = GetWidth() * GetLength();
        return area;
    }

    public double GetWidth()
    {
        return this.Right - this.Left;
    }

    public double GetLength()
    {
        return this.Bottom - this.Top;
    }

    private float getSplitX()
    {
        float x = Random.Range((float)(GetWidth() / 5), (float)((GetWidth() - 1) / 5 * 4));
        return x;
    }

    private float getSplitZ()
    {
        float y = Random.Range((float)(GetLength() / 5), (float)((GetLength() - 1) / 5 * 4));
        return y;
    }

    public (Area, Area) SplitTwo()
    {
        if (GetWidth() > GetLength())
        {
            return SplitTwoX();
        }
        else
        {
            return SplitTwoZ();
        }
    }

    public (Area, Area) SplitTwoX()
    {
        float x = getSplitX();
        Area a = new Area(this.Left, this.Top, this.Left + x, this.Bottom);
        Area b = new Area(this.Left + x, this.Top, this.Right, this.Bottom);
        return (a, b);
    }

    public (Area, Area) SplitTwoZ()
    {
        float y = getSplitZ();
        Area a = new Area(this.Left, this.Top, this.Right, this.Top + y);
        Area b = new Area(this.Left, this.Top + y, this.Right, this.Bottom);
        return (a, b);
    }

    public (Area, Area, Area) SplitThree(float size)
    {
        if (GetWidth() > GetLength())
        {
            return SplitThreeX(size);
        }
        else
        {
            return SplitThreeZ(size);
        }
    }

    public (Area, Area, Area) SplitThreeX(float size)
    {
        float x = getSplitX();
        float halfSize = size / 2;
        Area a = new Area(this.Left, this.Top, this.Left + x - halfSize, this.Bottom);
        Area b = new Area(this.Left + x - halfSize, this.Top, this.Left + x + halfSize, this.Bottom);
        Area c = new Area(this.Left + x + halfSize, this.Top, this.Right, this.Bottom);
        return (a, b, c);
    }

    public (Area, Area, Area) SplitThreeZ(float size)
    {
        float y = getSplitZ();
        float halfSize = size / 2;
        Area a = new Area(this.Left, this.Top, this.Right, this.Top + y - halfSize);
        Area b = new Area(this.Left, this.Top + y - halfSize, this.Right, this.Top + y + halfSize);
        Area c = new Area(this.Left, this.Top + y + halfSize, this.Right, this.Bottom);
        return (a, b, c);
    }

    public bool IsTouching(Area area)
    {
        bool widthIsPositive = Mathf.Min(Right, area.Right) >= Mathf.Max(Left, area.Left);
        bool heightIsPositive = Mathf.Min(Bottom, area.Bottom) >= Mathf.Max(Top, area.Top);
        return (widthIsPositive && heightIsPositive);
    }

    public void addDoor(Area door)
    {
        Doors.Add(door);
    }

    public static void createDoorBetween(Area area1, Area area2)
    {
        float x1 = Mathf.Max(area1.Left, area2.Left);
        float y1 = Mathf.Max(area1.Top, area2.Top);
        float x2 = Mathf.Min(area1.Right, area2.Right);
        float y2 = Mathf.Min(area1.Bottom, area2.Bottom);
        Area door = new Area(x1, y1, x2, y2);
        area1.addDoor(door);
        area2.addDoor(door);
    }

    public int CompareTo(Area other)
    {
        if (other == null)
        {
            return 1;
        }
        return this.GetArea().CompareTo(other.GetArea());
    }
}