using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_tiles_script : MonoBehaviour
{
    // Start is called before the first frame update
    public float width = 0;
    public float height = 0;
    public Sprite sprite;

    public Transform target;
    public Transform[] tileChecks;

    void Start()
    {
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                GameObject debugTile = new GameObject();
                debugTile.transform.position = new Vector3(1+x-width/2,1+y-height/2,0);
                debugTile.transform.SetParent(target);
                debugTile.AddComponent<SpriteRenderer>();
                debugTile.GetComponent<SpriteRenderer>().sprite = sprite;
                //Debug.Log("this happened");
            }
        }
        tileChecks = GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
