using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_target_grid_nomesh : MonoBehaviour
{
    // Start is called before the first frame update


    public Transform target;

    [Range(1,10)]
    public int multiplier = 1;
    public int offsetX = 0;
    public int offsetY = 0;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(Mathf.Floor(target.position.x / multiplier) * multiplier + offsetX,Mathf.Floor(target.position.y/ multiplier) * multiplier + offsetY,0);
        if(newPosition != transform.position){
            transform.position = newPosition;
        }
    }
}

