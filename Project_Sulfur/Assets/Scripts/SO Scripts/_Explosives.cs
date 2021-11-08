using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosive", menuName = "ScriptableObjects/Tiles/Explosive")]
public class _Explosives : _Tile
{
    public int ticksToExplode;

    public GameObject explosion;
    public GameObject nugget;

    public _Tile destroys;
    public _Tile replacedWith;

    private Quaternion myRotation = Quaternion.identity;
    
    
    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender)
    {
        
        //myRotation.eulerAngles = new Vector3(-90,0,0);
            
        
        tileManager.entityUpdater.saveValues[entityIndex]++; 

        //Debug.Log(tileManager.entityUpdater.saveValues[entityIndex]);
        if(tileManager.entityUpdater.saveValues[entityIndex] >= ticksToExplode){
            //Debug.Log("Explosion");
            //activate correct tilemanager
            tile_manager currentTileManager = tileManager.CheckTileManager(tilePosition + new Vector3Int(1,0,0));
            //check right of tnt position if wall and replace it
            Debug.Log(currentTileManager.transform.position);
            if(currentTileManager != null){
                _Tile rightTile = tile_dictionary.GetTileSO(tilePosition + new Vector3Int(1,0,0), currentTileManager.maps[1]);
                    //maybe destructible bool in SO??
                    //Debug.Log(rightTile + " " + (tilePosition + new Vector3Int(1,0,0)));
                    if(rightTile == destroys){
                        //we found wall -> destroy it
                        
                        currentTileManager.ReplaceTile(tilePosition + new Vector3Int(1,0,0), null, currentTileManager.maps[1]);
                        if(nugget != null){
                            Instantiate(nugget, tilePosition, Quaternion.identity);
                        }
                        
                    }
                
            }
            
            //remove thing
            tileManager.entityUpdater.removeList.Add(tilePosition);
            
            //tick_system.OnTick -= tileManager.entityUpdater.UpdateEntitys;
            tileManager.ReplaceTile(tilePosition, null, tileManager.maps[1]);
            //tick_system.OnTick -= this;
            tileManager.entityUpdater.valueRemoveList.Add(tileManager.entityUpdater.saveValues[entityIndex]);
            
            
            Instantiate(explosion, tilePosition, Quaternion.identity);
        }
        /*
        if(!setTick){
            currentTick = 0;
            setTick = true;
        }
        */
        //transform.position = transform.forward * 1f;
        //kaboom
        //Debug.Log(tileManager.vertices[tileIndex * 4 + 0]);
        //Debug.Log(tileManager.vertices[tileIndex * 4 + 0]);
        //tileManager.vertices[tileIndex * 4 + 0] = new Vector3(6,6);
        //tileManager.vertices[tileIndex * 4 + 1] = new Vector3(6,7); 
        //tileManager.vertices[tileIndex * 4 + 2] = new Vector3(7,7); 
        //tileManager.vertices[tileIndex * 4 + 3] = new Vector3(7,6); 
        //Debug.Log(tileManager.vertices[tileIndex * 4 + 0]); 
        
        //tileManager.GetComponent<MeshFilter>().mesh.vertices = tileManager.vertices.ToArray();
    }
}
