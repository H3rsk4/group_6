using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crafting_ui_script : MonoBehaviour
{
    public List<_CraftingRecipe> craftingRecipes = new List<_CraftingRecipe>();
    public GameObject slotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < craftingRecipes.Count; i++){
            GameObject newSlotPrefab = Instantiate(slotPrefab, transform.GetChild(0));
            //newSlotPrefab.GetComponentInChildren<Image>().sprite = craftingRecipes[i].Results[0].item.icon;
            //Debug.Log(craftingRecipes[i].Results[0].item.icon);
            newSlotPrefab.GetComponentInChildren<crafting_item_slot>().SetupVariables(this, i, craftingRecipes[i].Results[0].item.icon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
