using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class camera_view_scale : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //cam.TransparencySortMode = TransparencySortMode.Orthographic;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
