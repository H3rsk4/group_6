using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manager : MonoBehaviour
{
    public static bool isInventoryOn = false;
    public GameObject inventory;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            isInventoryOn = !isInventoryOn;
            SetInventoryStatus(isInventoryOn);
        }
        
    }

    private void SetInventoryStatus(bool status){
        inventory.SetActive(status);
    }
}
