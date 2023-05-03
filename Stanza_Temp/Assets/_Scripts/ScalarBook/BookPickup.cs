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
    public bool bCanBePickedUp = true;
    private Renderer _renderer;

    private BoxCollider _boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }


    public void OnBookPickup()
    {
        _renderer.enabled = false;
        //_boxCollider.enabled = false;
        bCanBePickedUp = false;

    }

    public void OnBookPutdown()
    {
        _renderer.enabled = true;
        //_boxCollider.enabled = true;
        bCanBePickedUp = true;
    }
}
