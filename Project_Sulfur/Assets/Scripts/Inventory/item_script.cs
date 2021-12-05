using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class item_script : MonoBehaviour/*, IPointerClickHandler*/, IDragHandler, IEndDragHandler, IDropHandler
{
    public const int MAX_AMOUNT = 999;
    public int currentAmount = 0;

    public Text textAmount;

    public _Item item;

    private Image image;

    public Sprite emptyItem;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        item_drag.isDragging = false;
        textAmount = transform.parent.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(item != null){
            if(item.icon != image.sprite){
                image.sprite = item.icon;
            }
        }else{
            image.sprite = emptyItem;
        }

        if(textAmount != null){
            if(currentAmount <= 1){
                textAmount.enabled = false;
            }else{
                textAmount.enabled = true;
                if(int.Parse(textAmount.text) != currentAmount){
                    textAmount.text = currentAmount.ToString();
                }
            }
            
        }
    }

    /*
    public void OnPointerClick(PointerEventData data){
        //select the item
        if(item != null){
            if(item != build_button.currentItem){
                if(item.tile != null){
                    build_button.currentItem = item;
                    build_button.currentAmount = currentAmount;
                    //deactivate inventory?
                    drop_down.isInventory = false;
                    transform.parent.parent.gameObject.SetActive(false);
                }
                
            }
            
        }

        if(item_drag.isDragging){
            item_drag.draggedItem = null;
            item_drag.isDragging = false;
        }
        
    }
    */
    public void OnDrag(PointerEventData data){
        //check so that "materials" cannot be dragged
        if(item != null){
            image.color = new Color(1,1,1,.5f);
            //if(item.tile != null){
            //grab this item and drag it
            item_drag.isDragging = true;
                if(item_drag.isDragging){
                    item_drag.draggedItem = item;
                    item_drag.currentAmount = currentAmount;
                    item_drag.itemDrag.position = Input.mousePosition;
                }
            //}
        }
        
    }

    public void OnEndDrag(PointerEventData data){
        //Debug.Log("EndDrag");
        image.color = new Color(1,1,1,1f);
        if(item_drag.isDragging){
            item_drag.draggedItem = null;
            item_drag.isDragging = false;
        }
    }

    public void OnDrop(PointerEventData data){
        //Debug.Log ("Dropped object was: "  + data.pointerDrag);
        //Debug.Log(data.pointerDrag.GetComponent<item_script>().item);
        if(item == null){
            item_drag.isDragging = false;
            if(!item_drag.isDragging){
                item = item_drag.draggedItem;
                currentAmount = item_drag.currentAmount;
                //item_drag.dragItem.transform.position = Input.mousePosition;
                item_script iventoryItem = data.pointerDrag.GetComponent<item_script>();
                item_hotbar hotbarItem = data.pointerDrag.GetComponent<item_hotbar>();

                if(iventoryItem != null){
                    iventoryItem.item = null;
                    iventoryItem.currentAmount = 0;
                }else{
                    hotbarItem.item = null;
                    hotbarItem.currentAmount = 0;
                    hotbarItem.hotbarScript.SetSelectedItem();
                }
            }
        }else if(item == item_drag.draggedItem){
            item_drag.isDragging = false;
            if(!item_drag.isDragging){
                currentAmount = currentAmount + item_drag.currentAmount;
                //item_drag.dragItem.transform.position = Input.mousePosition;
                item_script iventoryItem = data.pointerDrag.GetComponent<item_script>();
                item_hotbar hotbarItem = data.pointerDrag.GetComponent<item_hotbar>();

                if(iventoryItem != null){
                    iventoryItem.item = null;
                    iventoryItem.currentAmount = 0;
                }else{
                    hotbarItem.item = null;
                    hotbarItem.currentAmount = 0;
                    hotbarItem.hotbarScript.SetSelectedItem();
                }
            }
        }
        
    }


    
    
}
