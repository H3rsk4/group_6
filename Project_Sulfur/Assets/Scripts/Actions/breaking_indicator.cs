using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breaking_indicator : MonoBehaviour
{
    private int maxHealth;
    public int currentHealth;
    public float inactiveTime = 600;
    private _Tile tileSO;
    private tile_manager tileManager;

    private Vector3Int cellPosition;
    public healthbar_script healthBar;

    private bool isSetup = false;

    public int axeToughness;
    public int pickToughness;

    // Update is called once per frame
    void Update()
    {
        if(inactiveTime < 0){
            Destroy(this.gameObject);
        }
        inactiveTime -= Time.deltaTime;
    }

    public void SetupValues(int _structureHealth, _Tile _tileSO, tile_manager _tileManager, Vector3Int _cellPosition){
        maxHealth = _structureHealth;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        tileSO = _tileSO;
        axeToughness = tileSO.axeToughness;
        pickToughness = tileSO.pickToughness;
        tileManager = _tileManager;
        cellPosition = _cellPosition;
        
        isSetup = true;
    }

    public void Damage(int value){
        if(isSetup){
            inactiveTime = 600;
            currentHealth -= value;
            healthBar.SetHealth(currentHealth);
            if(currentHealth < 0){
                //destroy tile
                tileManager.ReplaceTile(cellPosition, null, tileManager.maps[1]);
                //destroy self
                Destroy(this.gameObject);
            }
        }
        
    }
}
