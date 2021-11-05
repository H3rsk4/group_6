using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Miner", menuName = "ScriptableObjects/Tiles/Miner")]
public class _Miner : _Tile
{
    public int ticksToMine;


    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender){
        /*
        //Debug.Log(name);
        if((string)sender == "entity_updater"){
            tileManager.entityUpdater.saveValues[entityIndex]++;
            if(tileManager.entityUpdater.saveValues[entityIndex] >= ticksToMine){
                //mine and move the material

                //check orientation
                Vector3 orientationOffset = new Vector3(0,0);
                    
                    //check the right
                    switch(tileManager.orientation[tileIndex]){
                        case 0:
                            //pointing up
                            orientationOffset = new Vector3(0,1);
                            break;
                        case 1:
                            //pointing right
                            orientationOffset = new Vector3(1,0);
                            break;
                        case 2:
                            //pointing down
                            orientationOffset = new Vector3(0,-1);
                            break;
                        case 3:
                            //pointing left
                            orientationOffset = new Vector3(-1,0);
                            break;
                    }
                tile_manager currentTileManager = tileManager.CheckTileManager(tileManager.vertices[tileIndex * 4 + 0] + orientationOffset);
                if(currentTileManager != null){
                    if(currentTileManager.CheckTile(tileManager.vertices[tileIndex * 4 + 0] + orientationOffset)){
                        int newTileIndex = currentTileManager.vertexIndex;
                        //_Vein checkedTile = (_Vein)currentTileManager.tileSO[newTileIndex];
                        _Vein checkedTile = currentTileManager.tileSO[newTileIndex] as _Vein;
                        if(checkedTile != null){
                            if(checkedTile.yields != null){
                                
                                //there is an vein -> check opposite and mine item
                                tile_manager oppositeTileManager = tileManager.CheckTileManager(tileManager.vertices[tileIndex * 4 + 0] - orientationOffset);
                                if(oppositeTileManager != null){
                                    if(oppositeTileManager.CheckTile(tileManager.vertices[tileIndex * 4 + 0] - orientationOffset)){
                                        int oppositeTileIndex = oppositeTileManager.vertexIndex;
                                        if(oppositeTileManager.tileSO[oppositeTileIndex].hasContainer){
                                            //Debug.Log("hey there");
                                            int containerIndex = 0;
                                            for(int i = 0; i < oppositeTileManager.invManager.motherInventory.childInventories.Count; i++){
                                                if(oppositeTileManager.invManager.motherInventory.childInventories[i].tileIndex == oppositeTileIndex){
                                                    containerIndex = i;
                                                }
                                            }
                                            if(oppositeTileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item == null){
                                                oppositeTileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item = checkedTile.yields;
                                            }else{
                                                tileManager.entityUpdater.saveValues[entityIndex] = ticksToMine;
                                                return;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
                tileManager.entityUpdater.saveValues[entityIndex] = 0;
            }
        }
        */
    }
}
