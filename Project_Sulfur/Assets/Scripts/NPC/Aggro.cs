using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    // Start is called before the first frame update
     public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float distance;
    public int damage = 5;
    private CircleCollider2D ccol;

    private float hitCooldown = 1f;
  
    void Start()
    {
    rb = this.GetComponent<Rigidbody2D>();
    distance = Vector3.Distance(player.playerT.position, transform.position);
    ccol = this.GetComponent<CircleCollider2D>();
    ccol.radius = 20;
    ccol.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.playerT.position - transform.position;
        distance = Vector3.Distance(player.playerT.position, transform.position);
        
        direction.Normalize();
        movement = direction;  
    }
private void FixedUpdate()
    {
        if(distance > 1f && distance < 10f)
        {
            moveCharacter(movement);
        } else 

        {
            //rotSpeed = .5f;
        }


           
    }
void moveCharacter(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}

