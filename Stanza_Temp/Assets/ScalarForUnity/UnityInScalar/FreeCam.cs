using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A simple free camera to be added to a Unity game object.
/// 
/// Keys:
///	wasd / arrows	- movement
///	q/e 			- up/down (local space)
///	r/f 			- up/down (world space)
///	pageup/pagedown	- up/down (world space)
///	hold shift		- enable fast movement mode
///	right mouse  	- enable free look
///	mouse			- free look / rotation
///     
/// </summary>
public class FreeCam : MonoBehaviour
{
    /// <summary>
    /// Normal speed of camera movement.
    /// </summary>
    public float movementSpeed = 10f;

    /// <summary>
    /// Speed of camera movement when shift is held down,
    /// </summary>
    public float fastMovementSpeed = 100f;

    /// <summary>
    /// Sensitivity for free look.
    /// </summary>
    public float freeLookSensitivity = 3f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel.
    /// </summary>
    public float zoomSensitivity = 5f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel (fast mode).
    /// </summary>
    public float fastZoomSensitivity = 50f;

    /// <summary>
    /// Set to true when free looking (on right mouse button).
    /// </summary>
    private bool looking = false;


    public GameObject zoomedCameraObj;
    private Camera zoomedCam;
    
    private Camera _regCamera;
    
    private Rigidbody _rb;


    private Transform _initTransform;
    
    private bool _isZoomed;
    
    private bool moveForwardButtonPressed;
    private bool moveBackwardButtonPressed;
    private bool moveLeftButtonPressed;
    private bool moveRightButtonPressed;
    private bool moveUpButtonPressed;
    private bool moveDownButtonPressed;
    
    private void Start()
    {
        _initTransform = transform;
        _rb = GetComponent<Rigidbody>();
        _regCamera = GetComponent<Camera>();
        zoomedCameraObj.SetActive(false);
        zoomedCam = zoomedCameraObj.GetComponent<Camera>();
    }

    void Update()
    {
        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || moveLeftButtonPressed)
        {
            transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || moveRightButtonPressed)
        {
            transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || moveForwardButtonPressed)
        {
            transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || moveBackwardButtonPressed)
        {
            transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q) || moveUpButtonPressed)
        {
            if(!_rb.useGravity)
                transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E) || moveDownButtonPressed)
        {
            if(!_rb.useGravity)
                transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            if(!_rb.useGravity)
                transform.position = transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            if(_rb!.useGravity)
                transform.position = transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
        }

        /*
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.useGravity = !_rb.useGravity;
        }
*/
        if (looking)
        {               

            if (!zoomedCameraObj.activeSelf)
            {
                float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
                float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
                transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);    
            }

            else
            {
                float newRotationX = zoomedCameraObj.transform.localEulerAngles.y +
                                     Input.GetAxis("Mouse X") * freeLookSensitivity;

                float newRotationY = zoomedCameraObj.transform.localEulerAngles.x -
                                     Input.GetAxis("Mouse Y") * freeLookSensitivity;

                zoomedCameraObj.transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);

                /*
                zoomedCameraObj.transform.Rotate(0,Input.GetAxis("Mouse X")*freeLookSensitivity,0);
                zoomedCameraObj.transform.Rotate(-Input.GetAxis("Mouse Y") * freeLookSensitivity, 0, 0);
                */

            }

        }

        /*
        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
            transform.position = transform.position + transform.forward * axis * zoomSensitivity;
        }
        */

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartLooking();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopLooking();
        }
    }

    void OnDisable()
    {
        StopLooking();
    }

    /// <summary>
    /// Enable free looking.
    /// </summary>
    public void StartLooking()
    {
        looking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Disable free looking.
    /// </summary>
    public void StopLooking()
    {
        looking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetTransform()
    {
        transform.position =_initTransform.position;
        transform.rotation = _initTransform.rotation;
    }

    public void ToggleZoom()
    {
        if (zoomedCameraObj.activeSelf)
        {
            zoomedCameraObj.SetActive(false);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
            
            zoomedCameraObj.SetActive(true);
                        
        }
        
        
        _regCamera.enabled = !zoomedCameraObj.activeSelf;
    }

// Movement button functions
    public void  MoveForward(){
        moveForwardButtonPressed = true;
    }
    public void  StopMoveForward(){
        moveForwardButtonPressed = false;
    }
    public void  MoveBackward(){
        moveBackwardButtonPressed = true;
    }
    public void  StopMoveBackward(){
        moveBackwardButtonPressed = false;
    }
    public void  MoveLeft(){
        moveLeftButtonPressed = true;
    }
    public void  StopMoveLeft(){
        moveLeftButtonPressed = false;
    }
    public void  MoveRight(){
        moveRightButtonPressed = true;
    }
    public void  StopMoveRight(){
        moveRightButtonPressed = false;
    }
    public void  MoveUp(){
        moveUpButtonPressed = true;
    }
    public void  StopMoveUp(){
        moveUpButtonPressed = false;
    }
    public void  MoveDown(){
        moveDownButtonPressed = true;
    }
    public void  StopMoveDown(){
        moveDownButtonPressed = false;
    }
    
    
    
}