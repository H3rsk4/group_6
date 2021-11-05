using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class item_hotbar : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public _Item item;

    private Image image;

    public Sprite emptyIcon;

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
        if(item != null){
            if(item != build_button.currentItem){
                build_button.currentItem = item;
            }
            
        }
        
    }
    public void OnDrop(PointerEventData data){
        //Debug.Log ("Dropped object was: "  + pointerEventData.pointerDrag);
        item_drag.isDragging = false;
        if(!item_drag.isDragging){
            item = item_drag.draggedItem;
            //item_drag.dragItem.transform.position = Input.mousePosition;
        }
    }
    
}
