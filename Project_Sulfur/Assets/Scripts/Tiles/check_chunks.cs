using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_chunks : MonoBehaviour
{
    public static check_chunks instance;
    public static bool isCheckingChunks;

    public Transform[] checkChunks = new Transform[9];
    public tile_manager[] tileManagers = new tile_manager[9];
    //public Transform checkChunk;

    [SerializeField] private LayerMask layerMask;
    private Collider2D[] hitColliders = new Collider2D[9];

    [Range(1,10)]
    public int multiplier = 1;

    public Transform target;
    private int offsetX = 0;
    private int offsetY = 0;

    public Transform chunkParent;
    public GameObject chunkPrefab;

    

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        for(int i = 0; i < 4; i++){
            Instantiate(checkChunk, transform);
            checkChunks[i] = checkChunk;

        }
        */
        Invoke("CheckChunks", .5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(Mathf.Floor(target.position.x / multiplier) * multiplier,Mathf.Floor(target.position.y/ multiplier) * multiplier ,0);
        if(newPosition != transform.position){
            transform.position = newPosition;

            CheckChunks();
            //world positions of the colliders,chunk checkers, etc.
            //Debug.Log(transform.TransformPoint(checkChunks[0].position) - transform.position);
        }
    }

    private void CheckChunks(){
        isCheckingChunks = true;

        for(int i = 0; i < checkChunks.Length; i++){
            //Debug.Log("checkchunks");
            hitColliders[i] = Physics2D.OverlapCircle(transform.TransformPoint(checkChunks[i].position) - transform.position, 1, layerMask);
            if(hitColliders[i] != null){
                //lets trade our script references
                tileManagers[i] = hitColliders[i].transform.GetComponent<tile_manager>();
                hitColliders[i].transform.GetComponent<entity_updater>().chunkChecker = this;
            }else{
                //spawn new chunk
                GameObject newChunk = Instantiate(chunkPrefab, chunkParent);
                if(newChunk.GetComponent<basic_world_generation_script>() != null){
                    newChunk.GetComponent<basic_world_generation_script>().offsetX = (int)checkChunks[i].position.x;
                    newChunk.GetComponent<basic_world_generation_script>().offsetY = (int)checkChunks[i].position.y;
                }
                if(newChunk.GetComponent<tile_manager>() != null){
                    tileManagers[i] = newChunk.GetComponent<tile_manager>();
                }
                
                //null exception
                //hitColliders[i].transform.GetComponent<entity_updater>().chunkChecker = null;

            }
            
        }
        //hitCollider = Physics2D.OverlapCircle(transform.TransformPoint(checkChunks[0].position) - transform.position, 1, layerMask);

        Invoke("InvokeBool", .5f);
        
    }

    public void InvokeBool(){
        isCheckingChunks = false;
    }

    public tile_manager CheckTileManager(Vector3 currentPos){
        tile_manager trueTileManager = null;
        
        if(currentPos.x > 0){
            offsetX = 1;
        } else {
            offsetX = 0;
        }
        
        if(currentPos.y > 0){
            offsetY = 1;
        } else {
            offsetY = 0;
        }
        Vector3 flooredPos = new Vector3(Mathf.Round((currentPos.x + offsetX) / multiplier) * multiplier,Mathf.Round((currentPos.y + offsetY) / multiplier) * multiplier,0);
        //Debug.Log(flooredPos);
        foreach (tile_manager tileManager in tileManagers)
        {
            if(tileManager != null){
                if(flooredPos == tileManager.trueCenter){
                    //use this tileManager
                    trueTileManager = tileManager;
                }
            }
            
        }
        return trueTileManager;
    }
}
