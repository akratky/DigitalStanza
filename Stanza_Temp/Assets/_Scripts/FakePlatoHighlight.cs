using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakePlatoHighlight : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    void Start()
    {

        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;

        TMP_TextEventHandler.OnDetailLinkSelected += HandleClicks;
    }

    private void HandleClicks(string tag)
    {
        print("Tag: " + tag);
        if (tag == "plato_image")
        {
            _meshRenderer.enabled = true;
        }
        else
            _meshRenderer.enabled = false;
    }
}
