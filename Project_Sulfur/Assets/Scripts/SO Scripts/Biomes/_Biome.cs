using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Biome")]
public class _Biome : ScriptableObject
{
    public TileBase baseTile;
    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase treeTile;

}

