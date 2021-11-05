using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class container_script : MonoBehaviour
{
    public bool isContainer;
    public static container_script instance;
    private GameObject Container;
    private Text header;
    private Button closeButton;

    private GameObject ItemSlot;

    public List<item_container> itemContainers = new List<item_container>();
    // Start is called before the first frame update
    void Awake(){
        instance = this;
    }

    void Start()
    {
        isContainer = false;
        Container = transform.GetChild(0).gameObject;

        header = Container.transform.GetChild(2).GetComponent<Text>();

        closeButton = GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(ExitContainer);

        //get new amount of slots when we update the UI
        transform.GetComponentsInChildren<item_container>(true, itemContainers);

        ItemSlot = Container.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isContainer && !Container.activeSelf){
            //Debug.Log("this should happen once");
            Container.SetActive(true);
            build_button.isHotBar = true;
            drop_down.isInventory = true;

        }
        if(!isContainer && Container.activeSelf){
            //Debug.Log("this should also happen once");
            Container.SetActive(false);
        }
    }

    private void ExitContainer(){
        isContainer = false;
    }

    public void SetUpContainer(string name, chunk_inventory_manager invManager, int containerIndex){
        //set up name
        if(name != null){
            header.text = name;
        }

        //clear list and remove old itemSlots
        for(int i = 1; i < itemContainers.Count; i++){
            Destroy(itemContainers[i].transform.parent.gameObject);
        }


        itemContainers.Clear();
        

        for(int i = 0; i < invManager.motherInventory.childInventories[containerIndex].containers.Count - 1; i++){
            GameObject currentItemSlot = Instantiate(ItemSlot, ItemSlot.transform.parent);
        }

        transform.GetComponentsInChildren<item_container>(true, itemContainers);

        int slotIndex = 0;
        foreach(item_container itemContainer in itemContainers){
            itemContainer.invManager = invManager;
            itemContainer.containerIndex = containerIndex;
            itemContainer.slotIndex = slotIndex;
            slotIndex++;
        }

    }
}
