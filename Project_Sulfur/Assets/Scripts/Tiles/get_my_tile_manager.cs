using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_my_tile_manager : MonoBehaviour
{
    public LayerMask layerMask;
    public tile_manager tileManager;
    private Vector3 flooredPos = new Vector3(0,0,0);

    // Update is called once per frame
    void Update()
    {
        if(tileManager == null){
            GetTileManager();
        }
        UpdateTileManager();
    }

    private void UpdateTileManager(){
        Vector3 currentFlooredPos = new Vector3(Mathf.Round(transform.position.x / 10 + .01f) * 10,Mathf.Round(transform.position.y / 10 + .01f)  * 10,0);
        if(flooredPos != currentFlooredPos){
            flooredPos = currentFlooredPos;
            GetTileManager();
        }
    }

    private void GetTileManager(){
        Collider2D managerCollider = Physics2D.OverlapCircle(flooredPos, 1, layerMask);
        if(managerCollider != null){
            tileManager = managerCollider.transform.GetComponent<tile_manager>();
        }
    }

    public void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(flooredPos,new Vector3(2,2,0));
    
    }
}
