using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{

public Retreat Retreat;
public Wander Wander;
public Attack Attack;
public ColliderAggro Aggro;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        if(Aggro.IsAggro)
        {
        this.Wander.wander = false;
        }  
         else { 
        this.Wander.wander = true;
        }   
 
 if(Retreat.retreat)
        {
        this.Wander.wander = false;        
        this.Aggro.IsAggro =false;
        }   
        else {
          this.Wander.wander = true;  
        } 

    
    }
}
