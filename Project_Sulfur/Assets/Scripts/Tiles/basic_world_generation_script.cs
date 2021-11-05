using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class basic_world_generation_script : MonoBehaviour
{
    public tile_manager tileManager;
    public int worldWidth = 0;
    public int worldHeight = 0;
    public int offsetX = 0;
    public int offsetY = 0;

    public int foliageIterations = 10;
    public int veinIterations = 5;
    public int gobIterations = 5;

    public TileBase ground;
    public TileBase tree;
    //public _Tile ground;
    public _Tile foliage;

    public _Tile veins;
    public _Tile cliff;
    public bool isGobs = true;
    public GameObject gob;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRectangleWorld();
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Debug.Log(mesh.bounds.center);
        
        //Invoke("GenerateWorld", .5f);

    }

    private void GenerateWorld(){
        //GenerateStuff(foliageIterations, foliage);
        if(veins != null && cliff != null){
            //GenerateVeins(veinIterations, veins, cliff);
        }
        
        if(Mathf.Abs(tileManager.trueCenter.x) >= 30 || Mathf.Abs(tileManager.trueCenter.y) >= 30){
            if(isGobs){
                //GenerateGobs(gobIterations);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateRectangleWorld(){
        for(int x = 0; x < worldWidth; x++){
            for(int y = 0; y < worldHeight; y++){
                //collisionTile.transform.position = new Vector3(1+x-width/2,1+y-height/2,0);
                //collisionTile.transform.position = new Vector3(x-width/2+.5f,y-width/2+.5f,0);

                //***IMPORTANT NOTE HERE******
                //if x/2 leaves a .5f in the end result -> +.5f
                //if not, do not +.5f
                //tileManager.UpdateMesh(new Vector3(x-worldWidth/2 + offsetX,y-worldHeight/2 + offsetY, 0) ,ground,0 /*Random.Range(0,4)*/);
                int chance = Random.Range(0,10);
                if(chance < 1){
                    tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), tree, tileManager.maps[1]);
                }
                tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), ground, tileManager.maps[0]);
            }
        }
    }

    private void GenerateStuff(int iterations, _Tile tile){
        for(int i = 0; i < Random.Range(0,iterations + 1); i++){
            int tileIndex = Random.Range(0, 100);
            //tileManager.ReplaceTile(tileIndex, tile, 0);
        }
    }
    /*
    private void GenerateVeins(int iterations, _Tile vein, _Tile cliff){
        for(int i = 0; i < Random.Range(0,iterations + 1); i++){
            int tileIndex = Random.Range(0, 100);
            //tileManager.ReplaceTile(tileIndex, vein, 0);
            
            tile_manager currentTileManager = tileManager.CheckTileManager(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(1,0,0));
            if(currentTileManager != null){
                if(currentTileManager.CheckTile(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(1,0,0))){
                    int newTileIndex = currentTileManager.vertexIndex;
                    //currentTileManager.ReplaceTile(newTileIndex, cliff, 0);
                }
            }
            currentTileManager = tileManager.CheckTileManager(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(-1,0,0));
            if(currentTileManager != null){
                if(currentTileManager.CheckTile(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(-1,0,0))){
                    int newTileIndex = currentTileManager.vertexIndex;
                    //currentTileManager.ReplaceTile(newTileIndex, cliff, 0);
                }
            }
            currentTileManager = tileManager.CheckTileManager(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(0,1,0));
            if(currentTileManager != null){
                if(currentTileManager.CheckTile(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(0,1,0))){
                    int newTileIndex = currentTileManager.vertexIndex;
                    //currentTileManager.ReplaceTile(newTileIndex, cliff, 0);
                }
            }
            currentTileManager = tileManager.CheckTileManager(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(0,-1,0));
            if(currentTileManager != null){
                if(currentTileManager.CheckTile(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(0,-1,0))){
                    int newTileIndex = currentTileManager.vertexIndex;
                    //currentTileManager.ReplaceTile(newTileIndex, cliff, 0);
                }
            }
            
            
            
            
            
            
        }
    }

    private void GenerateGobs(int iterations){
        for(int i = 0; i < Random.Range(0,iterations + 1); i++){
            int tileIndex = Random.Range(0, 100);
            //tileManager.ReplaceTile(tileIndex, tile, 0);
            Instantiate(gob, tileManager.vertices[tileIndex * 4 + 0] + new Vector3(.5f,.5f), Quaternion.identity);
        }
    }
    */
}
