using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;
using static ElementType;

public class Sand : Element
{
    public Sand(Vector3Int pos)
    {
        this.pos = pos;
    }

    public override Element UpdatePos(Tilemap grid)
    {
        if (grid.GetTile(this.pos + Vector3Int.down) == null)
        {
            this.pos = pos + new Vector3Int(0, -1, 0);
        }

        else if (grid.GetTile(this.pos + new Vector3Int(1, -1, 0)) == null)
        {
            this.pos = pos + new Vector3Int(1, -1, 0);
        }

        else if (grid.GetTile(this.pos + new Vector3Int(-1, -1, 0)) == null)
        {
            this.pos = pos + new Vector3Int(-1, -1, 0);
        }

        return this;
    }
}
