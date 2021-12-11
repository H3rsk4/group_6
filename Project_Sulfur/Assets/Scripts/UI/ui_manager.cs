using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manager : MonoBehaviour
{
    public static bool isInventoryOn = false;
    public GameObject inventory;
    public GameObject craftingUI;
    public GameObject invText;
    public GameObject pocketsText;
    void Start()
    {
        SetInventoryStatus(isInventoryOn);
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
        craftingUI.SetActive(status);
        invText.SetActive(status);
        pocketsText.SetActive(status);
    }
}
