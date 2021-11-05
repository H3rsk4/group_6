using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tiles/Searchable")]
public class _Searchable : _Tile
{
    public GameObject droppedItem;
    public _Item item;
    public int amount;

    public _Tile replacedTile;
    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender){
        /*
        GameObject currentDroppedItem = Instantiate(droppedItem, tileManager.vertices[tileIndex * 4 + 0] + new Vector3(.5f,.5f), Quaternion.identity);
        //Debug.Log(name);
        if(currentDroppedItem.GetComponent<pickup_script>() != null){
            currentDroppedItem.GetComponent<pickup_script>().item = item;
            currentDroppedItem.GetComponent<pickup_script>().itemAmount = amount;
        }

        //replace tile
        //tileManager.ReplaceTile(tileIndex, replacedTile, 1);
        */
    }

}
