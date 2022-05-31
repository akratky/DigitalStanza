using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using TMPro;
using UnityEngine;

public class FakePlatoHighlight : MonoBehaviour
{
    public ScalarCamera scalarCam;
    
    private MeshRenderer _meshRenderer;
    void Start()
    {

        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;

        TMP_TextEventHandler.OnDetailLinkSelected += HandleClicks;
    }

    private void HandleClicks(string tag)
    {
        if (tag == "Plato")
        {
            _meshRenderer.enabled = true;
        }
        else
            _meshRenderer.enabled = false;
    }
}
