using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_script : MonoBehaviour
{

    public Transform target;
    //public inventory inv;
    private float distance;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = .3f;
    private bool isPickingUp = false;

    public _Item item;
    public int itemAmount;
    // Start is called before the first frame update
    void Start()
    {
        target = player.playerT;
        //inv = inventory.inventoryT.GetComponent<inventory>();
        distance = Vector3.Distance(target.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        //Debug.Log(direction);
    }

    void FixedUpdate(){
        if(distance < 4){
            isPickingUp = true;
        }
        if(distance < .5f){
            //pickup
            if(!inventory.instance.IsFull()){
                for(int i = 0; i < itemAmount; i++){
                    inventory.instance.AddItem(item);
                }
                Destroy(this.gameObject);
            }
            isPickingUp = false;
            
        }

        if(isPickingUp){
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
            smoothTime -= .01f;
        }else{
            smoothTime = .3f;
        }
    }

}
