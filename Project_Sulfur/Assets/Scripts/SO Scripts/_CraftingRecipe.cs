using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [Serializable]
public struct ItemAmount{
    public _Item item;
    [Range(1,999)]
    public int Amount;
}

[CreateAssetMenu(fileName = "Crafting Recipe", menuName = "ScriptableObjects/Crafting Recipe")]
public class _CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    //public List<ItemAmount> ToBeReverted;

    public bool CanCraft(IItemContainer itemContainer){
        //Debug.Log(this);
        //restartLoop:
        foreach(ItemAmount itemAmount in Materials){
            if(itemContainer.ItemCount(itemAmount.item) < itemAmount.Amount){
                //craft child item?
                //if(itemAmount.item.craftingRecipe != null){
                    //if(itemAmount.item.craftingRecipe.CanCraft(itemContainer)){
                        //Debug.Log("mitä täällä tapahtuu  " + this + "   " + itemAmount.item);
                        //itemAmount.item.craftingRecipe.Craft(itemContainer); //if ends up being false, revert this!!
                        //ToBeReverted.Add(itemAmount);
                        //goto restartLoop;
                    //}
                //}
                return false;
                
            }
        }
        return true;
    }

    public void Craft(IItemContainer itemContainer){
        //if(CanCraft(itemContainer)){
            foreach (ItemAmount itemAmount in Materials){
                for(int i = 0; i < itemAmount.Amount; i++){
                    itemContainer.RemoveItem(itemAmount.item);
                }
            }
            foreach (ItemAmount itemAmount in Results){
                for(int i = 0; i < itemAmount.Amount; i++){
                    itemContainer.AddItem(itemAmount.item);
                }
            }

            //ToBeReverted.Clear();
        //}
    }
    /*
    public void UnCraft(IItemContainer itemContainer){
        //remove the item
        //when uncrafting a double crafted item (tnt_water) it returns a tnt even though we crafted it from common materials
        foreach (ItemAmount itemAmount in ToBeReverted){
            foreach(ItemAmount itemAmount1 in itemAmount.item.craftingRecipe.Materials){
                //if(itemAmount1.item.craftingRecipe != null)
                for(int i = 0; i < itemAmount1.Amount; i++){
                    itemContainer.AddItem(itemAmount1.item);
                }
            }
            
            for(int i = 0; i < itemAmount.Amount; i++){
                itemContainer.RemoveItem(itemAmount.item);
            }
            
        }
        ToBeReverted.Clear();
    }
    */
}
