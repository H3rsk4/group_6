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

    public float textureOffsetMultiplier = .2f;
    public static float offsetMultiplier = 0.01f;

    public int seed;

    void Start(){
        System.Random seedprng = new System.Random(); 
        seed = seedprng.Next();


        System.Random prng = new System.Random(seed);
        offsetX = prng.Next(-100000,100000);
        offsetY = prng.Next(-100000,100000);
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



    float CalculatePerlin(int x, int y){
        //offset -+ chunkposition

        float xCoord = (float)x / width * texScale + (textureOffsetMultiplier * -transform.position.x + offsetX);
        float yCoord = (float)y / height * texScale + (textureOffsetMultiplier * -transform.position.y + offsetY);

        float sample = Mathf.PerlinNoise(xCoord,yCoord);
        return sample;
    }

}
