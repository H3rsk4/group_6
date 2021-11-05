using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunk : MonoBehaviour
{
    public static Transform chunkPos;

    void Update(){
        chunkPos = transform;
    }
}
