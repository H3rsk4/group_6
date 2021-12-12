using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProximityTile", menuName = "ScriptableObjects/Tiles/ProximityTile")]
public class _ProximityTile : _Tile
{
    public _CraftingRecipe[] providedRecipes;

    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender){
        Vector3 worldPos = tileManager.maps[1].CellToWorld(tilePosition);
        Collider2D[] entityColliders = Physics2D.OverlapCircleAll(worldPos + new Vector3(.5f,.5f), 2f);
        bool playerFound = false;
        for(int i = 0; i < entityColliders.Length; i++){
            if(entityColliders[i].transform.GetComponent<player_proximity>() != null){
                //this is the player
                if(!player_proximity.proximityTiles.Contains(this)){
                    player_proximity.proximityTiles.Add(this);
                }
                playerFound = true;
                
            }
        }
        if(!playerFound){
            player_proximity.proximityTiles.Remove(this);
        }
    }
}
