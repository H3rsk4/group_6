using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;

    public float moveTime;
    private float moveCounter;
    public float waitTime;
    private float waitCounter;
    public bool isMoving;
    private int moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        moveCounter = moveTime;

        chooseDirection();
        chooseMoveTime();    
        chooseWaitTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            moveCounter -= Time.deltaTime;
        

            switch(moveDirection)
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
            }
            if(moveCounter < 0)
            {
                isMoving = false;
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
                chooseMoveTime();
                chooseWaitTime();
            }
        }
    }
    //Randomizes the move direction.
    public void chooseDirection()
    {
       moveDirection = Random.Range (0, 4);
       isMoving = true;
       moveCounter = moveTime; 
    }
    //Randomizes the time used for moving.
    public void chooseMoveTime()
    {
       moveTime = Random.Range (0, 4);
       isMoving = true;
       moveCounter = moveTime;
    }
    // Randomizes the time used waiting between moves.
    public void chooseWaitTime()
    {
       waitTime = Random.Range (0, 4);
       isMoving = true;
       moveCounter = moveTime;
    }
}
