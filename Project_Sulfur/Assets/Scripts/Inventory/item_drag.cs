using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class item_drag : MonoBehaviour
{
    public static Transform itemDrag;
    public static _Item draggedItem;
    private Image image;

    public static bool isDragging;

    void Start(){
        itemDrag = transform;
        image = GetComponent<Image>();
    }

    void Update(){
        if(draggedItem != null){
            if(draggedItem.icon != null){
                    if(image.sprite != draggedItem.icon){
                        image.sprite = draggedItem.icon;
                    }
                }
        }

        if(isDragging){
            image.enabled = true;
        }else{
            image.enabled = false;
        }
        
    }
}
