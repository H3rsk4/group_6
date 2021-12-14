using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_indicator : MonoBehaviour
{
    public get_my_tile_manager getMyTileManager;
    public tile_manager tileManager;
    public int damageAmount;
    
    public float activateSpeed;
    public float activeDuration;
    bool isSet;


    public int breakAmount;
    public int axeToughness;
    public int pickToughness;

    public GameObject breakingIndicatorPrefab;

    public LayerMask actionMask;
    public LayerMask entityMask;

    private Transform caster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupValues(int _damageAmount, float _activateSpeed, float _activeDuration, int _axeToughness, int _pickToughness, Transform _caster){
        damageAmount = _damageAmount;
        activateSpeed = _activateSpeed;
        activeDuration = _activeDuration;
        axeToughness = _axeToughness;
        pickToughness = _pickToughness;
        caster = _caster;
        
        isSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(tileManager == null){
            tileManager = getMyTileManager.tileManager;
        }
        
        if(isSet && tileManager != null){
                //activate: hit or break something

                Vector3Int cellPosition = tileManager.maps[1].WorldToCell(transform.position);
                _Tile currentTile = tile_dictionary.GetTileSO(cellPosition, tileManager.maps[1]);

                //check if there already is a breaking or action indicator
                Collider2D actionCollider = Physics2D.OverlapCircle(transform.position, .1f, actionMask);
                if(actionCollider != null){
                    //Debug.Log("found action");
                    //there already is a action or breaking
                    //find out what type
                    if(actionCollider.transform.GetComponent<action_indicator>() != null){
                        //this is an action indicator

                    }else{
                        // this is a breaking indicator
                        // try to break the tile
                        breaking_indicator currentBreakingIndicator = actionCollider.GetComponent<breaking_indicator>();
                        if(currentBreakingIndicator.axeToughness <= axeToughness || currentBreakingIndicator.pickToughness <= pickToughness){
                            //weaker than tool, strike
                            actionCollider.GetComponent<breaking_indicator>().Damage(damageAmount * (1 + axeToughness/10) * (1 + pickToughness/10));
                        }
                        
                        

                    }
                }else{
                    //Debug.Log("did not find action");
                    // no collider found
                    if(currentTile != null && currentTile.isDemolishable){
                        //check if our tool is stronger than the tile
                        if(currentTile.axeToughness <= axeToughness || currentTile.pickToughness <= pickToughness){
                            //start breaking the tile there is no breaking indicator
                            GameObject newBreakingIndicator = Instantiate(breakingIndicatorPrefab, transform.position, Quaternion.identity);
                            if(newBreakingIndicator.GetComponent<breaking_indicator>() != null){
                                newBreakingIndicator.GetComponent<breaking_indicator>().SetupValues(currentTile.structureHealth, currentTile, tileManager, cellPosition);
                                newBreakingIndicator.GetComponent<breaking_indicator>().Damage(damageAmount * (1 + axeToughness/10) * (1 + pickToughness/10));
                            }
                        }
                        
                        

                    }
                }

                Collider2D[] entityColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1,1), 0f, entityMask);
                foreach (Collider2D col in entityColliders)
                {
                    if(col is BoxCollider2D){
                        if(col.transform.GetComponent<stats>() != null){
                            //Debug.Log("Damaged");
                            col.transform.GetComponent<stats>().Damage(damageAmount, 5f,Vector3.Normalize(col.transform.position - (caster.position)));
                        }
                    }
                    
                }

                
                activeDuration -= Time.deltaTime;
                if(activeDuration < 0){
                    Destroy(this.gameObject);
                }
            
        }
    }

    void Breaking(){

    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(1,1));

    }
}
