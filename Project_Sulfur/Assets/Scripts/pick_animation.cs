using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick_animation : MonoBehaviour
{
    public ColliderAggro colliderAggro;
    public Retreat retreat;
    public Wander wander;

    public Animator animator;
    private string currentState;
    public Vector3 direction;
    public Vector3 currentDirection;
    public Vector3 newDirection;

    public bool isIdle;
    //public Vector3 snappedDirection;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 mousePos = Input.mousePosition;
        //Vector3 mousePos = GetInputPosition();
        //Vector3 mousePos = touch.position;
        
        //mousePos.z = 10;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //direction = mousePos - player.playerT.position;
        //direction.Normalize();
        newDirection = RoundDirection(direction);

        if(colliderAggro.IsAggro){
            //running
            direction = colliderAggro.direction;
        }
        if(retreat.retreat){
            //retreating
            direction = -retreat.direction;
        }
        if(!colliderAggro.IsAggro && !retreat.retreat){
            //wander
            direction = wander.rb.velocity;
            direction.Normalize();

            if(wander.IsMoving){
                isIdle = false;
            }else{
                isIdle = true;
            }

        }else{
            isIdle = false;
        }


        if(currentDirection != newDirection || isIdle != wander.IsMoving){
            //Debug.Log("snapped");
            currentDirection = newDirection;
            //change animation
            if(currentDirection == new Vector3(0,-1,0)){
                //down
                if(!isIdle){
                    ChangeAnimationState("run_down");
                }else{
                    ChangeAnimationState("idle_down");
                }
            }
            if(currentDirection == new Vector3(1,-1,0)){
                //down
                if(!isIdle){
                    ChangeAnimationState("run_right_down");
                }else{
                    ChangeAnimationState("idle_right_down");
                }
            }
            if(currentDirection == new Vector3(-1,-1,0)){
                //down
                if(!isIdle){
                    ChangeAnimationState("run_left_down");
                }else{
                    ChangeAnimationState("idle_left_down");
                }
            }
            
            if(currentDirection == new Vector3(-1,0,0)){
                //left
                if(!isIdle){
                    ChangeAnimationState("run_left");
                }else{
                    ChangeAnimationState("idle_left");
                }
            }
            if(currentDirection == new Vector3(1,0,0)){
                //right
                if(!isIdle){
                    ChangeAnimationState("run_right");
                }else{
                    ChangeAnimationState("idle_right");
                }
            }
            if(currentDirection == new Vector3(0,1,0)){
                //up
                if(!isIdle){
                    ChangeAnimationState("run_up");
                }else{
                    ChangeAnimationState("idle_up");
                }
            }
            if(currentDirection == new Vector3(-1,1,0)){
                //up
                if(!isIdle){
                    ChangeAnimationState("run_left_up");
                }else{
                    ChangeAnimationState("idle_left_up");
                }
            }
            if(currentDirection == new Vector3(1,1,0)){
                //up
                if(!isIdle){
                    ChangeAnimationState("run_right_up");
                }else{
                    ChangeAnimationState("idle_right_up");
                }
            }
        }
    }

    private Vector3 RoundDirection(Vector3 direction){
        //normalized direction
        Vector3 roundedDir = new Vector3(Mathf.RoundToInt(direction.x),Mathf.RoundToInt(direction.y),Mathf.RoundToInt(direction.z));

        return roundedDir;
    }

    private float GetAnimationFrame(){
        float frameTime;
        frameTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return frameTime;
    }

    void ChangeAnimationState(string newState){
        //stop the same animation from interrupting itself
        if(currentState == newState){
            return;
        }
        float frameTime = GetAnimationFrame();

        //play the animation
        animator.Play(newState, 0, frameTime);


        //reassign the current state
        currentState = newState;
    }
}
