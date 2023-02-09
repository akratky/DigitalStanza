using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoetryBookPickup : BookPickup
{

    public bool isVisible = true;

    private new void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera")
            && !isVisible)
        {
            bookPickupUI.enabled = true;
            bookPickupUI.text = "Press 'F' to put down " + bookName;
            bookPickerUp.canPlaceBook = true;
        }
    }

    private new void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera")
            && !isVisible)
        {
            bookPickupUI.enabled = false;
            bookPickerUp.canPlaceBook = false;
        }
    }
}
