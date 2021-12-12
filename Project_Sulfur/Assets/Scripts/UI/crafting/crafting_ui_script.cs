using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crafting_ui_script : MonoBehaviour
{

    public List<_CraftingRecipe> craftingRecipes = new List<_CraftingRecipe>();
    public GameObject slotPrefab;

    public List<crafting_item_slot> itemSlots = new List<crafting_item_slot>();

    public static bool listChecked;

    // Start is called before the first frame update
    void Awake()
    {
        //itemSlots = GetComponentsInChildren<crafting_item_slot>();

        for(int i = 0; i < craftingRecipes.Count; i++){
            GameObject newSlotPrefab = Instantiate(slotPrefab, transform.GetChild(0));
            itemSlots.Add(newSlotPrefab.GetComponentInChildren<crafting_item_slot>());
            //newSlotPrefab.GetComponentInChildren<Image>().sprite = craftingRecipes[i].Results[0].item.icon;
            //Debug.Log(craftingRecipes[i].Results[0].item.icon);
            itemSlots[i].SetupVariables(this, i, craftingRecipes[i].Results[0].item.icon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* DEBUG checking the proximity list
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("checking the list");
            for(int i = 0; i < player_proximity.proximityTiles.Count; i++){
                Debug.Log(player_proximity.proximityTiles[i]);
            }
        }
        */

        if(gameObject.activeSelf){
            if(!listChecked){
                //Debug.Log("hey");
                UpdateCraftingList();

                listChecked = true;
            }
        }
        
    }

    public void UpdateCraftingList(){
        for(int i = 0; i < craftingRecipes.Count; i++){
            //check if we can craft
            if(craftingRecipes[i].requiredTile != null){
                //this recipe needs a tool
                if(player_proximity.proximityTiles.Contains(craftingRecipes[i].requiredTile)){
                    //tool found
                    if(craftingRecipes[i].CanCraft(inventory.instance)){
                        itemSlots[i].transform.parent.gameObject.SetActive(true);
                    }else{
                        itemSlots[i].transform.parent.gameObject.SetActive(false);
                    }
                }else{
                    //tool not found
                    itemSlots[i].transform.parent.gameObject.SetActive(false);
                }

            }else{
                //this repice does not need a tool
                //Debug.Log("here");
                if(craftingRecipes[i].CanCraft(inventory.instance)){
                    //Debug.Log("we can craft this");
                    itemSlots[i].transform.parent.gameObject.SetActive(true);
                }else{
                    itemSlots[i].transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
}
