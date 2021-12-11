using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct TileVariant{
    public TileBase tileBase;

    public int buildLayer;
    
    //height in noisemap
    [Range(0f,1f)]
    public float minHeight;

    [Range(0f,1f)]
    public float maxHeight;

    public TileVariantDetails[] tileVariantDetails;

}

[Serializable]
public struct TileVariantDetails{
    public TileBase tileBase;

    [Range(0f,1f)]
    public float minHeight;

    [Range(0f,1f)]
    public float maxHeight;
}

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Biome")]
public class _Biome : ScriptableObject
{
    public TileVariant[] tileVariants;
    public TileBase baseTile;
    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase treeTile;

    //altitude area
    [Range(0f,1f)]
    public float minAltitude;
    [Range(0f,1f)]
    public float maxAltitude;

    //humidity area
    [Range(0f,1f)]
    public float minHumidity;
    [Range(0f,1f)]
    public float maxHumidity;


}

