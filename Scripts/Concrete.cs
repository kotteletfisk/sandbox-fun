using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static ElementType;

public class Concrete : Element
{
    public Concrete(Vector3Int pos)
    {
        this.pos = pos;
    }

    public override Element UpdatePos(Tilemap grid)
    {
        return this;
    }
}
