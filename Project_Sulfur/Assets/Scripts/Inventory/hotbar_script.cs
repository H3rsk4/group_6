using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hotbar_script : MonoBehaviour
{
    private item_hotbar[] itemHotbars;
    public Sprite selectedImage;
    public Sprite notSelectedImage;

    public _Item selectedItem;
    public int selectedIndex = 0;
    public int lastSelectedIndex = 1;


    void Start()
    {
        itemHotbars = GetComponentsInChildren<item_hotbar>();
        for (int i = 0; i < itemHotbars.Length; i++)
        {
            itemHotbars[i].myIndex = i;
            itemHotbars[i].hotbarScript = this;
        }
        SetSelectedItem();
    }

    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            //going forward
            selectedIndex = (selectedIndex + 1) % itemHotbars.Length;
            SetSelectedItem();
        }else if (Input.GetAxis("Mouse ScrollWheel") > 0f){
            //going backwards
            selectedIndex = (selectedIndex - 1);
            if(selectedIndex < 0){
                selectedIndex = itemHotbars.Length - 1 ;
            }
            SetSelectedItem();
        }
    }

    public void SetSelectedItem(){

        //Debug.Log("selectedIndex: " + selectedIndex);
        //Debug.Log("lastSelectedIndex: " + lastSelectedIndex);
        if(player_action_animation.animationDone){
            selectedItem = itemHotbars[selectedIndex].item;
            Image itemSlot = itemHotbars[selectedIndex].transform.parent.GetComponent<Image>();
            if(itemSlot != null){
                itemSlot.sprite = selectedImage;
            }

            if(lastSelectedIndex != selectedIndex){
                //setting last selected items border back
                Image lastItemSlot = itemHotbars[lastSelectedIndex].transform.parent.GetComponent<Image>();
                if(lastItemSlot != null){
                    lastItemSlot.sprite = notSelectedImage;
                }

                lastSelectedIndex = selectedIndex;
            }
        }
        
        

        
    }
}
