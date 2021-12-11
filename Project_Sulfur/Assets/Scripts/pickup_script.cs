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

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 midPos;
    private float time = 0;
    void Start()
    {
        target = player.playerT;
        //inv = inventory.inventoryT.GetComponent<inventory>();
        distance = Vector3.Distance(target.position, transform.position);
        startPos = transform.position;
        Vector3 endPosOffset = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0);
        endPos = startPos + endPosOffset;
        //Debug.Log(endPosOffset);
        midPos = Vector3.Lerp(startPos, endPos, .5f) + new Vector3(0f,1f,0f);
    }

    public void SetupItem(_Item _item, int _itemAmount){
        item = _item;
        itemAmount = _itemAmount;

        GetComponent<SpriteRenderer>().sprite = _item.icon;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        //Debug.Log(direction);
        Vector3 a = Vector3.Lerp(startPos, midPos, time);
        Vector3 b = Vector3.Lerp(midPos, endPos, time);
        Vector3 c = Vector3.Lerp(a, b, time);

        if(time < 1f && !isPickingUp){
            time += 2 * Time.deltaTime;
            transform.position = c;
        }
    }

    void FixedUpdate(){
        if(distance < item.pickUpDistance){
            isPickingUp = true;
        }
        if(distance < .5f){
            //pickup
            if(!inventory.instance.IsFull()){
                //Pickup Sound!

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
