using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Element : ScriptableObject
{
    public Vector3Int pos;
    public abstract Element UpdatePos(Tilemap grid);
}
