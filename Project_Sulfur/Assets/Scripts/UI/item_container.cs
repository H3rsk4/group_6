using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class item_container : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    public const int MAX_AMOUNT = 999;
    public int currentAmount = 0;
    public Image image;
    public Text textAmount;
    public chunk_inventory_manager invManager;
    public int containerIndex;
    public int slotIndex;
    public Sprite emptyItem;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(textAmount != null){
            if(int.Parse(textAmount.text) != invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].Amount){
                textAmount.text = invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].Amount.ToString();
            }
        }
        if(invManager != null){
            if(invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].item != null){
                image.sprite = invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].item.icon;
            }else{
                image.sprite = emptyItem;
            }

            
        } 
    }

    public void OnDrag(PointerEventData data){
        //Debug.Log("drag");
        if(invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].item != null){
            item_drag.isDragging = true;
            if(item_drag.isDragging){
                item_drag.draggedItem = invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].item;
                item_drag.itemDrag.position = Input.mousePosition;
            }
        }
        
    }
    public void OnEndDrag(PointerEventData data){
        //Debug.Log("end drag");
        if(item_drag.isDragging){
            item_drag.draggedItem = null;
            item_drag.isDragging = false;
        }
    }
    public void OnDrop(PointerEventData data){
        Debug.Log("drop");
        item_drag.isDragging = false;
        if(invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].item == null){
            invManager.motherInventory.childInventories[containerIndex].containers[slotIndex].item = item_drag.draggedItem;
        }
    }
}
