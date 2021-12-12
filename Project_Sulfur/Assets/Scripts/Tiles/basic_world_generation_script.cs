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

    public _Biome[] biomes;

    public TileBase ground;
    public TileBase water;
    public TileBase tree;
    public TileBase sand;
    public TileBase wall;
    
    public TileBase pebbles;
    public TileBase twigs;
    public TileBase vines;
    //public _Tile ground;
    public _Tile foliage;

    public _Tile veins;
    
    public bool isGobs = true;
    public GameObject gob;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateRectangleWorld();
        GenerateComplexChunk();
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
                                int rollchange = Random.Range(0,4);
                                switch(rollchange){
                                    case 0:
                                        //tree
                                        tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), tree, tileManager.maps[1]);
                                        break;
                                    case 1:
                                        //pebbles
                                        tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), pebbles, tileManager.maps[1]);
                                        break;
                                    case 2:
                                        //twigs
                                        tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), twigs, tileManager.maps[1]);
                                        break;
                                    case 3:
                                        //vines
                                        tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), vines, tileManager.maps[1]);
                                        break;
                                }
                                //tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), tree, tileManager.maps[1]);
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

    private void GenerateComplexChunk(){
        float[,] myNoiseMap = perlin_noise_script.GenerateFBMNoiseMap(worldWidth,worldHeight, transform.position, 0, 0, perlin_noise_script.scale);

        float[,] myHumidityMap = perlin_noise_script.GenerateFBMNoiseMap(worldWidth,worldHeight,transform.position,perlin_noise_script.humidityOffsetX,perlin_noise_script.humidityOffsetY, perlin_noise_script.biomeScale);
        float[,] myAltitudeMap = perlin_noise_script.GenerateFBMNoiseMap(worldWidth,worldHeight,transform.position,perlin_noise_script.altitudeOffsetX,perlin_noise_script.altitudeOffsetY, perlin_noise_script.biomeScale);
        
        //loop through chunk
        for(int x = 0; x < worldWidth; x++){
            for(int y = 0; y < worldHeight; y++){
                //check for negative values
                if(myAltitudeMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))] < 0){
                    Debug.Log(myAltitudeMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))] + " found negative");
                }

                //check biome
                _Biome currentBiome = biomes[0];
                for(int i = 0; i < biomes.Length; i++){
                    if(biomes[i].minAltitude <= myAltitudeMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))]){
                        
                        if(biomes[i].maxAltitude >= myAltitudeMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))]){
                            currentBiome = biomes[i];
                            break;
                        }else{
                            continue;
                        }
                    }else{
                        continue;
                    }
                    
                }
                //check Tile
                TileBase currentTile = currentBiome.tileVariants[0].tileBase;
                int baseLayer = 0;
                TileBase detailTile = null;
                int detailLayer = 1;
                for(int i = 0; i < currentBiome.tileVariants.Length; i++){
                    if(currentBiome.tileVariants[i].minHeight <= myNoiseMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))]){
                        
                        if(currentBiome.tileVariants[i].maxHeight >= myNoiseMap[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))]){
                            currentTile = currentBiome.tileVariants[i].tileBase;
                            baseLayer = currentBiome.tileVariants[i].buildLayer;
                            //tile specific dictionary check for detail noiseoffset
                            Vector2 detailOffset;
                            if(perlin_noise_script.customOffsets.ContainsKey(currentTile)){
                                //key found use this offset
                                detailOffset = perlin_noise_script.customOffsets[currentTile];
                            }else{
                                //add new key
                                detailOffset = perlin_noise_script.newRandomOffset();
                                perlin_noise_script.customOffsets.Add(currentTile, detailOffset);
                            }
                            float[,] detailNoise = perlin_noise_script.GenerateFBMNoiseMap(worldWidth,worldHeight, transform.position, detailOffset.x, detailOffset.y, 1f);

                            //check for details
                            for(int j = 0; j < currentBiome.tileVariants[i].tileVariantDetails.Length; j++){
                                if(currentBiome.tileVariants[i].tileVariantDetails[j].minHeight <= detailNoise[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))]){
                                    if(currentBiome.tileVariants[i].tileVariantDetails[j].maxHeight >= detailNoise[Mathf.Abs(x-(worldWidth-1)),Mathf.Abs(y-(worldHeight-1))]){
                                        detailTile = currentBiome.tileVariants[i].tileVariantDetails[j].tileBase;
                                    }else{
                                        continue;
                                    }
                                }else{
                                    continue;
                                }
                            }
                            break;
                        }else{
                            continue;
                        }
                    }else{
                        continue;
                    }
                }


                tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), currentTile, tileManager.maps[baseLayer]);
                if(baseLayer != 1){
                    tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), detailTile, tileManager.maps[1]);
                    /*
                    if(detailTile == vines){
                        Debug.Log(detailTile);
                    }
                    */
                    
                }else{
                    tileManager.ReplaceTile(new Vector3Int(x-worldWidth/2, y-worldHeight/2, 0), ground, tileManager.maps[0]);
                }
                

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
