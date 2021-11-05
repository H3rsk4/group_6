using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Transporter", menuName = "ScriptableObjects/Tiles/Transporter")]
public class _Transporter : _Tile
{
    public int ticksToTransport;

    public GameObject droppedItem;

    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender){
        /*
        if((string)sender == "entity_updater"){
            //Debug.Log("the sender was entity updater");
            if(tileManager.entityUpdater.saveValues[entityIndex] < 1){
                //this is too slow?
                int containerIndex = 0;
                for(int i = 0; i < tileManager.invManager.motherInventory.childInventories.Count; i++){
                    if(tileManager.invManager.motherInventory.childInventories[i].tileIndex == tileIndex){
                        containerIndex = i;

                        if(tileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item != null){
                            //update uv
                            tileManager.entityUpdater.saveValues[entityIndex]++;
                            tileManager.UpdateUV(tileIndex,0,-1);
                        } else {
                            tileManager.UpdateUV(tileIndex,0,0);
                        }
                    }
                    
                }
                
            }else{
                tileManager.entityUpdater.saveValues[entityIndex]++;
            }
            

            if(tileManager.entityUpdater.saveValues[entityIndex] >= ticksToTransport){
                
                //transport

                //find correct index of the containerlist
                
                int containerIndex = 0;
                for(int i = 0; i < tileManager.invManager.motherInventory.childInventories.Count; i++){
                    if(tileManager.invManager.motherInventory.childInventories[i].tileIndex == tileIndex){
                        containerIndex = i;
                    }
                }

                if(tileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item != null){
                

                    Vector3 orientationOffset = new Vector3(0,0);
                    //move the item
                    //check tilemanager
                    //check tile
                    
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

                            if(currentTileManager.tileSO[newTileIndex].hasContainer){
                                int newContainerIndex = 0;
                                for(int i = 0; i < currentTileManager.invManager.motherInventory.childInventories.Count; i++){
                                    if(currentTileManager.invManager.motherInventory.childInventories[i].tileIndex == newTileIndex){
                                        newContainerIndex = i;
                                    }
                                }
                                if(currentTileManager.invManager.motherInventory.childInventories[newContainerIndex].containers[0].item == null){
                                    currentTileManager.invManager.motherInventory.childInventories[newContainerIndex].containers[0].item = 
                                    tileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item;
                                    tileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item = null;

                                    //change sprite?
                                    _Transporter testTransport = currentTileManager.tileSO[newTileIndex] as _Transporter;
                                    if(testTransport != null){
                                        currentTileManager.UpdateUV(newTileIndex,0,-1);
                                    }
                                    tileManager.UpdateUV(tileIndex,0,0);
                                    
                                }
                            }else{
                                //if no next container to transport

                                if(currentTileManager.tileSO[newTileIndex].isWalkable){
                                    GameObject currentDroppedItem = Instantiate(droppedItem, tileManager.vertices[tileIndex * 4 + 0] + orientationOffset + new Vector3(.5f,.5f) ,Quaternion.identity);
                                    currentDroppedItem.GetComponent<pickup_script>().item = tileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item;
                                    currentDroppedItem.GetComponent<pickup_script>().itemAmount = 1;
                                    tileManager.invManager.motherInventory.childInventories[containerIndex].containers[0].item = null;
                                }else{
                                    tileManager.entityUpdater.saveValues[entityIndex] = 0;
                                }
                            }

                            
                        }
                    }
                }
                
                
                //tileManager.UpdateUV(tileIndex,0,0);
                tileManager.entityUpdater.saveValues[entityIndex] = 0;
                
            }
        }else{
            //Debug.Log("sender was someone else");
            //interacting with transporter
            //show UI
            int containerIndex = 0;
            for(int i = 0; i < tileManager.invManager.motherInventory.childInventories.Count; i++){
                if(tileManager.invManager.motherInventory.childInventories[i].tileIndex == tileIndex){
                    containerIndex = i;
                }
            }

            container_script.instance.isContainer = true;
            container_script.instance.SetUpContainer("Transport Belt",tileManager.invManager,containerIndex);
            
        }
        */
    }
    
}
