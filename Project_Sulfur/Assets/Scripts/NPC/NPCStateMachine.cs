using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<ColliderAggro>().Aggro)
        {
            this.GetComponent<Wander>().enabled = false;
            this.GetComponent<Wander>().isMoving = false;
        }   else 

        {
          this.GetComponent<Wander>().enabled = true;  
        }   
 if(this.GetComponent<Retreat>().retreat)
        {
            this.GetComponent<Wander>().enabled = false;
            this.GetComponent<Wander>().isMoving = false;
            this.GetComponent<ColliderAggro>().Aggro =false;
        }   else 

        {
          this.GetComponent<Wander>().enabled = true;  
        }   

    
    }
}
