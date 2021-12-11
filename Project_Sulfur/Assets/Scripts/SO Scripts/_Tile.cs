using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

 [Serializable]
public struct ItemDrop{
    public _Item item;
    [Range(1,999)]
    public int Amount;
}

[Serializable]
public class Rules{
    public TileBase tile;

    public int[] ruleSet;

}



[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tiles/Tile")]
public class _Tile : Tilebase
{
    public new string name;
    public int buildLayer;
    public int layer;

    public Sprite icon;
    
    public bool isWalkable;
    public bool needsUpdate;
    public bool isDemolishable;
    public int structureHealth;

    public ItemDrop[] itemDrops;
    
    public _Item demolishItem;
    public bool isInteractable;
    public bool hasContainer;
    public int containerSize;

    //tiles in a tilemap that represent this scriptable object
    public TileBase[] tiles;

    public bool hasMultipleTiles;
    public TileBase[] tileParts;
    public Vector3Int[] tilePartPositions;

    //ruletile stuff
    public bool isRuleTile;
    public Rules[] tileRules;



    //public Vector2[] uvs = new Vector2[4];

    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender){
        
        //Debug.Log(name);
    }

    

}
