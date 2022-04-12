using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookPickup : MonoBehaviour
{
    public string bookName;
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
            bookPickupUI.text = "Press 'f' to put down Vigerio";
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
