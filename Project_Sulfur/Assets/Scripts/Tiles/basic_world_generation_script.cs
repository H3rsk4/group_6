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

    public _Biome plains;
    public _Biome snowPlains;
    private _Biome currentBiome;

    public TileBase ground;
    public TileBase water;
    public TileBase tree;
    public TileBase sand;
    public TileBase wall;
    //public _Tile ground;
    public _Tile foliage;

    public _Tile veins;
    
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


    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void GenerateRectangleWorld(){
        float[,] myNoiseMap = perlin_noise_script.GenerateFBMNoiseMap(worldWidth,worldHeight, transform.position, 0, 0, perlin_noise_script.scale);

        float[,] myHumidityMap = perlin_noise_script.GenerateFBMNoiseMap(worldWidth,worldHeight,transform.position,perlin_noise_script.humidityOffsetX,perlin_noise_script.humidityOffsetY, perlin_noise_script.biomeScale);
        float[,] myAltitudeMap = perlin_noise_script.GenerateFBMNoiseMap(worldWidth,worldHeight,transform.position,perlin_noise_script.altitudeOffsetX,perlin_noise_script.altitudeOffsetY, perlin_noise_script.biomeScale);

        for(int x = 0; x < worldWidth; x++){
            for(int y = 0; y < worldHeight; y++){
                if(myHumidityMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))] < .5f){
                    currentBiome = snowPlains;
                }else{
                    currentBiome = plains;
                }
                if(myNoiseMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))] > .4f){
                    if(myNoiseMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))] < .45f){
                        //generate sand
                        tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), sand, tileManager.maps[0]);
                    }else{
                        if(myNoiseMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))] > .8f){
                            //wall
                            tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), wall, tileManager.maps[1]);
                        }else{
                            int chance = Random.Range(0,10);
                            if(chance < 1){
                                //tree
                                tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), tree, tileManager.maps[1]);
                            }
                        }
                        //generate ground
                        tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), currentBiome.floorTile, tileManager.maps[0]);
                    }
                    
                }else{
                    //generate water
                    tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), currentBiome.baseTile, tileManager.maps[0]);
                }
                

                /*
                int chance = Random.Range(0,10);
                if(chance < 1){
                    tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), tree, tileManager.maps[1]);
                }
                tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), ground, tileManager.maps[0]);
                */
            }
        }
    }
    

    private void GenerateStuff(int iterations, _Tile tile){
        for(int i = 0; i < Random.Range(0,iterations + 1); i++){
            int tileIndex = Random.Range(0, 100);
            //tileManager.ReplaceTile(tileIndex, tile, 0);
        }
    }

}
