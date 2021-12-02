using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player_action_animation : MonoBehaviour
{
    public GameObject actionPrefab;
    private SpriteRenderer spriteRenderer;

    [Range(0f,1f)]
    public float time;

    private float speed = 2f;

    private Vector3 startPos;
    private Vector3 midPos;
    public Vector3 endPos;

    private Vector3 flooredEndPos;

    public static bool animationDone = true;
    // Start is called before the first frame update

    private player_use_item playerUseItem;

    public Sprite actionIndicatorSprite;
    void Start()
    {
        actionPrefab.SetActive(false);
        playerUseItem = GetComponent<player_use_item>();
        spriteRenderer = actionPrefab.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time < 1f){
            Vector3 a = Vector3.Lerp(startPos, midPos, time);
            Vector3 b = Vector3.Lerp(midPos, flooredEndPos, time);
            Vector3 c = Vector3.Lerp(a, b, time);
            
            actionPrefab.transform.position = c;
            time += playerUseItem.hotbar.selectedItem.activateSpeed * Time.deltaTime;
        }else if(!animationDone){
            actionPrefab.SetActive(false);
            animationDone = true;
            if(playerUseItem.hotbar.selectedItem.tile != null){
                //placing tile
                playerUseItem.Building(endPos);
            }else{
                //using something else
                playerUseItem.Attacking(endPos);
            }
        }
        
    }

    public void ActionAnimation(Vector3 endPosition){
        

        spriteRenderer.sprite = playerUseItem.hotbar.selectedItem.placeSprite;
    
            
        flooredEndPos = new Vector3((int)Mathf.Floor(endPosition.x) + .5f, (int)Mathf.Floor(endPosition.y) + .5f, 0);


        startPos = transform.position;
        endPos = endPosition;
        midPos = Vector3.Lerp(startPos, endPos, .5f) + new Vector3(0,2f,0);

        time = 0f;
        animationDone = false;

        actionPrefab.SetActive(true);
        
        
    }
}
