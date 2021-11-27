using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class item_hotbar : MonoBehaviour, IPointerClickHandler, IDropHandler, IDragHandler, IEndDragHandler
{
    public _Item item;

    private Image image;

    public Sprite emptyIcon;

    public int myIndex;
    public hotbar_script hotbarScript;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(item != null){
            if(item.icon != image.sprite){
                image.sprite = item.icon;
            }
        }else{
            image.sprite = emptyIcon;
        }
    }

    public void OnPointerClick(PointerEventData data){
        //select the item
        if(hotbarScript.selectedIndex != myIndex){
            if(hotbarScript != null){
                hotbarScript.selectedIndex = myIndex;
                hotbarScript.SetSelectedItem();
            }
        }
        
        /*
        if(item != null){
            if(item != build_button.currentItem){
                build_button.currentItem = item;
            }
            
        }
        */

    }
    public void OnDrop(PointerEventData data){
        //Debug.Log ("Dropped object was: "  + data.pointerDrag);
        if(item == null){
            
            item_drag.isDragging = false;
            if(!item_drag.isDragging){
                item = item_drag.draggedItem;
                //item_drag.dragItem.transform.position = Input.mousePosition;
                item_script iventoryItem = data.pointerDrag.GetComponent<item_script>();
                item_hotbar hotbarItem = data.pointerDrag.GetComponent<item_hotbar>();

                if(iventoryItem != null){
                    iventoryItem.item = null;
                }else{
                    hotbarItem.item = null;
                }
                hotbarScript.SetSelectedItem();
            }
        }
        
    }

    public void OnDrag(PointerEventData data){
        //grab this item and drag it
        if(ui_manager.isInventoryOn){
            if(item != null){
                image.color = new Color(1,1,1,.5f);
                item_drag.isDragging = true;
                if(item_drag.isDragging){
                    item_drag.draggedItem = item;
                    item_drag.itemDrag.position = Input.mousePosition;
                }
            }
        }
        
        
        
    }

    public void OnEndDrag(PointerEventData data){
        image.color = new Color(1,1,1,1f);
        if(item_drag.isDragging){
            item_drag.draggedItem = null;
            item_drag.isDragging = false;
        }
    }


    
}
