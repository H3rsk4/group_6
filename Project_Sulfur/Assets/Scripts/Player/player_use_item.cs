using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_use_item : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousePos;
    private Vector3Int flooredMousePos;
    public hotbar_script hotbar;

    private tile_manager currentTileManager;

    private Vector3 flooredPos = new Vector3(0,0,0);

    public LayerMask chunkMask;

    private Vector3Int cellMousePosition;


    public GameObject actionPrefab;
    public LayerMask actionMask;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //getting mouse position
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //calculating floored mouse position
        flooredMousePos = new Vector3Int((int)Mathf.Floor(mousePos.x), (int)Mathf.Floor(mousePos.y), 0);


        //getting correct tilemanager for that mouse position
        Vector3 currentFlooredPos = new Vector3(Mathf.Round(mousePos.x / 10 + .01f) * 10,Mathf.Round(mousePos.y / 10 + .01f)  * 10,0);
        if(flooredPos != currentFlooredPos){
            flooredPos = currentFlooredPos;
            GetTileManager();
        }

        if(currentTileManager != null){
            //calculating cell position
            cellMousePosition = currentTileManager.maps[1].WorldToCell(mousePos);
        }

        if(Input.GetMouseButton(0)){
            if(hotbar.selectedItem != null){
                if(hotbar.selectedItem.tile != null){
                    //placing tile
                    Building();
                }else{
                    //using something else
                    Attacking();
                }
            }
        }

    }

    private void GetTileManager(){
        Collider2D managerCollider = Physics2D.OverlapCircle(flooredPos, 1, chunkMask);
        if(managerCollider != null){
            currentTileManager = managerCollider.transform.GetComponent<tile_manager>();
        }
    }

    private void Building(){
        _Tile currentTileSO = tile_dictionary.GetTileSO(cellMousePosition, currentTileManager.maps[1]);
        _Tile bottomTileSO = tile_dictionary.GetTileSO(cellMousePosition, currentTileManager.maps[0]);

        // no tile in the same spot
        if(currentTileSO == null){
            
            // if placing succeeds
            if(currentTileManager.PlaceTile(bottomTileSO, hotbar.selectedItem, cellMousePosition)){
                //if tile has states
                if(hotbar.selectedItem.tile.needsUpdate){
                    currentTileManager.entityUpdater.tilePositions.Add(cellMousePosition);
                    currentTileManager.entityUpdater.saveValues.Add(0);
                }
            }
        }
    }

    private void Attacking(){
        Collider2D actionCollider = Physics2D.OverlapCircle(flooredMousePos + new Vector3(.5f,.5f,0), .1f, actionMask);
        if(actionCollider == null){
            GameObject newActionPrefab = Instantiate(actionPrefab, flooredMousePos, Quaternion.identity);
            newActionPrefab.GetComponent<action_indicator>().SetupValues(hotbar.selectedItem.baseDamage, hotbar.selectedItem.activateSpeed, hotbar.selectedItem.activeDuration);
        }else if(actionCollider.transform.GetComponent<action_indicator>() == null){
            GameObject newActionPrefab = Instantiate(actionPrefab, flooredMousePos, Quaternion.identity);
            newActionPrefab.GetComponent<action_indicator>().SetupValues(hotbar.selectedItem.baseDamage, hotbar.selectedItem.activateSpeed, hotbar.selectedItem.activeDuration);
            
        }
    }

    public void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(flooredPos,new Vector3(1,1,0));
    
    }
}
