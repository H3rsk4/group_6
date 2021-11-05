using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drop_down : MonoBehaviour, IPointerClickHandler
{
    //public _Tile currentItem;

    public GameObject inventory;
    public static bool isInventory = false;
    //public GameObject Inventory;

    void Update(){
        if(!isInventory && inventory.activeSelf){
            inventory.SetActive(false);
        }
        if(isInventory && !inventory.activeSelf){
            inventory.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        //Debug.Log(currentItem);
        isInventory = !isInventory;
    }

}
