using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap grid;

    public int destroyLimit = -100;

    public int numParticles = 0;

    public int brushSize = 1;
    private List<Element> liveElements;

    public void Awake()
    {
        liveElements = new List<Element>();
    }

    public void Update()
    {
        SpawnElementifClicked();
    }

    public void FixedUpdate()
    {
        grid.ClearAllTiles();

        // Sort live elements list based on grid position
        liveElements = liveElements.OrderBy(e => e.pos.y).ThenBy(e => e.pos.x).ToList();

        foreach (Element particle in liveElements.ToArray())
        {
            liveElements.Remove(particle);
            Element newParticle = particle.UpdatePos(grid);

            if (newParticle.pos.y < destroyLimit)
                continue;

            liveElements.Add(newParticle);

            if (newParticle is Concrete)
                grid.SetTile(newParticle.pos, AssetController.Instance.concreteTile);

            else if (newParticle is Sand)
                grid.SetTile(newParticle.pos, AssetController.Instance.sandTile);
        }
        numParticles = liveElements.Count;
    }

    private void SpawnElementifClicked()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3Int mousePos = GetMousePos();

            if (!IsPosEmpty(mousePos))
                return;

            SpawnElement<Concrete>(mousePos);
        }


        if (Input.GetMouseButton(0))
        {
            Vector3Int mousePos = GetMousePos();

            if (!IsPosEmpty(mousePos))
                return;

            SpawnElement<Sand>(mousePos);
        }
    }


    private Vector3Int GetMousePos()
    {
        UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mousePos);
    }

    private bool IsPosEmpty(Vector3Int pos)
    {
        return grid.GetTile(pos) == null;
    }

    private void SpawnElement<T>(Vector3Int pos) where T : Element
    {
        if (brushSize < 2)
        {
            T newEl = ScriptableObject.CreateInstance<T>();
            newEl.pos = pos;
            liveElements.Add(newEl);
            return;
        }

        for (int x = -brushSize; x <= brushSize; x++)
        {
            for (int y = -brushSize; y <= brushSize; y++)
            {
                Vector3Int newPos = pos + new Vector3Int(x, y, 0);
                if (IsPosEmpty(newPos))
                {
                    T newEl = ScriptableObject.CreateInstance<T>();
                    newEl.pos = newPos;
                    liveElements.Add(newEl);
                }
            }
        }
    }
}
