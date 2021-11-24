using UnityEngine;

public class voronoi_noise : MonoBehaviour
{
    public int size;
    public int regionAmount;
    public int regionColorAmount;

    public float offsetX;

    

    // Start is called before the first frame update
    void Start()
    {
        CreateVoronoi();
    }

    // Update is called once per frame
    void Update()
    {
        //CreateVoronoi();
    }

    public void CreateVoronoi(){
        Vector2[] points = new Vector2[regionAmount];
        Color[] regionColors = new Color[regionColorAmount];

        //making a pointmap
        for(int i = 0; i < regionAmount; i++){
            //perlin noise here
            //random point in the area
            points[i] = new Vector2(Random.Range(0,size), Random.Range(0,size));
        }

        //to make random colours
        for(int i = 0; i < regionColorAmount; i++){
            //float color = Random.Range(0f,1f);
            regionColors[i] = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), 1);
            //regionColors[i] = new Color(color, color, color, 1);
        }

        Color[] pixelColors = new Color[size * size];

        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                float distance = float.MaxValue;
                int value = 0;

                for(int i = 0; i < regionAmount; i++){
                    if(Vector2.Distance(new Vector2(x,y), points[i]) < distance){
                        distance = Vector2.Distance(new Vector2(x,y), points[i]);
                        value = i;
                    }
                }

                pixelColors[x + y * size] = regionColors[value % regionColorAmount];
            }
        }

        Texture2D myTexture = new Texture2D(size, size);
        myTexture.SetPixels(pixelColors);
        myTexture.Apply();

        GetComponent<Renderer>().material.mainTexture = myTexture;
    }
}
