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
    
    // public Camera cam;
    // public float interactDistance;
    // public LayerMask mask;
    public BookPickup currentlyHeldBook;
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
        //looking at book and press use button
        if (_bookPickupObj && Input.GetKeyDown(_pickupKey))
        {
            
            //put book down
            if (!_bookPickupObj.GetComponent<BookPickup>().bCanBePickedUp)
            {
                if (currentlyHeldBook)
                {
                    currentlyHeldBook.OnBookPutdown();
                    playerBook.SetActive(false);
                    currentlyHeldBook = null;
                }

            }
            
            //pick book up
            else
            {
                //swap books
                if (currentlyHeldBook)
                {
                    currentlyHeldBook.OnBookPutdown();
                }
            
                currentlyHeldBook = _bookPickupObj.GetComponent<BookPickup>();

                playerBook.SetActive(true);
                _bookPickupObj.GetComponent<MeshRenderer>().enabled = false;
                currentlyHeldBook.OnBookPickup();
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
        BookPickup bookInSight;

        other.TryGetComponent<BookPickup>(out bookInSight);

        //if we are looking at a book pickup
        if (bookInSight)
        {
            if (bookInSight.bCanBePickedUp && (bookInSight != currentlyHeldBook))
            {
                
                bookPickUpUI.text = "Press 'F' to pick up " + bookInSight.bookName;
                bookPickUpUI.enabled = true;
                _bookPickupObj = other.gameObject;

            }
            else if (!bookInSight.bCanBePickedUp)
            {
                bookPickUpUI.text = "Press 'F' to put book down";
                bookPickUpUI.enabled = true;
                _bookPickupObj = other.gameObject;
            }
        }

    }

    
    private void OnTriggerExit(Collider other)
    {
        BookPickup bookInSight;

        other.TryGetComponent<BookPickup>(out bookInSight);

        if (bookInSight)
        {
            if (bookInSight.bCanBePickedUp)
            {
                bookPickUpUI.enabled = false;
                _bookPickupObj = null;
            }
        }

    }
    
    
}
