using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class mouse_touch_controller : MonoBehaviour
{
    //this is part of a joystick script. this particular script should be inside player gameObject

    public static mouse_touch_controller instance;
    public bool debugMouse = false;

    public static bool isControlling;
    public static bool isBuilding;


    public static int controlIndex;
    public static int buildIndex;
    private int myTouchCount;

    void Awake(){
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(debugMouse){
            //mouseInput
            MouseInput();
        }else{
            //touchInput
            TouchInput();
        }
    }

    public void MouseInput(){
        if(Input.GetMouseButton(0)){
            if(Input.mousePosition.x < ui_joystick.areaWidth && Input.mousePosition.y < ui_joystick.areaHeight){
                isControlling = true;
                isBuilding = false;            
            }else{
                if(!EventSystem.current.IsPointerOverGameObject()){
                    isBuilding = true;
                    isControlling = false;
                }else{
                    isBuilding = false;
                }
            }
        }else{
            isBuilding = false;
            isControlling = false;
        }
    }
    public void TouchInput(){
        if(Input.touchCount != myTouchCount){

            myTouchCount = Input.touchCount;
            
            if(CheckJoystick()){
                isControlling = true;
            }else{
                isControlling = false;
            }

            if(CheckBuildInput()){
                isBuilding = true;
            }else{
                isBuilding = false;
            }
        }


    }

    public bool CheckJoystick(){
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch currentTouch = Input.GetTouch(i);
            if(currentTouch.position.x < ui_joystick.areaWidth && currentTouch.position.y < ui_joystick.areaHeight){
                // control touch
                controlIndex = i;
                return true;
            }
            
        }
        return false;
    }

    public bool CheckBuildInput(){
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch currentTouch = Input.GetTouch(i);
            if(currentTouch.position.x < ui_joystick.areaWidth && currentTouch.position.y < ui_joystick.areaHeight){
                
                
            }else{
                if(!IsTouchOverUIObject(currentTouch)){
                    // build touch
                    buildIndex = i;
                    return true;
                }else{
                    return false;
                }
                
                
                
            }
            
        }
        return false;
    }

    private bool IsTouchOverUIObject(Touch currentTouch) {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(currentTouch.position.x, currentTouch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
