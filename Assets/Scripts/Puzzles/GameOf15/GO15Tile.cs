using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GO15Tile : Tile
{
    private int m_tileNumber;
    public int TileNumber { get => m_tileNumber; set => m_tileNumber = value; }


    public GO15Tile(int x, int y) : base(x, y) {  }


    
}
