using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake_camera : MonoBehaviour
{
    private Camera cam;
    private float time = 1f;
    public float magnitude = 5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        time = 1;
        //Vector3 originalPos = cam.transform.position;
    }

    void Awake(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        
        time -= Time.deltaTime;
        if(time > 0){
            cam.transform.position = cam.transform.position + new Vector3(Random.Range(-time,time) * magnitude, Random.Range(-time,time) * magnitude);
        }
    }
}
