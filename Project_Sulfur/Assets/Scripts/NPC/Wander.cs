using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float moveSpeed = 3;
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    private Vector2 movement;
    //public bool Idle = true;

    public float moveTime;
    private float moveCounter;
    public float waitTime;
    private float waitCounter;
    public bool wander = true;
    public bool IsMoving = true;
    //private int moveDirection;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(Random.Range(-1,1),Random.Range(-1,1));

        waitCounter = waitTime;
        moveCounter = moveTime;

        chooseDirection();
        SetDirection();
        chooseMoveTime();    
        chooseWaitTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(wander){
        if(IsMoving)
        {
          //  Debug.Log("on");
            moveCounter -= Time.deltaTime;
            rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
        

          /*  switch(moveDirection)
            {
            case 0:
                rb.velocity = new Vector2 (0, moveSpeed);
                break;
            case 1:
                rb.velocity = new Vector2 (moveSpeed, 0);

                break;
            case 2:
                rb.velocity = new Vector2 (0, -moveSpeed);
                break;
            case 3:
                rb.velocity = new Vector2 (-moveSpeed, 0);
                break;
            } */
            if(moveCounter < 0)
            {
              //  Debug.Log("off");
                IsMoving = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;

            if(waitCounter < 0)
            {
                chooseDirection();
                SetDirection();
                chooseMoveTime();
                chooseWaitTime();
            }
        }
        }
    }
    //Randomizes the move direction.
    public void chooseDirection()
    {
       moveDirection = new Vector2(Random.Range(-1,2),Random.Range(-1,2));
       IsMoving = true;
       moveCounter = moveTime; 
    }
    //Randomizes the time used for moving.
    public void chooseMoveTime()
    {
       moveTime = Random.Range (0, 4);
        IsMoving = true;
       moveCounter = moveTime;
    }
    // Randomizes the time used waiting between moves.
    public void chooseWaitTime()
    {
       waitTime = Random.Range (0, 4);
       IsMoving = true;
       moveCounter = moveTime;
    }
    void SetDirection(){
      movement = new Vector2(moveDirection.x, moveDirection.y).normalized;
    }
}
