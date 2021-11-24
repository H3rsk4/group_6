using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity_updater : MonoBehaviour
{
    //list of tileIndexes that are unique and sent to SO Do() void
    public List<Vector3Int> tilePositions = new List<Vector3Int>();
    public List<Vector3Int> removeList = new List<Vector3Int>();

    public List<int> saveValues = new List<int>();
    public List<int> valueRemoveList = new List<int>();

    public check_chunks chunkChecker;

    public tile_manager tileManager;

    private bool isSubscribed = false;

    private bool isUpdated;


    void Awake(){
        tileManager = GetComponent<tile_manager>();
    }

    void Update()
    {

        //maybe add here a way to unsubscribe from chunkChecker once out of range
        
        if(tilePositions.Count > 0){
            
            if(!isSubscribed){
                tick_system.OnTick += UpdateEntitys;
                isSubscribed = true;
            }
            
        } else {
            if(isSubscribed){
                tick_system.OnTick -= UpdateEntitys;
                isSubscribed = false;
            }
            
        }
        
    }

    public void UpdateEntitys(){
        //Debug.Log("tick");
        int entityIndex = 0;
        foreach(Vector3Int tilePosition in tilePositions){
            _Tile currentTileSO = tile_dictionary.GetTileSO(tilePosition, tileManager.maps[1]);
            if(currentTileSO != null){
                currentTileSO.Do(tilePosition, tileManager, entityIndex, "entity_updater");
            }
            entityIndex++;
        }
        foreach(Vector3Int toRemove in removeList){
            tilePositions.Remove(toRemove);
        }
        removeList.Clear();
        foreach(int toRemove in valueRemoveList){
            saveValues.Remove(toRemove);
        }
        valueRemoveList.Clear();


    }
}
