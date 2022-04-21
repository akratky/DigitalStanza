using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBookPickerUp : MonoBehaviour
{
    public TMP_Text bookPickUpUI;
    public GameObject playerBook;
    public bool canPlaceBook = false;
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
            if (playerBook.activeSelf && canPlaceBook)
            {
                
                playerBook.SetActive(false);
                _bookPickupObj.GetComponent<MeshRenderer>().enabled = true;
                bookPickUpUI.enabled = false;
                BookLineRenderer.DestroyLineEvent?.Invoke();
                TMP_TextEventHandler.OnDetailLinkSelected?.Invoke("null");
                TMP_TextEventHandler.OnManuscriptLinkSelected?.Invoke("null");
                

            }
            else
            {
                playerBook.SetActive(true);
                _bookPickupObj.GetComponent<MeshRenderer>().enabled = false;
                bookPickUpUI.enabled = false;


            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BookPickup")
        && !playerBook.activeSelf)
        {
            bookPickUpUI.text =  "Press 'f' to pick up " + other.gameObject.GetComponent<BookPickup>().bookName;
            
            bookPickUpUI.enabled = true;
            _bookPickupObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BookPickup")
        && !playerBook.activeSelf)
        {
            bookPickUpUI.enabled = false;
            _bookPickupObj = null;
        }
    }
    
    
}
