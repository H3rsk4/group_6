using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_indicator : MonoBehaviour
{
    //Type
    public int damangeAmount;
    public float activateSpeed;
    public float activeDuration;
    bool isSet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupValues(int _damageAmount, float _activateSpeed, float _activeDuration){
        damangeAmount = _damageAmount;
        activateSpeed = _activateSpeed;
        activeDuration = _activeDuration;

        isSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSet){
            activateSpeed -= Time.deltaTime;
            if(activateSpeed < 0){
                //activate
                activeDuration -= Time.deltaTime;
                if(activeDuration < 0){
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
