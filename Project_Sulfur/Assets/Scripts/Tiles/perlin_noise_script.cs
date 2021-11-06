using System;
using UnityEngine;

public class perlin_noise_script : MonoBehaviour
{

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public int seed;

    void Start(){
        System.Random seedprng = new System.Random(); 
        seed = seedprng.Next();
    }

    void Update(){
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();

        System.Random prng = new System.Random(seed);
        offsetX = prng.Next(-100000,100000);
        offsetY = prng.Next(-100000,100000);
    }

    Texture2D GenerateTexture(){
        Texture2D texture = new Texture2D(width, height);

        //Generate a perlin noise map for the texture
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Color color = CalculateColor(x,y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y){

        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord,yCoord);
        return new Color(sample,sample,sample);
    }

}
