using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class change_tile_color : MonoBehaviour
{
    private tile_manager tileManager;
    private Color transparentColor;
    private Color normal;


    public List<Vector3Int> tilePositions = new List<Vector3Int>();
    public List<tile_manager> tileManagers = new List<tile_manager>();

    void Start(){
        transparentColor = new Color(1f,1f,1f, .5f);
        normal = new Color(1f,1f,1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        tileManager = GetComponent<get_my_tile_manager>().tileManager;

        if(tileManager != null){
            tile_manager currentTileManager = tileManager.CheckTileManager(transform.position);
            if(currentTileManager != null){
                for(int x = -1; x < 2; x++){
                        for(int y = -1; y < 2; y++){
                            Vector3Int cellPosition = currentTileManager.maps[2].WorldToCell(transform.position + new Vector3(x, y, 0));

                            if(currentTileManager.maps[2].GetTile(cellPosition)){

                                bool foundMatch = false;
                                foreach(Vector3 tilePos in tilePositions){
                                    if(cellPosition == tilePos){
                                        foundMatch = true;
                                        break;
                                    }
                                    foundMatch = false;
                                }
                                if(!foundMatch){
                                    currentTileManager.maps[2].SetTileFlags(cellPosition, TileFlags.None);
                                    currentTileManager.maps[2].SetColor(cellPosition, transparentColor);
                                    tilePositions.Add(cellPosition);
                                    tileManagers.Add(currentTileManager);

                                }
                            }
                        }

                    }
                }
                

                
            
            
        }

        for (int i = 0; i < tilePositions.Count; i++)
        {
            Vector3 worldPosition = tileManagers[i].maps[2].CellToWorld(tilePositions[i]);
            float distance = Vector3.Distance(worldPosition, transform.position);
            if(distance > 2f){
                tileManagers[i].maps[2].SetColor(tilePositions[i], normal);
                tilePositions.Remove(tilePositions[i]);
                tileManagers.Remove(tileManagers[i]);
                
            }
        }
    }
}
