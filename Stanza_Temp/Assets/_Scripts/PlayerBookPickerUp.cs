using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBookPickerUp : MonoBehaviour
{
    public TMP_Text bookPickUpUI;
    public TMP_Text bookHeaderUI;
    public GameObject playerBook;
    public GameObject playerPoetryBook;
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
            if ((playerBook.activeSelf|| playerPoetryBook.activeSelf) && canPlaceBook)
            {
                
                playerBook.SetActive(false);
                playerPoetryBook.SetActive(false);
                
                if(!_bookPickupObj.GetComponent<PoetryBookPickup>())
                    _bookPickupObj.GetComponent<MeshRenderer>().enabled = true;
                else
                {
                    _bookPickupObj.GetComponent<PoetryBookPickup>().isVisible = true;
                    MeshRenderer[] renderers = _bookPickupObj.GetComponentsInChildren<MeshRenderer>();
                    foreach (var r in renderers)
                    {
                        r.enabled = true;
                    }
                }

                bookPickUpUI.enabled = false;

                BookLineRenderer.DestroyLineEvent?.Invoke();
                TMP_TextEventHandler.OnDetailLinkSelected?.Invoke("null");
                TMP_TextEventHandler.OnManuscriptLinkSelected?.Invoke("null");
                

            }
            else if (_bookPickupObj.GetComponent<BookPickup>().bookName.Contains("Poetry"))
            {
                playerPoetryBook.SetActive(true);

                MeshRenderer[] renderers = _bookPickupObj.GetComponentsInChildren<MeshRenderer>();
                foreach (var r in renderers)
                {
                    r.enabled = false;
                }
                bookPickUpUI.enabled = false;
                _bookPickupObj.GetComponent<PoetryBookPickup>().isVisible = false;
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
                bookHeaderUI.text = pickedUpBook.bookHeading;
                playerScalarInterleaf.LoadInterleafText();
                mPlayerScalarBook.LoadManuscriptRoot();

            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BookPickup")
        && (!playerBook.activeSelf || !playerPoetryBook.activeSelf))
        {
            bookPickUpUI.text =  "Press 'f' to pick up " + other.gameObject.GetComponent<BookPickup>().bookName;
            
            bookPickUpUI.enabled = true;
            _bookPickupObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BookPickup")
        && (!playerBook.activeSelf || !playerPoetryBook.activeSelf))
        {
            bookPickUpUI.enabled = false;
            _bookPickupObj = null;
        }
    }
    
    
}
