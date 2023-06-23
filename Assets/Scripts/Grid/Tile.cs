using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    //private int m_tileNumber;
    //public int TileNumber { get => m_tileNumber; set => m_tileNumber = value; }

    public int x;
    public int y;

    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString() => x + "," + y;
}
