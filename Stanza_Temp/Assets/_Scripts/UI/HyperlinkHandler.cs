using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//from: https://deltadreamgames.com/unity-tmp-hyperlinks/

[RequireComponent(typeof(TextMeshProUGUI))]
public class HyperlinkHandler : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnHyperlinkClicked(string s);

    public static OnHyperlinkClicked onHyperlinkClickedEvent;
    
    private TextMeshPro _textMeshPro;
    private Camera _cam;
    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _cam = Camera.main;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshPro, Input.mousePosition, _cam);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];
            
            
        }
    }
    
}



