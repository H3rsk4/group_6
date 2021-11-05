using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class tile_manager : MonoBehaviour
{
    // Start is called before the first frame update

    //****we need a way to replace tiles -> performance friendly

    //checktile when dropping a moving tile has not been implemented

    //implement easier way to add all uv maps

    public Tilemap map;
    public Tilemap[] maps;

    public int tileAmount = 0;
    public int tileIndex = 0;
    public int vertexIndex = 0;
    int vertexIndex2 = 0;
    

    int tileIndexDetect = 0;
    int tileIndexDetect2 = 0;

    private float tileSize = .125f;

    bool isDuplicate;
    public bool isPlacing = true;
    //public bool isMoving;
    public bool showOutline = false;

    public int tiles;


    public _Tile water;
    public _Tile ground;
    public _Tile wall;
    public _Tile tnt;
    public _Tile tnt_water;
    public _Tile currentTile;

    private MeshRenderer meshRenderer;

    //Vector3[] tilePositions = new Vector3[1];
    public List<Vector3> vertices = new List<Vector3>();
    
    List<Vector2> uv = new List<Vector2>();
    List<int> triangles = new List<int>();


    //public List<int> tileID = new List<int>();
    public List<_Tile> tileSO = new List<_Tile>();

    public List<int> orientation = new List<int>();

    [SerializeField] private LayerMask layerMask;
    public tile_manager[] Neighbours = new tile_manager[9];
    private Collider2D[] hitColliders = new Collider2D[8];

    public entity_updater entityUpdater;
    public chunk_inventory_manager invManager;

    //public Transform chunkChecker;
    //public check_chunks checkChunks;

    private Mesh mesh;
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

    

    /*
    private void DrawMesh(Vector3 mousePos){
        //need to make "update mesh" that saves the uv mapping and positions and whatnot


        //need to add meshfilter and meshrenderer
        Mesh mesh = new Mesh();

        vertices = new Vector3[4 * tileAmount];
        Vector2[] uv = new Vector2[4 * tileAmount];
        int[] triangles = new int[6 * tileAmount];

        for(int i = 0; i < tileAmount; i++){
            vertices[i * 4 + 0] = new Vector3(0,0) + tilePositions[i];
            vertices[i * 4 + 1] = new Vector3(0,1) + tilePositions[i];
            vertices[i * 4 + 2] = new Vector3(1,1) + tilePositions[i];
            vertices[i * 4 + 3] = new Vector3(1,0) + tilePositions[i];


            if(changeTile){
                uv[i * 4 + 0] = new Vector2(0,0);
                uv[i * 4 + 1] = new Vector2(0,.5f);
                uv[i * 4 + 2] = new Vector2(.5f,.5f);
                uv[i * 4 + 3] = new Vector2(.5f,0);
            } else {
                uv[i * 4 + 0] = new Vector2(0,.5f);
                uv[i * 4 + 1] = new Vector2(0,1);
                uv[i * 4 + 2] = new Vector2(.5f,1);
                uv[i * 4 + 3] = new Vector2(.5f,.5f);
            }
            

            


            //draw tris always clockwise
            triangles[i * 6 + 0] = i * 4 + 0;
            triangles[i * 6 + 1] = i * 4 + 1;
            triangles[i * 6 + 2] = i * 4 + 2;
            triangles[i * 6 + 3] = i * 4 + 0;
            triangles[i * 6 + 4] = i * 4 + 2;
            triangles[i * 6 + 5] = i * 4 + 3;
        }
        
        
        

        


        GetComponent<MeshFilter>().mesh.vertices = vertices;
        GetComponent<MeshFilter>().mesh.uv = uv;
        GetComponent<MeshFilter>().mesh.triangles = triangles;

        //GetComponent<MeshFilter>().mesh = mesh;
    }
    */

    //this method creates more tiles, not replace them
    public void UpdateMesh(Vector3 mousePos, _Tile currentTile, int currentRotation){
        //need to make "update mesh" that saves the uv mapping and positions and whatnot

        //need to add meshfilter and meshrenderer
        Mesh mesh = new Mesh();



        /*******************RANDOM ROTATIONS**************
        Vector3[] curretVectors = {
            new Vector3(0,0) + mousePos,
            new Vector3(0,1) + mousePos,
            new Vector3(1,1) + mousePos,
            new Vector3(1,0) + mousePos
        };

        Vector3[] newRotation = GetRotationNoTileIndex(currentRotation, curretVectors);


        vertices.Add(newRotation[0]);
        vertices.Add(newRotation[1]);
        vertices.Add(newRotation[2]);
        vertices.Add(newRotation[3]);
        */

        vertices.Add(new Vector3(0,0) + mousePos);
        vertices.Add(new Vector3(0,1) + mousePos);
        vertices.Add(new Vector3(1,1) + mousePos);
        vertices.Add(new Vector3(1,0) + mousePos);
        
        
        


        orientation.Add(currentRotation);

        
        
        uv.Add(new Vector3(0,0) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos));   
        uv.Add(new Vector3(0,tileSize) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos));
        uv.Add(new Vector3(tileSize,tileSize) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos));
        uv.Add(new Vector3(tileSize,0) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos));
        


        //new Vector3(0,0) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos)
        //new Vector3(0,tileSize) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos)
        //new Vector3(tileSize,tileSize) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos)
        //new Vector3(tileSize,0) + new Vector3(tileSize * currentTile.xPos,tileSize * currentTile.yPos)

        //tileID.Add(currentTile.ID);
        tileSO.Add(currentTile);    


        //draw tris always clockwise
        //now when the list is counting from last to 6th last -> it has to be reverse
        
        
        triangles.Add(4 * tileAmount + 0);
        triangles.Add(4 * tileAmount + 1);
        triangles.Add(4 * tileAmount + 2);
        triangles.Add(4 * tileAmount + 0);
        triangles.Add(4 * tileAmount + 2);
        triangles.Add(4 * tileAmount + 3);
        
        /*
        triangles.Add(4 * tileAmount + 3);
        triangles.Add(4 * tileAmount + 2);
        triangles.Add(4 * tileAmount + 0);
        triangles.Add(4 * tileAmount + 2);
        triangles.Add(4 * tileAmount + 1);
        triangles.Add(4 * tileAmount + 0);
        */
        
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();

        GetComponent<MeshFilter>().mesh = mesh;

        tileAmount++;
    }

    //this is used to remove actual tile not needed for gameplay
    private void RemoveTile(int tileIndex){
        
        //Debug.Log(tileIndex);
        Mesh mesh = new Mesh();

            //list fills holes before the next remove happens
            vertices.RemoveAt(4 * tileIndex);
            vertices.RemoveAt(4 * tileIndex);
            vertices.RemoveAt(4 * tileIndex);
            vertices.RemoveAt(4 * tileIndex);

            orientation.RemoveAt(tileIndex);
     
            uv.RemoveAt(4 * tileIndex);   
            uv.RemoveAt(4 * tileIndex);
            uv.RemoveAt(4 * tileIndex);
            uv.RemoveAt(4 * tileIndex);
          
            

            //tileID.RemoveAt(tileIndex);
            tileSO.RemoveAt(tileIndex);


            //draw tris always clockwise
            //triangles have to be in order
            triangles.RemoveAt(triangles.Count - 6);
            triangles.RemoveAt(triangles.Count - 5);
            triangles.RemoveAt(triangles.Count - 4);
            triangles.RemoveAt(triangles.Count - 3);
            triangles.RemoveAt(triangles.Count - 2);
            triangles.RemoveAt(triangles.Count - 1);
            
            
        //}
        
        
        

        

        
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();

        
        GetComponent<MeshFilter>().mesh = mesh;
        
        
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

    public void UpdateUV(int tileIndex, int xAdd, int yAdd){
        
        int tileXpos = GetSpriteRotation(tileIndex,orientation[tileIndex],tileSO[tileIndex]);
        //Debug.Log(tileXpos);
        int tileYpos = tileSO[tileIndex].yPos;

        tileXpos += xAdd;
        tileYpos += yAdd;

        uv[tileIndex * 4 + 0] = new Vector3(0,0) + new Vector3(tileSize * tileXpos,tileSize * tileYpos);
        uv[tileIndex * 4 + 1] = new Vector3(0,tileSize) + new Vector3(tileSize * tileXpos,tileSize * tileYpos);
        uv[tileIndex * 4 + 2] = new Vector3(tileSize,tileSize) + new Vector3(tileSize * tileXpos,tileSize * tileYpos);
        uv[tileIndex * 4 + 3] = new Vector3(tileSize,0) + new Vector3(tileSize * tileXpos,tileSize * tileYpos);



        GetComponent<MeshFilter>().mesh.uv = uv.ToArray();
    }

    public int GetSpriteRotation(int tileIndex, int currentRotation, _Tile tile){
        int tileXpos = tile.xPos;
        int newXpos = 0;
        if(tile.hasMultipleSprites){
            /*
            switch(orientation[tileIndex]){
            case 0:
                break;
            case 1:
                spriteOffset = tileXpos - 1;
                break;
            case 2:
                spriteOffset = tileXpos - 2;
                break;
            case 3:
                spriteOffset = tileXpos - 3;
                break;
            }
            */
            switch(currentRotation){
                case 0:
                    break;
                case 1:
                    newXpos = 1;
                    break;
                case 2:
                    newXpos = 2;
                    break;
                case 3:
                    newXpos = 3;
                    break;
            }

            tileXpos += newXpos;
            return tileXpos;

        }else{
            return tile.xPos;
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
        Vector3 flooredPos = new Vector3(Mathf.Round((currentPos.x + offsetX) / 10) * 10,Mathf.Round((currentPos.y + offsetY) / 10) * 10,0);
        //Debug.Log(flooredPos);
        foreach (tile_manager tileManager in Neighbours)
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

    public Vector3[] GetRotation(int currentRotation, Vector3[] currentVectors, int tileIndex){
        Vector3[] newRotation = new Vector3[4];
        Vector3[] offsets = new Vector3[4];

        switch(orientation[tileIndex]){
            case 0:
                offsets[0] = currentVectors[0] - new Vector3(0,0);
                offsets[1] = currentVectors[1] - new Vector3(0,1);
                offsets[2] = currentVectors[2] - new Vector3(1,1);
                offsets[3] = currentVectors[3] - new Vector3(1,0);
                break;
            case 1:
                offsets[0] = currentVectors[0] - new Vector3(0,1);
                offsets[1] = currentVectors[1] - new Vector3(1,1);
                offsets[2] = currentVectors[2] - new Vector3(1,0);
                offsets[3] = currentVectors[3] - new Vector3(0,0);
                break;
            case 2:
                offsets[0] = currentVectors[0] - new Vector3(1,1);
                offsets[1] = currentVectors[1] - new Vector3(1,0);
                offsets[2] = currentVectors[2] - new Vector3(0,0);
                offsets[3] = currentVectors[3] - new Vector3(0,1);
                break;
            case 3:
                offsets[0] = currentVectors[0] - new Vector3(1,0);
                offsets[1] = currentVectors[1] - new Vector3(0,0);
                offsets[2] = currentVectors[2] - new Vector3(0,1);
                offsets[3] = currentVectors[3] - new Vector3(1,1);
                break;
        }

        switch(currentRotation){
            case 0:
                newRotation[0] = new Vector3(0,0) + offsets[0];
                newRotation[1] = new Vector3(0,1) + offsets[1];
                newRotation[2] = new Vector3(1,1) + offsets[2];
                newRotation[3] = new Vector3(1,0) + offsets[3];
                break;
            case 1:
                newRotation[0] = new Vector3(0,1) + offsets[0];
                newRotation[1] = new Vector3(1,1) + offsets[1];
                newRotation[2] = new Vector3(1,0) + offsets[2];
                newRotation[3] = new Vector3(0,0) + offsets[3];
                break;
            case 2:
                newRotation[0] = new Vector3(1,1) + offsets[0];
                newRotation[1] = new Vector3(1,0) + offsets[1];
                newRotation[2] = new Vector3(0,0) + offsets[2];
                newRotation[3] = new Vector3(0,1) + offsets[3];
                break;
            case 3:                  
                newRotation[0] = new Vector3(1,0) + offsets[0];
                newRotation[1] = new Vector3(0,0) + offsets[1];
                newRotation[2] = new Vector3(0,1) + offsets[2];
                newRotation[3] = new Vector3(1,1) + offsets[3];
                break;
        }
            
        return newRotation;
    }

    public Vector3[] GetRotationNoTileIndex(int currentRotation, Vector3[] currentVectors){
        Vector3[] newRotation = new Vector3[4];
        Vector3[] offsets = new Vector3[4];

        //minus the origin offsets
        offsets[0] = currentVectors[0] - new Vector3(0,0);
        offsets[1] = currentVectors[1] - new Vector3(0,1);
        offsets[2] = currentVectors[2] - new Vector3(1,1);
        offsets[3] = currentVectors[3] - new Vector3(1,0);




        switch(currentRotation){
            case 0:
                newRotation[0] = new Vector3(0,0) + offsets[0];
                newRotation[1] = new Vector3(0,1) + offsets[1];
                newRotation[2] = new Vector3(1,1) + offsets[2];
                newRotation[3] = new Vector3(1,0) + offsets[3];
                break;
            case 1:
                newRotation[0] = new Vector3(0,1) + offsets[0];
                newRotation[1] = new Vector3(1,1) + offsets[1];
                newRotation[2] = new Vector3(1,0) + offsets[2];
                newRotation[3] = new Vector3(0,0) + offsets[3];
                break;
            case 2:
                newRotation[0] = new Vector3(1,1) + offsets[0];
                newRotation[1] = new Vector3(1,0) + offsets[1];
                newRotation[2] = new Vector3(0,0) + offsets[2];
                newRotation[3] = new Vector3(0,1) + offsets[3];
                break;
            case 3:                  
                newRotation[0] = new Vector3(1,0) + offsets[0];
                newRotation[1] = new Vector3(0,0) + offsets[1];
                newRotation[2] = new Vector3(0,1) + offsets[2];
                newRotation[3] = new Vector3(1,1) + offsets[3];
                break;
        }
            
        return newRotation;
    }

    

    //Chooses the correct input regarding if it is a touch or a mouseinput
    public Vector3 GetInputPosition(){
        Vector3 newPos = new Vector3(0,0,0);
        //Debug.Log(debugMouse);
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

    /*
    public bool CheckActivity(){
        foreach(Transform chunkChecker in check_chunks.instance.checkChunks){
            if(chunkChecker.position == trueCenter){
                
                return true;
            }
        }
        return false;
        
    }
    */

        //U   U PPPP  DDD   AA  TTTTTT EEEE 
        //U   U P   P D  D A  A   TT   E    
        //U   U PPPP  D  D AAAA   TT   EEE  
        //U   U P     D  D A  A   TT   E    
        // UUU  P     DDD  A  A   TT   EEEE 
                                      


    // Update is called once per frame
    void Update()
    {
        /*
        if(CheckActivity()){
            meshRenderer.enabled = true;
        }else{
            meshRenderer.enabled = false;
        }
        */
        
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
        Vector3 mousePos = GetInputPosition();
        //Vector3 mousePos = touch.position;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector3 mousePosGrid = new Vector3(Mathf.Floor(mousePos.x),Mathf.Floor(mousePos.y),Mathf.Floor(mousePos.z));
        Vector3Int mousePosGrid = maps[1].WorldToCell(mousePos);
        
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
            /*
            if(Input.GetMouseButton(1)){
                //this is basically checking if there is a tile and what is its index
                if(CheckTile(mousePosGrid)){
                    int tileIndex = vertexIndex;

                    //check if allowed to destroy
                    if(tileSO[tileIndex] == ground && build_button.currentItem == water){
                        //we are destroying ground
                        //uv and tileid change
                        ReplaceTile(tileIndex,build_button.currentItem);
                        
                    }
                    if(tileSO[tileIndex] == wall && build_button.currentItem == ground){
                        //we are destroying wall
                        //uv and tileid change
                        ReplaceTile(tileIndex,build_button.currentItem);
                        
                    }
                    
                    
                }
            }
            */
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
    }

    /*
    checktile needs:
    - position(rounded values) where we are checking
    - list of vertices

    maybe we can give the list as a parameter to make it more modular

    */
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

    
      
    public void OnDrawGizmos(){
        if(showOutline){
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(trueCenter,new Vector3(10,10,0));
        }
        
    }
              
}
