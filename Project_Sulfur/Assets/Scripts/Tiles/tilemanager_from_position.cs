using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilemanager_from_position : MonoBehaviour
{
    public LayerMask layerMask;
    public static LayerMask chunkMask;

    public void Start(){
        chunkMask = layerMask;
    }
    public static tile_manager GetTileManager(Vector3 position){
        tile_manager tileManager = null;
        Vector3 flooredPos = new Vector3(Mathf.Round(position.x / 10 + .01f) * 10,Mathf.Round(position.y / 10 + .01f)  * 10,0);

        Collider2D managerCollider = Physics2D.OverlapCircle(flooredPos, 1, chunkMask);
        if(managerCollider != null){
            tileManager = managerCollider.transform.GetComponent<tile_manager>(); 
        }
        
        return tileManager;
    }
}
