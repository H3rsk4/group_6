using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class perlin_noise_script_mod_01 : MonoBehaviour
{

    public int width = 256;
    public int height = 256;

    public static float scale = .1f;
    public float texScale = 5f;

    public static float offsetX = 100f;
    public static float offsetY = 100f;

    public float altitudeOffsetX;
    public float altitudeOffsetY;

    public float humidityOffsetX;
    public float humidityOffsetY;
    

    public float textureOffsetMultiplier = .2f;
    public static float offsetMultiplier = 0.01f;

    public float lowCutoff;
    public float highCutoff;

    public int biomeAmount;

    [Range(.01f, 10f)]
    public float testValue;

    public int seed;

    public List<Vector2> voronoiPoints = new List<Vector2>();
    float[,] perlinHumidity;
    float[,] perlinAltitude;

    Color[] biomeColors;

    void Start(){
        System.Random seedprng = new System.Random(); 
        seed = seedprng.Next();


        System.Random prng = new System.Random(seed);
        offsetX = prng.Next(-100000,100000);
        offsetY = prng.Next(-100000,100000);
        biomeColors = new Color[biomeAmount];
        for(int i = 0; i < biomeAmount; i++){
            biomeColors[i] = new Color((float)prng.NextDouble(),(float)prng.NextDouble(),(float)prng.NextDouble(), 1);
        }

        altitudeOffsetX = prng.Next(-100000,100000);
        altitudeOffsetY = prng.Next(-100000,100000);
        

        humidityOffsetX = prng.Next(-100000,100000);
        humidityOffsetY = prng.Next(-100000,100000);
        

    }

    void Update(){

        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null){
            
            perlinAltitude = GenerateNoise(width, height, transform.position, altitudeOffsetX, altitudeOffsetY);
            perlinHumidity = GenerateNoise(width, height, transform.position, humidityOffsetX, humidityOffsetY);
            renderer.material.mainTexture = GenerateTexture();
        }
    }

    Texture2D GenerateTexture(){
        voronoiPoints.Clear();
        System.Random prng = new System.Random(seed);
        Texture2D texture = new Texture2D(width, height);
        Color[] regionColors = new Color[biomeAmount];

        
        //Generate a perlin noise map for the texture
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                
                //Color color = CalculateColor(perlinAltitude[x,y], 0);
                
                if(perlinHumidity[x,y] < .45f){
                    if(perlinAltitude[x,y] < .45f){
                        //Debug.Log("Setting pixels");
                        texture.SetPixel(x, y, Color.green);
                    } else {
                        //Debug.Log("Setting pixels");
                        texture.SetPixel(x, y, Color.yellow);
                        if(perlinAltitude[x,y] > .7f){
                            texture.SetPixel(x, y, Color.red);
                        }
                    }
                }else{
                    if(perlinHumidity[x,y] > .65f){
                        if(perlinAltitude[x,y] > .65f){
                            //Debug.Log("Setting pixels");
                            texture.SetPixel(x, y, Color.white);
                        }else{
                            //Debug.Log("Setting pixels");
                            texture.SetPixel(x, y, Color.cyan);
                        }

                    }else{
                        texture.SetPixel(x, y, Color.blue);
                    }
                    
                }
                
                
    

                //texture.SetPixel(x, y, color);


                
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(float sample, int index){
        
        if(sample == 0){
            
        }
        return new Color(sample,sample,sample, 1);
    }

    public float[,] GenerateNoise(int mapWidth, int mapHeight, Vector3 chunkCoord, float biomeOffsetX, float biomeOffsetY){
        float[,] noiseMap = new float[mapWidth,mapHeight];
        for(int x = 0; x < mapWidth; x++){
            for(int y = 0; y < mapHeight; y++){

                float xCoord = (float)x / mapWidth * texScale + (textureOffsetMultiplier * -chunkCoord.x + offsetX + biomeOffsetX);
                float yCoord = (float)y / mapHeight * texScale + (textureOffsetMultiplier * -chunkCoord.y + offsetY + biomeOffsetY);

                float sample = Mathf.PerlinNoise(xCoord,yCoord);

                noiseMap[x,y] = sample;
            }
        }
        return noiseMap;
    }


    float CalculatePerlin(int x, int y, float _biomeOffsetX, float _biomeOffsetY){
        
        //offset -+ chunkposition
        float sample = 0;
        
        float xCoord = (float)x / width * texScale + (textureOffsetMultiplier * -transform.position.x + offsetX);
        float yCoord = (float)y / height * texScale + (textureOffsetMultiplier * -transform.position.y + offsetY);

        sample = Mathf.PerlinNoise(xCoord,yCoord);

        /*
        if(sample < lowCutoff){
            sample = 0f;
        }
        if(sample > highCutoff){
            sample = 1f;
        }
        */
        
        
        return sample;
    }

}
