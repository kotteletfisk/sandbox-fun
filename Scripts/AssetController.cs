using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AssetController : MonoBehaviour
{
    private static AssetController _instance;

    public static AssetController Instance
    {
        get
        {
            if (_instance == null)
            {
               _instance = (Instantiate(Resources.Load("AssetController")) as GameObject)
                            .GetComponent<AssetController>();
            }
            return _instance;
        }
    }

    public Tile sandTile;
    public Tile concreteTile;
}
