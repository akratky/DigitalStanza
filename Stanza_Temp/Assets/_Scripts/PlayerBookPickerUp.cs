using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBookPickerUp : MonoBehaviour
{
    public TMP_Text bookPickUpUI;
    public GameObject playerBook;
    public ScalarInterleaf playerScalarInterleaf;
    public bool canPlaceBook = false;
    private GameObject _bookPickupObj;
    private KeyCode _pickupKey = KeyCode.F;

    private ScalarBook mPlayerScalarBook;
    // Start is called before the first frame update
    void Start()
    {
        mPlayerScalarBook = playerBook.GetComponent<ScalarBook>();
        playerBook.SetActive(false);
        bookPickUpUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bookPickupObj && Input.GetKeyDown(_pickupKey))
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

                
                BookPickup pickedUpBook = _bookPickupObj.GetComponent<BookPickup>();
                mPlayerScalarBook.numDigitsInPageNumber = pickedUpBook.numDigitsInPageNumber;
                mPlayerScalarBook.manuscriptRootURLSlug = pickedUpBook.manuscriptScalarPageURLSlug;
                playerScalarInterleaf.interleafURLSlug = pickedUpBook.interleafScalarPageURLSlug;
                
                playerScalarInterleaf.LoadInterleafText();
                mPlayerScalarBook.LoadManuscriptRoot();

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
