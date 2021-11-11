using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class tile_manager : MonoBehaviour
{

    public Tilemap map;
    public Tilemap[] maps;

    public int tileAmount = 0;
    

    bool isDuplicate;
    public bool isPlacing = true;
    //public bool isMoving;
    public bool showOutline = false;

    public int tiles;

    public Vector3Int mousePosition;

    public _Tile water;
    public _Tile ground;
    public _Tile wall;
    public _Tile tnt;
    public _Tile tnt_water;
    public _Tile currentTile;




    [SerializeField] private LayerMask layerMask;
    public tile_manager[] Neighbours = new tile_manager[9];
    private Collider2D[] hitColliders = new Collider2D[8];

    public entity_updater entityUpdater;
    public chunk_inventory_manager invManager;

    //public Transform chunkChecker;
    //public check_chunks checkChunks;

    public BoxCollider2D boxCollider;

    public Vector3 trueCenter;

    private basic_world_generation_script basicWorldGen;
    void Start()
    {
        //map = GetComponent<Tilemap>();

        //meshRenderer = GetComponent<MeshRenderer>();
        
        currentTile = water;

        entityUpdater = GetComponent<entity_updater>();
        invManager = GetComponent<chunk_inventory_manager>();

        //mesh = GetComponent<MeshFilter>().mesh;
        boxCollider = GetComponent<BoxCollider2D>();
        basicWorldGen = GetComponent<basic_world_generation_script>();
        //Debug.Log(colliders[0].transform.position);
        trueCenter = new Vector3(basicWorldGen.offsetX, basicWorldGen.offsetY, 0);
        boxCollider.offset = new Vector2(basicWorldGen.offsetX, basicWorldGen.offsetY);
        
        
        //too slow
        Invoke("GetNeighbours", .5f);
    }

    



    // As it is called in the method name, this changes the tile to desired tile
    public void ReplaceTile(Vector3Int tilePosition, TileBase tile, Tilemap _map){
        
        _Tile tileSO = tile_dictionary.GetTileSO(tilePosition, _map);

        if(tileSO != null){
            if(tileSO.hasMultipleTiles){
                //destroy tileparts
                for(int i = 0; i < tileSO.tileParts.Length; i++){
                    maps[2].SetTile(tilePosition + tileSO.tilePartPositions[i], null);
                }
            }
        }

        _map.SetTile(tilePosition, tile);

        _Tile placedTile = tile_dictionary.GetTileSO(tilePosition, _map);
        if(placedTile != null){
            if(placedTile.hasMultipleTiles){
                //build tileparts
                for(int i = 0; i < placedTile.tileParts.Length; i++){
                    maps[2].SetTile(tilePosition + placedTile.tilePartPositions[i], placedTile.tileParts[i]);
                }
            }
        }
        
    }



    // finds tilemanager scripts from other chunks by checking collisions
    private void GetNeighbours(){
            /*
            //self
            Neighbours[0] = this;

            //right
            hitColliders[0] = Physics2D.OverlapCircle(trueCenter + new Vector3(10,0,0), 1, layerMask);
            if(hitColliders[0] != null){
                Neighbours[1] = hitColliders[0].transform.GetComponent<tile_manager>();
            }

            //left
            hitColliders[1] = Physics2D.OverlapCircle(trueCenter + new Vector3(-10,0,0), 1, layerMask);
            if(hitColliders[1] != null){
                Neighbours[2] = hitColliders[1].transform.GetComponent<tile_manager>();
            }

            //top
            hitColliders[2] = Physics2D.OverlapCircle(trueCenter + new Vector3(0,10,0), 1, layerMask);
            if(hitColliders[2] != null){
                Neighbours[3] = hitColliders[2].transform.GetComponent<tile_manager>();
            }

            //bottom
            hitColliders[3] = Physics2D.OverlapCircle(trueCenter + new Vector3(0,-10,0), 1, layerMask);
            if(hitColliders[3] != null){
                Neighbours[4] = hitColliders[3].transform.GetComponent<tile_manager>();
            }
            */
            int i = 0;
            for(int x = 0; x < Mathf.Sqrt(Neighbours.Length); x++){
                for(int y = 0; y < Mathf.Sqrt(Neighbours.Length); y++){
                    
                    //vector3((x * chunkWidth) - chunkWidth, (y * chunkHeight) - chunkHeight, 0)
                    Collider2D currentCollider = Physics2D.OverlapCircle(transform.position + new Vector3Int((x * basicWorldGen.worldWidth) - basicWorldGen.worldWidth, (y * basicWorldGen.worldHeight) - basicWorldGen.worldHeight, 0), 1, layerMask);
                    if(currentCollider != null){
                        //add this chunk's tilemanager to neighbours
                        Neighbours[i] = currentCollider.transform.GetComponent<tile_manager>();

                        currentCollider.transform.GetComponent<tile_manager>().Neighbours[Mathf.Abs(i - 8)] = this;

                    }
                    i++;
                }
            }
    }

    // This method tries to find a correct tilemanager with a given position from the neighbours list
    public tile_manager CheckTileManager(Vector3 currentPos){
        tile_manager trueTileManager = null;
        int offsetX = 0;
        int offsetY = 0;
        
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
        //ToPositiveInfinity
        //double modifiedX = currentPos.x / 10;
        //System.Math.Round(modifiedX, MidpointRounding.ToPositiveInfinity);
        Vector3 flooredPos = new Vector3(Mathf.Round(currentPos.x / 10 + .01f) * 10,Mathf.Round(currentPos.y / 10 + .01f)  * 10,0);
        //Debug.Log(flooredPos);
        
        foreach (tile_manager tileManager in Neighbours)
        {
            if(tileManager != null){
                if(tileManager.transform.position == flooredPos){
                    //use this tileManager
                    trueTileManager = tileManager;
                }
            }
            
        }
        
        //Debug.Log(this + "\n" + "tilemanager: " + trueTileManager + " " + flooredPos);
        if(trueTileManager != null){
            Debug.Log("currentPos: " + currentPos);
            Debug.Log(this + " " + transform.position + "\n" + "new tilemanager: " + trueTileManager + " " + trueTileManager.transform.position);
            
        }
        return trueTileManager;
    }

    

    //Chooses the correct input regarding if it is a touch or a mouseinput
    public Vector3 GetInputPosition(){
        Vector3 newPos = new Vector3(0,0,0);
        if(mouse_touch_controller.instance.debugMouse){
            //mouseInput
            if(mouse_touch_controller.isBuilding){
                newPos = Input.mousePosition;
            }
        }else{
            //touchInput
            if(mouse_touch_controller.isBuilding){
                Touch touch = Input.GetTouch(mouse_touch_controller.buildIndex);
                newPos = touch.position;
                
            }
        }
        return newPos;
    }



        //U   U PPPP  DDD   AA  TTTTTT EEEE 
        //U   U P   P D  D A  A   TT   E    
        //U   U PPPP  D  D AAAA   TT   EEE  
        //U   U P     D  D A  A   TT   E    
        // UUU  P     DDD  A  A   TT   EEEE 
                                      


    // Update is called once per frame
    void Update()
    {
        
        if(check_chunks.isCheckingChunks){
            GetNeighbours();
        }
        
        
        
        /*
        foreach(Transform chunk in checkChunks.checkChunks){
            if(chunk != null){
                if(mesh.bounds.center == chunk.position){
                    if(!check_chunks.tileManagers.Contains(this)){
                        check_chunks.tileManagers.Add(this);
                        break;
                    }
                } else {
                    //if(checkChunks.tileManagers.Contains(this)){
                        //checkChunks.tileManagers.Remove(this);
                    //}
                }
            }
            
        }
        */
        /*
        if(Input.touchCount != myTouchCount){
            //this will happen everytime Input.touchCount changes
            //Debug.Log("touchCount change!");
            myTouchCount = Input.touchCount;
            Debug.Log(ui_joystick.joystickTouchIndex);
            for (int i = 0; i < Input.touchCount; i++)
            {
                //Touch currentTouch = Input.GetTouch(i);

                //problem with this (null int being 0)
                if(i != ui_joystick.joystickTouchIndex){
                    //Debug.Log("we did get here");
                    buildTouchIndex = i;
                    break;
                }
            }
            
        }
        
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(buildTouchIndex);
        */
        
        
        

        //MOUSEPOSGRID IMPORTANT
        //TRANSLATES MOUSEPOSITION TO 2D
        Vector3 mousePos = Input.mousePosition;
        //Vector3 mousePos = GetInputPosition();
        //Vector3 mousePos = touch.position;
        
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int worldMousePos = new Vector3Int((int)mousePos.x,(int)mousePos.y,0);
        Vector3Int mousePosGrid = maps[1].WorldToCell(mousePos);
        mousePosition = mousePosGrid;
        /*
        if(Input.GetKeyDown(KeyCode.G)){
            isPlacing = !isPlacing;
        }
        */
        /*
        if(isPlacing){
            if(Input.GetMouseButton(0)){
            foreach(Vector3 tileposition in tilePositions){
                if(tileposition == mousePosGrid){
                    isDuplicate = true;
                    break;
                }
                isDuplicate = false;
            }
            if(!isDuplicate){
                tilePositions[tileIndex] = mousePosGrid;
                DrawMesh(mousePos);
                tileAmount++;
                tileIndex++;
                Vector3[] temp = new Vector3[tileAmount];
                tilePositions.CopyTo(temp, 0);
                tilePositions = temp;
            }
          
            }
        }
        */
        //****BUILDING****
        if(build_button.isHotBar && !build_button.isDemolish){    
            //if(Input.GetMouseButton(0)){
            if(mouse_touch_controller.isBuilding){
            //if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary){
                //if(!EventSystem.current.IsPointerOverGameObject()){
                    //getting the tile on the ground
                    _Tile currentTileSO = tile_dictionary.GetTileSO(mousePosGrid, maps[1]);
                    _Tile bottomTileSO = tile_dictionary.GetTileSO(mousePosGrid, maps[0]);
                    if(currentTileSO == null){
                        if(currentTileSO != build_button.currentTile){
                            if(build_button.currentTile != null){
                                //build_button.currentCR.Craft(inventory.instance);
                                if(inventory.instance.ItemCount(build_button.currentItem) > 0){
                                    if(PlaceTile(bottomTileSO, mousePosGrid)){
                                        

                                        if(build_button.currentTile.needsUpdate){
                                            //Debug.Log(vertices[tileIndex]);
                                            entityUpdater.tilePositions.Add(mousePosGrid);
                                            entityUpdater.saveValues.Add(0);
                                        }
                                        /*
                                        if(build_button.currentTile.hasContainer){
                                            //add a container
                                            invManager.CreateContainer(tileIndex,build_button.currentTile.containerSize);
                                        } 
                                        */
                                    }
                                
                                } else {
                                    if(build_button.currentCR != null){
                                        if(build_button.currentCR.CanCraft(inventory.instance)){
                                            if(PlaceTile(bottomTileSO, mousePosGrid)){
                                                
                                                if(build_button.currentTile.needsUpdate){ 
                                                    entityUpdater.tilePositions.Add(mousePosGrid);
                                                    entityUpdater.saveValues.Add(0);
                                                }
                                                /*
                                                if(build_button.currentTile.hasContainer){
                                                    //add a container
                                                    invManager.CreateContainer(tileIndex,build_button.currentTile.containerSize);
                                                }
                                                 */
                                                
                                            }
                                            
                                        } else {
                                            //if(build_button.currentCR.ToBeReverted.Count > 0){
                                                //Debug.Log("uncrafting");
                                                //uncraft already crafted
                                                //build_button.currentCR.UnCraft(inventory.instance);
                                            // }
                                            
                                        }
                                    }else{
                                        //No Recipe? just place it, it's probably free
                                        //check if allowed to place on top of
                                        if(PlaceTile(bottomTileSO, mousePosGrid)){
                                            if(build_button.currentTile.needsUpdate){ 
                                                entityUpdater.tilePositions.Add(mousePosGrid);
                                                entityUpdater.saveValues.Add(0);
                                            }
                                        }
                                    }
                                        
                                }
                                
                            }
                        }
                    }
                        
                    
                //}   //ispointer
            }
        }

        //****DECONSTRUCTING****
        if(build_button.isHotBar && build_button.isDemolish){
            //Debug.Log("Demolishing");
            if(mouse_touch_controller.isBuilding){
            //if(Input.GetMouseButton(0)){
                //if(!EventSystem.current.IsPointerOverGameObject()){
                    _Tile currentTileSO = tile_dictionary.GetTileSO(mousePosGrid, maps[1]);
                    if(currentTileSO != null){
                        if(currentTileSO.isDemolishable){
                            //inventory.instance.AddItem(currentTileSO.demolishItem);
                            //uv and tileid change
                            
                            if(currentTileSO.needsUpdate){
                                entityUpdater.removeList.Add(mousePosGrid);
                                entityUpdater.valueRemoveList.Add(entityUpdater.saveValues[entityUpdater.tilePositions.IndexOf(mousePosGrid)]);
                            }
                            /*
                            if(tileSO[tileIndex].hasContainer){
                                //remove the container
                                int childIndex = invManager.findChildInventory(tileIndex);
                                invManager.RemoveContainer(childIndex);
                            }
                            */
                            ReplaceTile(mousePosGrid, null, maps[1]);                           
                        }
                    }
                        
                    
                //}  // is pointer
            } // getmousebutton
        }
        //****INTERACTING****
        if(!build_button.isHotBar){
            //means we are interacting
            if(mouse_touch_controller.isBuilding){
                //CheckTileManager(mousePos);
            //if(Input.GetMouseButtonDown(0)){
                //if(!EventSystem.current.IsPointerOverGameObject()){
                    /*
                    if(CheckTile(mousePosGrid)){
                        int tileIndex = vertexIndex;
                        if(tileSO[tileIndex].isInteractable){
                            //Debug.Log("do something");
                            tileSO[tileIndex].Do(tileIndex,this,0,"tile_manager");
                        }
                    }
                    */
                //} //is pointer
            } // get mouse
        }
        /*
        if(!isPlacing){
            
            if(Input.GetMouseButtonDown(0)){
                if(vertices != null){
                        if(isMoving){
                            //dropping
                            vertices[tileIndex * 4 + 0] = new Vector3(0,0) + mousePosGrid;
                            vertices[tileIndex * 4 + 1] = new Vector3(0,1) + mousePosGrid;
                            vertices[tileIndex * 4 + 2] = new Vector3(1,1) + mousePosGrid;
                            vertices[tileIndex * 4 + 3] = new Vector3(1,0) + mousePosGrid;
                            isMoving = false;
                            return;
                        }
                        if(!isMoving){
                            if(CheckTile(mousePosGrid)){
                                tileIndex = vertexIndex;

                                Debug.Log(vertices[tileIndex * 4+0]);
                                
                                Debug.Log(tileID[tileIndex]);
                                
                                //Debug.Log(uv[tileIndex * 4 + 0]);
                                //Debug.Log(uv[tileIndex * 4 + 1]);
                                //Debug.Log(uv[tileIndex * 4 + 2]);
                                //Debug.Log(uv[tileIndex * 4 + 3]);
                                
                                isMoving = true;
                            }
                    } 
                }
                        
            }

            if(isMoving){
                
                vertices[tileIndex * 4 + 0] = new Vector3(0,0) + mousePosGrid;
                vertices[tileIndex * 4 + 1] = new Vector3(0,1) + mousePosGrid;
                vertices[tileIndex * 4 + 2] = new Vector3(1,1) + mousePosGrid;
                vertices[tileIndex * 4 + 3] = new Vector3(1,0) + mousePosGrid;
                                

                GetComponent<MeshFilter>().mesh.vertices = vertices.ToArray();
            }       
        }
        */
        //}
    }

    public bool PlaceTile(_Tile currentTile, Vector3Int tilePosition){
        if(currentTile != null){
            if(currentTile.buildLayer == 0 && build_button.currentTile.buildLayer == 1){
            
            if(inventory.instance.ItemCount(build_button.currentItem) <= 0){
                if(build_button.currentCR != null){
                    build_button.currentCR.Craft(inventory.instance);
                }
            }
            
            ReplaceTile(tilePosition, build_button.currentTile.tiles[0], maps[1]);

            inventory.instance.RemoveItem(build_button.currentItem);
            return true;
            }
            if(currentTile.buildLayer == 1 && build_button.currentTile.buildLayer == 2){

                if(inventory.instance.ItemCount(build_button.currentItem) <= 0){
                    if(build_button.currentCR != null){
                        build_button.currentCR.Craft(inventory.instance);
                    }
                }

                ReplaceTile(tilePosition, build_button.currentTile.tiles[0], maps[1]);
                /*
                if(build_button.currentTile.hasMultipleTiles){
                    for(int i = 0; i < build_button.currentTile.tileParts.Length; i++){
                        ReplaceTile(tilePosition + build_button.currentTile.tilePartPositions[i], build_button.currentTile.tileParts[i], maps[2]);
                    }
                }
                */

                inventory.instance.RemoveItem(build_button.currentItem);
                return true;
            }
            return false;
        }else{
            return false;
        }
        
    }

    /*
    checktile needs:
    - position(rounded values) where we are checking
    - list of vertices

    maybe we can give the list as a parameter to make it more modular

    */



    /*
    public bool CheckTile(Vector3 mousePos){
        
        //Debug.Log("checktile");
        tileIndexDetect = 0;
            if(vertices != null){
                foreach(Vector3 vertex in vertices){
                    if(vertex == mousePos){
                        vertexIndex = (int)Mathf.Floor(tileIndexDetect/4);
                        tileIndexDetect2 = 0;
                        foreach (Vector3 vertex2 in vertices)
                        {
                            if(vertex2 == new Vector3(1,1) + mousePos){
                                vertexIndex2 = (int)Mathf.Floor(tileIndexDetect2/4);

                                if(vertexIndex == vertexIndex2){
                                    return true;
                            
                                }
                            }
                            tileIndexDetect2++;
                        }
                    }
                    tileIndexDetect++;
                }
            }
            return false;
    }
    */
    
      
    public void OnDrawGizmos(){
        if(showOutline){
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position,new Vector3(10,10,0));
        }
        
    }
              
}
