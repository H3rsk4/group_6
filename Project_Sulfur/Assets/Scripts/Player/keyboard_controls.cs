using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboard_controls : MonoBehaviour
{
    public float speed;
    public Vector2 input;

    public tile_manager currentTileManager;

    
    private Vector3 leftRay;

    private Vector3 centerRay;
    private Vector3 rightRay;

    Vector3[] rays = new Vector3[3];

    private Vector3 right = new Vector3(.5f,0,0);
    private Vector3 left = new Vector3(-.5f,0,0);
    private Vector3 up = new Vector3(0,.5f,0);
    private Vector3 down = new Vector3(0,-.5f,0);
    private Vector3 upright = new Vector3(.5f,.5f,0);
    private Vector3 upleft = new Vector3(-.5f,.5f,0);
    private Vector3 downright = new Vector3(.5f,-.5f,0);
    private Vector3 downleft = new Vector3(-.5f,-.5f,0);

    void Start()
    {
        rays[0] = leftRay;
        rays[1] = centerRay;
        rays[2] = rightRay;
    }

    // Update is called once per frame
    void Update()
    {
        

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        //input.Normalize();

        if(input.x == 1 && input.y == 0){
            //right
            centerRay = transform.position + right;
            rightRay = transform.position + downright;
            leftRay = transform.position + upright;
        }
        if(input.x == -1 && input.y == 0){
            //left
            centerRay = transform.position + left;
            rightRay = transform.position + upleft;
            leftRay = transform.position + downleft;

        }
        if(input.x == 0 && input.y == -1){
            //down
            centerRay = transform.position + down;
            rightRay = transform.position + downleft;
            leftRay = transform.position + downright;

        }
        if(input.x == 0 && input.y == 1){
            //up
            centerRay = transform.position + up;
            rightRay = transform.position + upright;
            leftRay = transform.position + upleft;

        }

        if(input.x == 1 && input.y == 1){
            //up right
            centerRay = transform.position + upright;
            rightRay = transform.position + right;
            leftRay = transform.position + up;
        }
        if(input.x == -1 && input.y == 1){
            //up left
            centerRay = transform.position + upleft;
            rightRay = transform.position + up;
            leftRay = transform.position + left;
        }
        if(input.x == 1 && input.y == -1){
            //down right
            centerRay = transform.position + downright;
            rightRay = transform.position + down;
            leftRay = transform.position + right;
        }
        if(input.x == -1 && input.y == -1){
            //down left
            centerRay = transform.position + downleft;
            rightRay = transform.position + left;
            leftRay = transform.position + down;
        }
        
        /*
        leftRay = new Vector3(Mathf.Floor(leftRay.x),Mathf.Floor(leftRay.y),Mathf.Floor(leftRay.z));
        tile_manager newTileManager = currentTileManager.CheckTileManager(leftRay);
        if(newTileManager != null){
            //Debug.Log("check");
            if(newTileManager.CheckTile(leftRay)){
                int tileIndex = newTileManager.vertexIndex;
                //Debug.Log(newTileManager.tileSO[tileIndex]);
                if(!newTileManager.tileSO[tileIndex].isWalkable){
                    
                    if(input.x < 0){
                        input.x = 0;
                    }

                }
            }
        }
        */

        if(transform.position.x > currentTileManager.trueCenter.x+5){
            //went right
            //currentTileManager.showOutline = false;
            currentTileManager = currentTileManager.Neighbours[1];
            //currentTileManager.showOutline = true;
        }
        if(transform.position.x < currentTileManager.trueCenter.x-5){
            //went left
            //currentTileManager.showOutline = false;
            currentTileManager = currentTileManager.Neighbours[2];
            //currentTileManager.showOutline = true;
        }
        if(transform.position.y > currentTileManager.trueCenter.y+5){
            //went up
            //currentTileManager.showOutline = false;
            currentTileManager = currentTileManager.Neighbours[3];
            //currentTileManager.showOutline = true;
        }
        if(transform.position.y < currentTileManager.trueCenter.y-5){
            //went down
            //currentTileManager.showOutline = false;
            currentTileManager = currentTileManager.Neighbours[4];
            //currentTileManager.showOutline = true;
        }
    }

    void FixedUpdate(){
        transform.Translate(input * speed * Time.deltaTime);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawLine(centerRay, transform.position);
        Gizmos.DrawLine(rightRay, transform.position);
        Gizmos.DrawLine(leftRay, transform.position);

    }
    

}
