using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_action_indicator : MonoBehaviour
{
    public GameObject actionPrefab;
    public LayerMask actionMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int worldMousePos = new Vector3Int((int)Mathf.Floor(mousePos.x), (int)Mathf.Floor(mousePos.y), 0);


        if(Input.GetMouseButtonDown(0)){
            if(!build_button.isHotBar){
                Collider2D actionCollider = Physics2D.OverlapCircle(worldMousePos + new Vector3(.5f,.5f,0), .1f, actionMask);
                if(actionCollider == null){
                    GameObject newActionPrefab = Instantiate(actionPrefab, worldMousePos, Quaternion.identity);
                    newActionPrefab.GetComponent<action_indicator>().SetupValues(1, 1f, 0, transform);
                }else if(actionCollider.transform.GetComponent<action_indicator>() == null){
                    GameObject newActionPrefab = Instantiate(actionPrefab, worldMousePos, Quaternion.identity);
                    newActionPrefab.GetComponent<action_indicator>().SetupValues(1, 1f, 0, transform);
                    
                }
            }
            
            
        }
    }
}
