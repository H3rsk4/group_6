using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_damager : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask layermask;
    public Collider2D col;
    void Start()
    {
        col = Physics2D.OverlapCircle(transform.position, 2f, layermask);
        /*
        if(col != null){
            Debug.Log("yes");
        }
        */

        //damage the gob
        if(col != null){
            col.transform.GetComponent<stats>().Damage(10);
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
