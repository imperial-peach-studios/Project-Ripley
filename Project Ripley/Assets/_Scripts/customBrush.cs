using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
namespace UnityEditor
{
    //[CreateAssetMenu]
    //[CustomGridBrush(false, true, false, "Per Brush")]
    //public class customBrush : GridBrush
    //{
    //    public List<GameObject> prefabs;
    //    private Tile temp;
    //    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    //    {
    //        //Debug.Log(cells[0].tile);
    //        if (cells[0].tile != null)
    //        {
    //            temp = (Tile)cells[0].tile;
    //            foreach (GameObject prefab in prefabs)
    //            {
    //                if (temp.sprite == prefab.GetComponent<SpriteRenderer>().sprite)
    //                {
    //                    if (GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, 0)) != null)
    //                    {
    //                        return;
    //                    }
    //                    Vector3Int cellPosition = new Vector3Int(position.x, position.y, 0);
    //                    Vector3 centerTile;
    //                    centerTile = new Vector3(0.5f, 0.5f, 0f);
    //                    GameObject ob;
    //                    ob = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

    //                    ob.transform.SetParent(brushTarget.transform);
    //                    ob.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(cellPosition + centerTile));
    //                }
    //            }
    //        }
    //    }
    //    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    //    {
    //        Transform selected = GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, 0));
    //        //Debug.Log(selected);
    //        if (selected != null)
    //        {
    //            DestroyImmediate(selected.gameObject);
    //        }
    //    }

    //    private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
    //    {
    //        int childCount = parent.childCount;

    //        Vector3 min = grid.LocalToWorld(grid.CellToLocalInterpolated(position));

    //        Vector3 max = grid.LocalToWorld(grid.CellToLocalInterpolated(position + Vector3Int.one));

    //        Bounds bounds = new Bounds((min + max) * 0.5f, max - min);

    //        for (int i = 0; i < childCount; i++)
    //        {
    //            Transform child = parent.GetChild(i);
    //            if (bounds.Contains(child.position))
    //            {
    //                return child;
    //            }
    //        }
    //        return null;
    //    }
    //}

}
