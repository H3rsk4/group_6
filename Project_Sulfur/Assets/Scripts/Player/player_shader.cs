using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shader : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
    }
}
