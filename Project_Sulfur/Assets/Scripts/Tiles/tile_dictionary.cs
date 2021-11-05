using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tile_dictionary : MonoBehaviour
{
    [SerializeField]
    private List<_Tile> tileScriptableObjects;

    static Dictionary<TileBase, _Tile> SOFromTiles;

    private void Awake(){
        SOFromTiles = new Dictionary<TileBase, _Tile>();

        foreach(var currentTileSO in tileScriptableObjects){
            foreach(var tile in currentTileSO.tiles){
                SOFromTiles.Add(tile, currentTileSO);
            }
        }
    }

    public static _Tile GetTileSO(Vector3Int tilePosition, Tilemap currentTilemap){
        TileBase tile = currentTilemap.GetTile(tilePosition);

        if(tile == null){
            return null;
        }else{
            return SOFromTiles[tile];
        }
    }
}
