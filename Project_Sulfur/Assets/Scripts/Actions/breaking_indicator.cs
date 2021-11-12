using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breaking_indicator : MonoBehaviour
{
    private int structureHealth;
    public float inactiveTime = 600;
    private _Tile tileSO;
    private tile_manager tileManager;

    private Vector3Int cellPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inactiveTime < 0){
            Destroy(this.gameObject);
        }
        inactiveTime -= Time.deltaTime;
    }

    public void SetupValues(int _structureHealth, _Tile _tileSO, tile_manager _tileManager, Vector3Int _cellPosition){
        structureHealth = _structureHealth;
        tileSO = _tileSO;
        tileManager = _tileManager;
        cellPosition = _cellPosition;
    }

    public void Damage(int value){
        inactiveTime = 600;
        structureHealth -= value;
        if(structureHealth < 0){
            //destroy tile
            tileManager.ReplaceTile(cellPosition, null, tileManager.maps[1]);
            //destroy self
            Destroy(this.gameObject);
        }
    }
}
