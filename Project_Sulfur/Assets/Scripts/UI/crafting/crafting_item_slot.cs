using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class crafting_item_slot : MonoBehaviour, IPointerClickHandler
{
    private crafting_ui_script craftingUI;
    private int myIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupVariables(crafting_ui_script _craftingUI, int _myIndex, Sprite _icon){
        craftingUI = _craftingUI;
        myIndex = _myIndex;
        GetComponent<Image>().sprite = _icon;
    }

    public void OnPointerClick(PointerEventData data){
        //craft the item
        craftingUI.craftingRecipes[myIndex].Craft(inventory.instance);

    }
}
