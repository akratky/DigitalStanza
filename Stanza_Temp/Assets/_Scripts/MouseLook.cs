using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float lookSensitivity = 100f;
    public Transform Character;
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            Character.Rotate(Vector3.up * mouseX);
        } else
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        
    }

}
