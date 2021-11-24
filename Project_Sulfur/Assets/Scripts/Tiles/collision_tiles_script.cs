using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_tiles_script : MonoBehaviour
{
    // Start is called before the first frame update
    public float width = 0;
    public float height = 0;
    public Sprite sprite;


    public Transform target;
    public Transform player;
    public BoxCollider2D[] tileChecks;

    public tile_manager tileplacer;
    public check_chunks chunkChecker;

    private int ticksToCheckTiles = 5;
    private int currentTick = 0;

    void Start()
    {
        tick_system.OnTick += delegate{
            currentTick++;
        };

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                GameObject collisionTile = new GameObject();
                
                //collisionTile.transform.position = new Vector3(1+x-width/2,1+y-height/2,0);
                collisionTile.transform.position = new Vector3(x-width/2+.5f,y-height/2+.5f,0);
                collisionTile.transform.SetParent(target);

                collisionTile.AddComponent<SpriteRenderer>();
                collisionTile.GetComponent<SpriteRenderer>().sprite = sprite;
                collisionTile.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);

                collisionTile.AddComponent<BoxCollider2D>();
                collisionTile.GetComponent<BoxCollider2D>().enabled = false;

                //Debug.Log("this happened");
            }
        }
        tileChecks = GetComponentsInChildren<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(currentTick >= ticksToCheckTiles){
            UpdateCollisionPos();
            currentTick = 0;
        }

        Vector3 newPosition = new Vector3(Mathf.Floor(player.position.x),Mathf.Floor(player.position.y),Mathf.Floor(player.position.z));
        if(newPosition != transform.position){
            transform.position = newPosition;
            //call out update collision boxes here
            UpdateCollisionPos();
        }
    }

    private void UpdateCollisionPos(){
        //Debug.Log(transform.TransformPoint(tileChecks[24].transform.position) - transform.position);
        foreach (BoxCollider2D collider in tileChecks){
            tile_manager currentTileManager = chunkChecker.CheckTileManager(transform.TransformPoint(collider.transform.position) - transform.position);
            //Debug.Log(currentTileManager);
            if(currentTileManager != null){
                //Debug.Log(transform.TransformPoint(collider.transform.position) - transform.position + " transform point");
                //Debug.Log(collider.transform.position + " normal pos");
                if(currentTileManager.CheckTile(transform.TransformPoint(collider.transform.position) - transform.position)){
                    int tileIndex = currentTileManager.vertexIndex;
                    if(!currentTileManager.tileSO[tileIndex].isWalkable){
                        // we found water
                        // activate collider
                        collider.enabled = true;
                        //collider.transform.GetComponent<SpriteRenderer>().color = Color.red;
                    } else {
                        collider.enabled = false;
                        //collider.transform.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                } else {
                    //collider.enabled = true;
                    //collider.transform.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
            
            
        }
    }
    /*
    private void UpdateCollisionPos(){
        foreach (BoxCollider2D collider in tileChecks){
                if(tileplacer.CheckTile(collider.transform.position)){
                    int tileIndex = tileplacer.vertexIndex;
                    if(!tileplacer.tileSO[tileIndex].isWalkable){
                        // we found water
                        // activate collider
                        collider.enabled = true;
                        //collider.transform.GetComponent<SpriteRenderer>().color = Color.red;
                    } else {
                        collider.enabled = false;
                        //collider.transform.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                } else {
                    collider.enabled = true;
                    //collider.transform.GetComponent<SpriteRenderer>().color = Color.green;
                }
            
        }
    }
    */
}
