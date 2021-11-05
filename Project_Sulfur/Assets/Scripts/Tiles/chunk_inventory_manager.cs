using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//debug?
[Serializable]
public class ItemSlot{ //slot for item and amount
    public _Item item;
    public int Amount;

    public ItemSlot(_Item newItem, int newAmount){
        item = newItem;
        Amount = newAmount;
    }
}
[Serializable]
public class ChildInventory{
    public List<ItemSlot> containers = new List<ItemSlot>(); //size and unit of one(1) inventory
    public int tileIndex;
}

[Serializable]
public class MotherInventory{
    public List<ChildInventory> childInventories = new List<ChildInventory>(); //list of inventorys
}


public class chunk_inventory_manager : MonoBehaviour
{
    
    public MotherInventory motherInventory = new MotherInventory();
    
    //public List<List<ItemSlot>> motherContainers = new List<List<ItemSlot>>();

    // Start is called before the first frame update
    void Start()
    {
        //**Adds a new ChildInventory**
        //motherInventory.childInventories.Add(new ChildInventory());

        //**Adds a new itemSlot in containers**
        //motherInventory.childInventories[0].containers.Add(new ItemSlot(null,5));

        /* 
        THIS HONKER OF CODE MAKES TWO INVENTORIES. FIRST ONE HOLDS 2 SLOTS
        AND SECOND ONE THREE.

        motherInventory.childInventories.Add(new ChildInventory());
        motherInventory.childInventories.Add(new ChildInventory());
        motherInventory.childInventories[0].containers.Add(new ItemSlot(null,5));
        motherInventory.childInventories[0].containers.Add(new ItemSlot(null,5));
        motherInventory.childInventories[1].containers.Add(new ItemSlot(null,5));
        motherInventory.childInventories[1].containers.Add(new ItemSlot(null,5));
        motherInventory.childInventories[1].containers.Add(new ItemSlot(null,5));
        */

        //motherInventory.childInventories[0].containers[0].Amount;
        //motherInventory.childInventories[0].containers[0].item;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateContainer(int tileIndex, int slotCount){
        motherInventory.childInventories.Add(new ChildInventory());
        motherInventory.childInventories[motherInventory.childInventories.Count-1].tileIndex = tileIndex;
        for(int i = 0; i < slotCount; i++){
            motherInventory.childInventories[motherInventory.childInventories.Count-1].containers.Add(new ItemSlot(null,0));
        }
    }

    public void RemoveContainer(int childIndex){
        motherInventory.childInventories.RemoveAt(childIndex);
    }

    public int findChildInventory(int tileIndex){
        int childIndex = 0;
        for(int i = 0; i < motherInventory.childInventories.Count; i++){
            if(motherInventory.childInventories[i].tileIndex == tileIndex){
                childIndex = i;
            }
        }
        return childIndex;
    }
}
