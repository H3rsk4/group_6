using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_script : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 0;
    public float angle = 0;

    public LayerMask layerMask;
    public Collider2D col;

    public Vector3 newDirection;
    public Vector3 target;
    public Vector3 offset;

    public float range = 5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = target - transform.position;
        //newDirection = Vector3.RotateTowards(transform.position, offset, 0f, 0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(angle != transform.eulerAngles.z){
            transform.eulerAngles = new Vector3(0,0,angle);
        }
        */
        
        
        col = Physics2D.OverlapCircle(transform.position, .2f, layerMask);
        if(col != null){
            col.transform.GetComponent<stats>().Damage(damage);
            RemoveProjectile();
        }
    }
    void FixedUpdate(){
        

        range -= Time.deltaTime;
        if(range < 0){
            range = 50f;
            RemoveProjectile();
        }
        Vector2 clampedDirection = Vector2.ClampMagnitude(offset, 1);
        transform.Translate(clampedDirection * speed * Time.deltaTime);
    }

    void RemoveProjectile(){
        Destroy(this.gameObject);
    }
}
