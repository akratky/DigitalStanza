using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBookPickerUp : MonoBehaviour
{
    public TMP_Text bookPickUpUI;
    public GameObject playerBook;
    
    private GameObject _bookPickupObj;
    private KeyCode _pickupKey = KeyCode.F;
    // Start is called before the first frame update
    void Start()
    {
        playerBook.SetActive(false);
        bookPickUpUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bookPickupObj != null && Input.GetKeyDown(_pickupKey))
        {
            if (playerBook.activeSelf)
            {
                
                playerBook.SetActive(false);
                _bookPickupObj.GetComponent<MeshRenderer>().enabled = true;

            }
            else
            {
                playerBook.SetActive(true);
                _bookPickupObj.GetComponent<MeshRenderer>().enabled = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BookPickup"))
        {

            if (!playerBook.activeSelf)
            {
                bookPickUpUI.text =  "Press 'f' to pick up " + other.gameObject.GetComponent<BookPickup>().bookName;
            }
            else 
                bookPickUpUI.text =  "Press 'f' to put down " + other.gameObject.GetComponent<BookPickup>().bookName;

            
            bookPickUpUI.enabled = true;
            _bookPickupObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BookPickup"))
        {
            bookPickUpUI.enabled = false;
            _bookPickupObj = null;
        }
    }
}
