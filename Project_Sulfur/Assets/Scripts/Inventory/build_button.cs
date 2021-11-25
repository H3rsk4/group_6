using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class build_button : MonoBehaviour, IPointerClickHandler
{
    public static _Item currentItem;
    public static _Tile currentTile;
    public static _CraftingRecipe currentCR;

    //copy the inventory amount of the item
    public static int currentAmount;

    public static int tileOrientation = 0;

    public Image image;

    public drop_down dropDown;
    public GameObject hotBar;
    public GameObject inventory;
    public static bool isHotBar = true;
    
    //public GameObject Inventory;


    //********BUILD BUTTONS**********
    public static bool isDemolish = false;

    public GameObject demolishButton;
    public GameObject buildButton;

    public GameObject rotationButton;

    public Sprite dButton;
    public Sprite dButtonSelected;
    private Image dButtonImage;

    public Sprite bButton;
    public Sprite bButtonSelected;
    private Image bButtonImage;

    public Sprite emptyIcon;

    
    

    void Start(){
        //get them
        dropDown = transform.parent.GetComponentInChildren<drop_down>();
        inventory = transform.parent.GetComponentInChildren<inventory>().transform.gameObject;
        hotBar = dropDown.transform.parent.gameObject;
        image = transform.GetChild(0).GetComponent<Image>();



        demolishButton = transform.GetChild(1).gameObject;
        buildButton = transform.GetChild(2).gameObject;
        rotationButton = transform.GetChild(3).gameObject;

        dButtonImage = demolishButton.GetComponent<Image>();
        bButtonImage = buildButton.GetComponent<Image>();

        Button dButton = demolishButton.GetComponent<Button>();
        dButton.onClick.AddListener(() => BnDButtons(0));

        Button bButton = buildButton.GetComponent<Button>();
        bButton.onClick.AddListener(() => BnDButtons(1));

        Button rButton = rotationButton.GetComponent<Button>();
        rButton.onClick.AddListener(RotationButton);

        //hide them
        hotBar.SetActive(false);
        inventory.SetActive(false);
        drop_down.isInventory = false;

        demolishButton.SetActive(false);
        buildButton.SetActive(false);
        rotationButton.SetActive(false);
    }

    void Update(){
        if(currentItem != null){
            if(currentItem.icon != image.sprite){
                image.sprite = currentItem.icon;
            }

            if(currentItem.craftingRecipe != null){
                if(currentItem.craftingRecipe != currentCR){
                    currentCR = currentItem.craftingRecipe;
                }
                
            }else{
                currentCR = null;
            }

            if(currentItem.tile != null){
                if(currentItem.tile != currentTile){
                    currentTile = currentItem.tile;
                }
            }
        }else{
            image.sprite = emptyIcon;
        }

        if(isHotBar && !hotBar.activeSelf){
            hotBar.SetActive(true);
            //inventory.SetActive(true);

            demolishButton.SetActive(true);
            buildButton.SetActive(true);
            isDemolish = true;
            BnDButtons(1);

            rotationButton.SetActive(true);
        }
        if(!isHotBar && hotBar.activeSelf){
            hotBar.SetActive(false);
            inventory.SetActive(false);
            drop_down.isInventory = false;

            demolishButton.SetActive(false);
            buildButton.SetActive(false);

            rotationButton.SetActive(false);

            container_script.instance.isContainer = false;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        //Debug.Log(currentItem);
        isHotBar = !isHotBar;
    }

    private void BnDButtons(int button){
        //Debug.Log(button);
        if(button == 1 && isDemolish){
            //selecting build
            isDemolish = false;

            bButtonImage.sprite = bButtonSelected;
            dButtonImage.sprite = dButton;

        }
        if(button == 0 && !isDemolish){
            //selecting demolish
            isDemolish = true;

            dButtonImage.sprite = dButtonSelected;
            bButtonImage.sprite = bButton;
        }
    }

    private void RotationButton(){
        
        rotationButton.transform.Rotate(0,0,-90);
        if(tileOrientation < 3){
            tileOrientation++;
        }else{
            tileOrientation = 0;
        }

        //Debug.Log(tileOrientation);
        
    }
}
