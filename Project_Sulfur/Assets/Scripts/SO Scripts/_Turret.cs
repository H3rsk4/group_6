using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObjects/Tiles/Turret")]
public class _Turret : _Tile
{
    public int ticksToShoot;
    public GameObject projectile;
    public int damage;

    public LayerMask layerMask;

    public override void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender){
        /*
        Collider2D col = Physics2D.OverlapCircle(tileManager.vertices[tileIndex * 4 + 0] + new Vector3(.5f,.5f), 8f, layerMask);
        if(col != null){
            tileManager.entityUpdater.saveValues[entityIndex]++;
        }

        if(tileManager.entityUpdater.saveValues[entityIndex] >= ticksToShoot){
            int invIndex = tileManager.invManager.findChildInventory(tileIndex);
            if(tileManager.invManager.motherInventory.childInventories[invIndex].containers[0].item != null){
                GameObject currentProjectile = Instantiate(projectile, tileManager.vertices[tileIndex * 4 + 0] + new Vector3(.5f,.5f), Quaternion.identity);
                if(currentProjectile.GetComponent<projectile_script>() != null){
                    currentProjectile.GetComponent<projectile_script>().target = col.transform.position;
                    currentProjectile.GetComponent<projectile_script>().damage = damage;
                }
                tileManager.invManager.motherInventory.childInventories[invIndex].containers[0].item = null;
            }

            
            

            tileManager.entityUpdater.saveValues[entityIndex] = 0;
        }
        */
    }
}
