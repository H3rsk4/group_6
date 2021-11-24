using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_gob : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    float distance;
    public int damage = 5;

    private float hitCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = player.playerT.position - transform.position;
        distance = Vector3.Distance(player.playerT.position, transform.position);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        
    }
    private void FixedUpdate()
    {
        if(distance > 1f && distance < 10f){
            moveCharacter(movement);
        } else {
            //rotSpeed = .5f;
        }

        if(distance < 1f && hitCooldown < 0){
            //damage player;
            player.playerT.GetComponent<stats>().Damage(damage);
            hitCooldown = 1f;
        }
        
        hitCooldown -= Time.deltaTime;

    }
    void moveCharacter(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}