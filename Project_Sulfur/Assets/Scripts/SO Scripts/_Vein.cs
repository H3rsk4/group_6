using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vein", menuName = "ScriptableObjects/Tiles/Vein")]
public class _Vein : _Tile
{
    public _Item yields;

    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender){
        
        //Debug.Log(name);
    }
}
