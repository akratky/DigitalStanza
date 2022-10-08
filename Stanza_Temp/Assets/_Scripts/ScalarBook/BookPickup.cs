using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookPickup : MonoBehaviour
{
    //this is the text that pops up inside in the in-game book.
    public string bookHeading;
    
    //this is the name that pops up on hte in-game UI
    public string bookName;
    public string interleafScalarPageURLSlug;
    public string manuscriptScalarPageURLSlug;
    public int numDigitsInPageNumber;
    public TMP_Text bookPickupUI;
    public PlayerBookPickerUp bookPickerUp;
    
    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera") &&
            !_renderer.enabled)
        {
            bookPickupUI.enabled = true;
            bookPickupUI.text = "Press 'F' to put down " + bookName;
            bookPickerUp.canPlaceBook = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera") &&
            !_renderer.enabled)
        {
            bookPickupUI.enabled = false;
            bookPickerUp.canPlaceBook = false;

        }
    }
}
