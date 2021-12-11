using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAggro : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private CircleCollider2D ccol;
    //private CircleCollider2D ccolB;
    private Transform target;
    public bool Aggro = false;
    public float OriginalRadius;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        ccol = gameObject.AddComponent<CircleCollider2D>();
        ccol.radius = OriginalRadius;
        ccol.isTrigger = true;

        Aggro = false;

       /* ccolB = gameObject.AddComponent<CircleCollider2D>();
        ccolB.radius = 10;
        ccolB.isTrigger = true;  */    
    }

    // Update is called once per frame
    void Update()
    {
 
        if(Aggro){
            Vector3 direction = player.playerT.position - transform.position;
            direction.Normalize();
            movement = direction; 
        }

    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Aggro = true;
            ccol.radius = (OriginalRadius + 5);
           // transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            Debug.Log("enter");
        } else 

        {

        }     
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
        Aggro = false;
        ccol.radius = OriginalRadius;
        Debug.Log("Exit");
        }
    }

    void FixedUpdate()
    {
        if(Aggro){
            moveCharacter(movement);
        }
    }
void moveCharacter(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}

