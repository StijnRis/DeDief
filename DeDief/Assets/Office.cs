using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office
{
    string[,] grid;

    public Office(int width, int length)
    {
        this.grid = new string[width, length];
    }

    public int getWidth()
    {
        return grid.GetLength(1);
    }

    public int getLength()
    {
        return grid.GetLength(0);
    }

    public int getAmountOfCells()
    {
        return getLength() * getWidth();
    }

    public string? getCell(int x, int y)
    {
        if (x < 0 || x >= getWidth() || y < 0 || y >= getLength())
        {
            return null;
        }
        return grid[y, x];
    }

    public void setCell(int x, int y, string value)
    {
        if (x < 0 || x >= getWidth() || y < 0 || y >= getLength())
        {
            return;
        }
        grid[y, x] = value;
    }
}
