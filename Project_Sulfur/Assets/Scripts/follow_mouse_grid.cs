using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_mouse_grid : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 mousePosGrid = new Vector3(Mathf.Floor(mousePos.x),Mathf.Floor(mousePos.y),Mathf.Floor(mousePos.z));
        transform.position = mousePosGrid + new Vector3(.5f,.5f,0);
    }
}
