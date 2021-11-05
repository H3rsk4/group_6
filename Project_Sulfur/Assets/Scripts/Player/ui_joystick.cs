using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ui_joystick : MonoBehaviour
{
    //script should be in a canvas object

    //public int fingerCount = 0;
    public Transform circle;
    public Transform outerCircle;
    public Transform player;
    private Rigidbody2D rb;

    private bool isTouching = false;
    //public bool debugMouse = false;
    private Vector2 pointA;
    private Vector2 pointB;

    public float speed = 5.0f;
    public float deadZone = .2f;

    public static float areaWidth, areaHeight;

    public static int joystickTouchIndex;
    private int myTouchCount;

    private Camera cam;

    // Update is called once per frame
    void Start(){
        areaWidth = transform.GetComponent<RectTransform>().sizeDelta.x;
        areaHeight = transform.GetComponent<RectTransform>().sizeDelta.y;
        rb = player.GetComponent<Rigidbody2D>();

        cam = Camera.main;
    }


    void Update(){
    
        //rb.velocity = Vector2.zero;
        
        if(mouse_touch_controller.instance.debugMouse){
            //move with mouse
            MouseInput();
        } else {
            //move with touch
            TouchInput();
        }

    }

    private void MouseInput(){
        
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Input.mousePosition;
            if(pointA.x < areaWidth && pointA.y < areaHeight){
                isTouching = true;
                circle.position = pointA;
                outerCircle.position = pointA;
            }
            
        }
        
        
        if (Input.GetMouseButton(0))
        {
            if(pointA.x < areaWidth && pointA.y < areaHeight){
                pointB = Input.mousePosition;
            }
        }
        else
        {
            isTouching = false;
        }
        
    }

    private void TouchInput(){
        /*
        //Detect correct touchindex and use that
        if(Input.touchCount != myTouchCount){
            //this will happen everytime Input.touchCount changes
            //Debug.Log("touchCount change!");
            myTouchCount = Input.touchCount;
            
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch currentTouch = Input.GetTouch(i);
                if(currentTouch.position.x < areaWidth && currentTouch.position.y < areaHeight){
                    joystickTouchIndex = i;
                    break;
                }
            }
            
        }
        */

        //this activates multiple times if touch happens
        //if(Input.touchCount > 0){
        //Debug.Log(mouse_touch_controller.isControlling);
        //Debug.Log(mouse_touch_controller.controlIndex);
        if(mouse_touch_controller.isControlling){
            
            Touch touch = Input.GetTouch(mouse_touch_controller.controlIndex);
            //first touch
            //Touch touch;
            
            
            

            //if first touch begins
            if(!isTouching){
                pointA = touch.position;
                  
                if(pointA.x < areaWidth && pointA.y < areaHeight){
                    isTouching = true;
                    pointB = pointA;
                    circle.position = pointA;
                    outerCircle.position = pointA;
                }
            }
            

            //if movement happens within the first touch
            if(touch.phase == TouchPhase.Moved){
                pointB = touch.position;
            }

            //this needs to be added because touchphase.moved detects only movement
            if(touch.phase == TouchPhase.Ended){
                isTouching = false;
                mouse_touch_controller.isControlling = false;
            }
            //Debug.Log(touch.phase);
        }else{
            isTouching = false;
        }
    }

    private void FixedUpdate()
    {
        //Vector3 playerPos = new Vector3(Mathf.Round(player.position.x*100f)/100f, Mathf.Round(player.position.y*100f)/100f, -10);
        Vector3 playerPos = new Vector3(player.position.x,player.position.y, -10);
        //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,playerPos,.2f);
        Camera.main.transform.position = playerPos;

        if (isTouching)
        {
            circle.GetComponent<Image>().enabled = true;
            outerCircle.GetComponent<Image>().enabled = true;

            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 150);
            moveCharacter();

            circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        } else {
            circle.GetComponent<Image>().enabled = false;
            outerCircle.GetComponent<Image>().enabled = false;
        }

    }
    void moveCharacter()
    {
        Vector2 offset = cam.ScreenToWorldPoint(circle.transform.position) - cam.ScreenToWorldPoint(outerCircle.transform.position);
        Vector2 clampedDirection = Vector2.ClampMagnitude(offset, 1);
        if(Mathf.Abs(clampedDirection.x) < deadZone && Mathf.Abs(clampedDirection.y) < deadZone){ //deadzone
            clampedDirection = new Vector2(0,0);
        }
        
        player.Translate(clampedDirection * speed * Time.deltaTime);
    }

}
