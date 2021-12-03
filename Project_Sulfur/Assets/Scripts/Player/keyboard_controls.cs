using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboard_controls : MonoBehaviour
{
    public Animator animator;
    private string currentState;

    //all animation states

    //idle
    const string IDLE_DOWN = "idle_down";
    const string IDLE_LEFT = "idle_left";
    const string IDLE_LEFT_DOWN = "idle_left_down";
    const string IDLE_LEFT_UP = "idle_left_up";
    const string IDLE_RIGHT = "idle_right";
    const string IDLE_RIGHT_DOWN = "idle_right_down";
    const string IDLE_RIGHT_UP = "idle_right_up";
    const string IDLE_UP = "idle_up";

    //run
    const string RUN_DOWN = "run_down";
    const string RUN_LEFT = "run_left";
    const string RUN_LEFT_DOWN = "run_left_down";
    const string RUN_LEFT_UP = "run_left_up";
    const string RUN_RIGHT = "run_right";
    const string RUN_RIGHT_DOWN = "run_right_down";
    const string RUN_RIGHT_UP = "run_right_up";
    const string RUN_UP = "run_up";




    public float speed;
    public Vector2 input;

    
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

    private Vector2 lastInput = new Vector2(0,-1);

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

        if(input.x == 0 && input.y == 0){
            //idle
            switch(lastInput.x){
                case -1:
                    switch(lastInput.y){
                        case -1:
                            //left down
                            ChangeAnimationState(IDLE_LEFT_DOWN);
                            break;
                        case 0:
                            //left
                            ChangeAnimationState(IDLE_LEFT);
                            break;
                        case 1:
                            //left up
                            ChangeAnimationState(IDLE_LEFT_UP);
                            break;
                    }
                    break;
                case 0:
                    switch(lastInput.y){
                        case -1:
                            //down
                            ChangeAnimationState(IDLE_DOWN);
                            break;
                        case 1:
                            //up
                            ChangeAnimationState(IDLE_UP);
                            break;
                    }
                    break;
                case 1:
                    switch(lastInput.y){
                        case -1:
                            //right down
                            ChangeAnimationState(IDLE_RIGHT_DOWN);
                            break;
                        case 0:
                            //right
                            ChangeAnimationState(IDLE_RIGHT);
                            break;
                        case 1:
                            //right up
                            ChangeAnimationState(IDLE_RIGHT_UP);
                            break;
                    }
                    break;
            }
                
        }

        if(input.x == 1 && input.y == 0){
            //right
            centerRay = transform.position + right;
            rightRay = transform.position + downright;
            leftRay = transform.position + upright;

            ChangeAnimationState(RUN_RIGHT);
            lastInput = new Vector2(input.x, input.y);
        }
        if(input.x == -1 && input.y == 0){
            //left
            centerRay = transform.position + left;
            rightRay = transform.position + upleft;
            leftRay = transform.position + downleft;

            ChangeAnimationState(RUN_LEFT);
            lastInput = new Vector2(input.x, input.y);
        }
        if(input.x == 0 && input.y == -1){
            //down
            centerRay = transform.position + down;
            rightRay = transform.position + downleft;
            leftRay = transform.position + downright;

            ChangeAnimationState(RUN_DOWN);
            lastInput = new Vector2(input.x, input.y);
        }
        if(input.x == 0 && input.y == 1){
            //up
            centerRay = transform.position + up;
            rightRay = transform.position + upright;
            leftRay = transform.position + upleft;

            ChangeAnimationState(RUN_UP);
            lastInput = new Vector2(input.x, input.y);
        }

        if(input.x == 1 && input.y == 1){
            //up right
            centerRay = transform.position + upright;
            rightRay = transform.position + right;
            leftRay = transform.position + up;

            ChangeAnimationState(RUN_RIGHT_UP);
            lastInput = new Vector2(input.x, input.y);
        }
        if(input.x == -1 && input.y == 1){
            //up left
            centerRay = transform.position + upleft;
            rightRay = transform.position + up;
            leftRay = transform.position + left;

            ChangeAnimationState(RUN_LEFT_UP);
            lastInput = new Vector2(input.x, input.y);
        }
        if(input.x == 1 && input.y == -1){
            //down right
            centerRay = transform.position + downright;
            rightRay = transform.position + down;
            leftRay = transform.position + right;

            ChangeAnimationState(RUN_RIGHT_DOWN);
            lastInput = new Vector2(input.x, input.y);
        }
        if(input.x == -1 && input.y == -1){
            //down left
            centerRay = transform.position + downleft;
            rightRay = transform.position + left;
            leftRay = transform.position + down;

            ChangeAnimationState(RUN_LEFT_DOWN);
            lastInput = new Vector2(input.x, input.y);
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
        
        
    }

    void FixedUpdate(){
        transform.Translate(input * speed * Time.deltaTime);
    }

    void ChangeAnimationState(string newState){
        //stop the same animation from interrupting itself
        if(currentState == newState){
            return;
        }

        //play the animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawLine(centerRay, transform.position);
        Gizmos.DrawLine(rightRay, transform.position);
        Gizmos.DrawLine(leftRay, transform.position);

    }
    

}
