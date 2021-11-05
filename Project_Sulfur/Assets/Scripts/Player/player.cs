using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static Transform playerT;
    void Awake()
    {
        playerT = this.transform;
    }
}
