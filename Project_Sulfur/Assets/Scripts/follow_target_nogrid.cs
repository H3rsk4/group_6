using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_target_nogrid : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void FixedUpdate(){
        transform.position = target.position + offset;
    }
}
