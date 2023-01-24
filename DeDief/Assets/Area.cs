using UnityEngine;

public class Area : System.IComparable<Area>
{
    public double Left { get; }
    public double Top { get; }
    public double Right { get; }
    public double Bottom { get; }

    public Area(double left, double top, double right, double bottom)
    {
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
        double area = GetWidth() * GetHeight();
        return area;
    }

    public double GetWidth()
    {
        return this.Right - this.Left;
    }

    public double GetHeight()
    {
        return this.Bottom - this.Top;
    }

    public (Area, Area) SplitTwo()
    {
        if (GetWidth() > GetHeight())
        {
            return SplitTwoX();
        }
        else
        {
            return SplitTwoY();
        }
    }

    public (Area, Area) SplitTwoX()
    {
        float x = Random.Range(1, (float)(GetWidth() - 1));
        Area a = new Area(this.Left, this.Top, this.Left + x, this.Bottom);
        Area b = new Area(this.Left + x, this.Top, this.Right, this.Bottom);
        return (a, b);
    }

    public (Area, Area) SplitTwoY()
    {
        float y = Random.Range(1, (float)(GetHeight() - 1));
        Area a = new Area(this.Left, this.Top, this.Right, this.Top + y);
        Area b = new Area(this.Left, this.Top + y, this.Right, this.Bottom);
        return (a, b);
    }

    public (Area, Area, Area) SplitThree()
    {
        if (GetWidth() > GetHeight())
        {
            return SplitThreeX();
        }
        else
        {
            return SplitThreeY();
        }
    }

    public (Area, Area, Area) SplitThreeX()
    {
        float x = Random.Range(1, (float)(GetWidth() - 1));
        Area a = new Area(this.Left, this.Top, this.Left + x - 0.5, this.Bottom);
        Area b = new Area(this.Left + x - 0.5, this.Top, this.Left + x + 0.5, this.Bottom);
        Area c = new Area(this.Left + x + 0.5, this.Top, this.Right, this.Bottom);
        return (a, b, c);
    }

    

    public (Area, Area, Area) SplitThreeY()
    {
        float y = Random.Range(1, (float)(GetHeight() - 1));
        Area a = new Area(this.Left, this.Top, this.Right, this.Top + y - 0.5);
        Area b = new Area(this.Left, this.Top + y - 0.5, this.Right, this.Top + y - 0.5);
        Area c = new Area(this.Left, this.Top + y + 0.5, this.Right, this.Bottom);
        return (a, b, c);
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