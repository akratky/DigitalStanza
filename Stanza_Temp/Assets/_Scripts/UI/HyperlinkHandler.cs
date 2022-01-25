using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//from: https://deltadreamgames.com/unity-tmp-hyperlinks/

public class HyperlinkHandler : MonoBehaviour
{

    public void OnLinkClick(string s1, string s2, int index)
    {
        Debug.Log("Clicked on: " + s2);

    }
    
}



