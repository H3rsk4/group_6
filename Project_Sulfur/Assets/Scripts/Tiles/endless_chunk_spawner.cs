using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endless_chunk_spawner : MonoBehaviour
{
    public GameObject chunkPrefab;
    public const float maxViewDst = 200;
    public Transform viewer;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunksVisibleInViewDst;

    Dictionary<Vector2, Chunk> chunkDictionary = new Dictionary<Vector2, Chunk>();
    List<Chunk> chunksVisibleLastUpdate = new List<Chunk>();

    private List<Vector3> activeChunks = new List<Vector3>();

    void Start(){
        //size of the chunk
        chunkSize = 10;
        chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst/chunkSize);
    }

    void Update(){


        viewerPosition = new Vector3(viewer.position.x, viewer.position.y,0);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks(){

        for(int i = 0; i < chunksVisibleLastUpdate.Count; i++){
            chunksVisibleLastUpdate[i].UpdateChunk();
        }
        chunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        /*      
        for(int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++){
            for(int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++){
                Vector3 viewedChunkCoord = new Vector3(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset,0);

                if(chunkDictionary.ContainsKey(viewedChunkCoord)){
                    chunkDictionary[viewedChunkCoord].UpdateChunk();
                    if(chunkDictionary[viewedChunkCoord].IsVisible()){
                        chunksVisibleLastUpdate.Add(chunkDictionary[viewedChunkCoord]);
                    }
                }else{
                    chunkDictionary.Add(viewedChunkCoord, new Chunk(viewedChunkCoord, chunkSize, transform, chunkPrefab));
                }

            }
        }
        */
        for(int yOffset = chunksVisibleInViewDst; yOffset >= -chunksVisibleInViewDst; yOffset--){
            for(int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++){
                Vector3 viewedChunkCoord = new Vector3(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset,0);
                if(chunkDictionary.ContainsKey(viewedChunkCoord)){
                    //update chunk
                    /*
                    float viewerDstFromNearestEdge = Mathf.Sqrt(new Bounds(viewedChunkCoord * chunkSize, Vector2.one * chunkSize).SqrDistance(viewerPosition));
                    bool visible = viewerDstFromNearestEdge <= maxViewDst;
                    chunkDictionary[viewedChunkCoord].SetActive(visible);
                    */
                    chunkDictionary[viewedChunkCoord].UpdateChunk();

                    bool foundMatch = false;
                    foreach(Vector3 activeChunk in activeChunks){
                        if(viewedChunkCoord == activeChunk){
                            foundMatch = true;
                            break;
                        }
                        foundMatch = false;
                    }
                    if(!foundMatch){
                        activeChunks.Add(viewedChunkCoord);
                    }
                    //check if its already visible
                    /*
                    if(chunkDictionary[viewedChunkCoord].activeSelf == true){
                        //chunksVisibleLastUpdate.Add(chunkDictionary[viewedChunkCoord]);
                    }
                    */
                    if(chunkDictionary[viewedChunkCoord].IsVisible()){
                        //chunksVisibleLastUpdate.Add(chunkDictionary[viewedChunkCoord]);
                    }
                }else{
                    //add new chunk
                    /*
                    GameObject newChunk = Instantiate(chunkPrefab, transform);
                    Vector2 position = viewedChunkCoord * chunkSize;
                    newChunk.transform.position = new Vector3(position.x, position.y,0);
                    chunkDictionary.Add(viewedChunkCoord, newChunk);
                    */
                    chunkDictionary.Add(viewedChunkCoord, new Chunk(viewedChunkCoord, chunkSize, transform, chunkPrefab));
                }

            }
        }

        foreach(Vector3 activeChunk in activeChunks){
            chunkDictionary[activeChunk].UpdateChunk();
        }
        

    }


    public class Chunk{

        GameObject chunkObject;
        Vector2 position;
        Bounds bounds;

        public Chunk(Vector3 coord, int size, Transform parent, GameObject _chunkPrefab){
            //

            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x,position.y,0);

            //?
            chunkObject = Instantiate(_chunkPrefab, parent);

            //instantiate
            //Instantiate(chunkObject,positionV3,Quaternion.identity,parent);
            //chunkObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            
            chunkObject.transform.position = positionV3;
            //chunkObject.transform.eulerAngles = new Vector3(-90,0,0);
            //chunkPrefab.transform.localScale = Vector3.one * size / 10f;
            //chunkObject.transform.parent = parent;
            SetVisible(false);
        }


        public void UpdateChunk(){
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDstFromNearestEdge <= maxViewDst;
            SetVisible(visible);

            //new Bounds(viewedChunkCoord * chunkSize, Vector2.one * chunkSize)
            //float viewerDstFromNearestEdge = Mathf.Sqrt(new Bounds(viewedChunkCoord * chunkSize, Vector2.one * chunkSize).SqrDistance(viewerPosition));
            //bool visible = viewerDstFromNearestEdge <= maxViewDst
            //gameobject.SetActive(visible);

        }
        public void SetVisible(bool visible){
            chunkObject.SetActive(visible);
        }

        public bool IsVisible(){
            return chunkObject.activeSelf;
        }

    }

}
