using System;
using UnityEngine;

public class perlin_noise_script : MonoBehaviour
{

    public int width = 256;
    public int height = 256;

    public static float scale = .1f;
    public float texScale = 5f;

    public static float offsetX = 100f;
    public static float offsetY = 100f;

    //modifying perlin value
    public static int octaves = 3;
    public static float lacunarity = 2f;
    public static float persistence = .5f;
    static float maxNoiseHeight = float.MinValue;
    static float minNoiseHeight = float.MaxValue;

    
    // biome variables
    public static float altitudeOffsetX;
    public static float altitudeOffsetY;
    public static float humidityOffsetX;
    public static float humidityOffsetY;
    public static float biomeScale = .05f;
    public static float biomeOffsetMultiplier = .005f;


    public float textureOffsetMultiplier = .2f;
    public static float offsetMultiplier = 0.01f;

    public static int seed;

    void Start(){
        System.Random seedprng = new System.Random(); 
        seed = seedprng.Next();


        System.Random prng = new System.Random(seed);
        offsetX = prng.Next(-100000,100000);
        offsetY = prng.Next(-100000,100000);

        //setup biome variables;
        altitudeOffsetX = prng.Next(-100000,100000);
        altitudeOffsetY = prng.Next(-100000,100000);
        humidityOffsetX = prng.Next(-100000,100000);
        humidityOffsetY = prng.Next(-100000,100000);
    }

    void Update(){

        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null){
            renderer.material.mainTexture = GenerateTexture();
        }
    }

    Texture2D GenerateTexture(){
        Texture2D texture = new Texture2D(width, height);

        //Generate a perlin noise map for the texture
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                float sample = CalculatePerlin(x, y);
                Color color = CalculateColor(sample);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(float sample){


        return new Color(sample,sample,sample);
    }

    public static float[,] GenerateNoise(int mapWidth, int mapHeight, Vector3 chunkCoord){
        float[,] noiseMap = new float[mapWidth,mapHeight];
        for(int x = 0; x < mapWidth; x++){
            for(int y = 0; y < mapHeight; y++){

                float xCoord = (float)x / mapWidth * scale + (offsetMultiplier * -chunkCoord.x + offsetX);
                float yCoord = (float)y / mapHeight * scale + (offsetMultiplier * -chunkCoord.y + offsetY);

                float sample = Mathf.PerlinNoise(xCoord,yCoord);

                noiseMap[x,y] = sample;
            }
        }
        return noiseMap;
    }

    public static float[,] GenerateBiomeNoise(int mapWidth, int mapHeight, Vector3 chunkCoord, float biomeOffsetX, float biomeOffsetY){
        float[,] noiseMap = new float[mapWidth,mapHeight];
        for(int x = 0; x < mapWidth; x++){
            for(int y = 0; y < mapHeight; y++){

                float xCoord = (float)x / mapWidth * biomeScale + (biomeOffsetMultiplier * -chunkCoord.x + offsetX) + biomeOffsetX;
                float yCoord = (float)y / mapHeight * biomeScale + (biomeOffsetMultiplier * -chunkCoord.y + offsetY) + biomeOffsetY;

                float sample = Mathf.PerlinNoise(xCoord,yCoord);

                noiseMap[x,y] = sample;
            }
        }
        return noiseMap;
    }



    float CalculatePerlin(int x, int y){
        //offset -+ chunkposition
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {
            float xCoord = ((float)x -transform.position.x + offsetX) / width * texScale * frequency;
            float yCoord = ((float)y -transform.position.y + offsetY) / height * texScale * frequency;
            float sample = (Mathf.PerlinNoise(xCoord,yCoord) * 2 - 1);
            noiseHeight += sample * amplitude;

            maxValue += amplitude;
            amplitude *= persistence;
            frequency *= lacunarity;
        }

        if(noiseHeight > maxNoiseHeight){
            maxNoiseHeight = noiseHeight;
        }else if (noiseHeight < minNoiseHeight){
            minNoiseHeight = noiseHeight;
        }

        noiseHeight = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseHeight);
        
        return noiseHeight;
    }

    public static float[,] GenerateFBMNoiseMap(int mapWidth, int mapHeight, Vector3 chunkCoord, float biomeOffsetX, float biomeOffsetY, float _scale){
        float[,] noiseMap = new float[mapWidth,mapHeight];
        for(int x = 0; x < mapWidth; x++){
            for(int y = 0; y < mapHeight; y++){
                float frequency = 1;
                float amplitude = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float xCoord = ((float)x -chunkCoord.x + offsetX + biomeOffsetX) / mapWidth * _scale * frequency;
                    float yCoord = ((float)y -chunkCoord.y + offsetY + biomeOffsetY) / mapHeight * _scale * frequency;

                    float sample = Mathf.PerlinNoise(xCoord,yCoord) * 2 - 1;

                    noiseHeight += sample * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeight){
                    maxNoiseHeight = noiseHeight;
                }else if (noiseHeight < minNoiseHeight){
                    minNoiseHeight = noiseHeight;
                }
                
                

                noiseMap[x,y] = noiseHeight;
            }
        }

        for (int x = 0; x < mapWidth; x++){
            for (int y = 0; y < mapHeight; y++)
            {
                noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);
            }
        }
        return noiseMap;
    }

}
