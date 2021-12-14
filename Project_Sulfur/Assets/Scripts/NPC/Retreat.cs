using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool retreat = false;
    public float timeLeft = 0f;
    float startingTime = 10f;
    public Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
         rb = this.GetComponent<Rigidbody2D>();
         timeLeft = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<stats>().currentHealth < 4){
            retreat=true;
        }
        if(retreat){
            direction = player.playerT.position - transform.position;
            direction.Normalize();
            movement = -direction;
            timeLeft -= 1 * Time.deltaTime;
        }
        if(timeLeft <= 0){
            retreat = false;
            timeLeft = 10;
            this.GetComponent<stats>().currentHealth = 4;
        }
    }
    void FixedUpdate()
    {
        if(retreat){
            moveCharacter(movement);
        }
    }
    void moveCharacter(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
