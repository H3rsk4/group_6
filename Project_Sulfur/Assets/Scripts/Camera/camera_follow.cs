using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    [SerializeField]
    private int distance; //do not change!!

    public Transform thisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = player.playerT;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(thisPlayer.position.x,thisPlayer.position.y,distance);
        //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,playerPos,.2f);
        Camera.main.transform.position = playerPos;
    }
}
