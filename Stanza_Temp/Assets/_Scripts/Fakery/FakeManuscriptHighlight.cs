using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeManuscriptHighlight : MonoBehaviour
{
    public string highlightTag;

    private MeshRenderer _meshRenderer;


    public delegate void DisableManuscriptHighlightDelegate();

    public static DisableManuscriptHighlightDelegate disableManuscriptEvent;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
        TMP_TextEventHandler.OnDetailLinkSelected += HandleClicks;
        disableManuscriptEvent += DisableManuscriptHighlight;
    }

    public void HandleClicks(string tag)
    {

        if (tag == highlightTag)
        {
            _meshRenderer.enabled = true;
        }
        else 
            DisableManuscriptHighlight();
        
    }

    public void DisableManuscriptHighlight()
    {
        _meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
