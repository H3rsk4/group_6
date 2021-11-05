using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scoreboard_handler_script : MonoBehaviour, IPointerClickHandler
{
    public static bool isScoreboard = false;
    public GameObject scoreboard;

    //public playfabManager pfManager;

    public item_script itemScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isScoreboard && !scoreboard.activeSelf){
            //show scoreboard
            scoreboard.SetActive(true);

            
            
            //pfManager.GetLeaderboard();
        }
        if(!isScoreboard && scoreboard.activeSelf){
            if(itemScript != null){
                //pfManager.SendLeaderboard(itemScript.currentAmount);
            }

            //don't show scoreboard
            foreach(Transform child in scoreboard.transform){
                //yeet the children
                Destroy(child.gameObject);
            }
            scoreboard.SetActive(false);

            
        }
    }

    public void OnPointerClick(PointerEventData data){
        isScoreboard = !isScoreboard;
    }
}
