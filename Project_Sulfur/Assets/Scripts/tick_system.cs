using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tick_system : MonoBehaviour
{
    public static event Action OnTick;


    private const float TICK_TIMER_MAX = .2f;
    private float tickTimer;

    /*
    //*********** this can be put anywhere in the project ************

    void Start(){
        
        tick_system.OnTick += delegate{
            Debug.Log("tick");
        };
        
    }
    */

    void Update()
    {
        tickTimer += Time.deltaTime;
        if(tickTimer >= TICK_TIMER_MAX){
            tickTimer -= TICK_TIMER_MAX;
            //tick happens
            //Debug.Log("tick");
            if(OnTick != null) OnTick();
        }
    }
}
