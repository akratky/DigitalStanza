using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeLyreHighlight : MonoBehaviour
{
    // Start is called before the first frame update

    private MeshRenderer _meshRenderer;
    void Start()
    {

        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;

        TMP_TextEventHandler.OnDetailLinkSelected += HandleClicks;
    }

    private void HandleClicks(string tag)
    {
        if (tag == "lyre")
        {
            _meshRenderer.enabled = true;
        }
        else
            _meshRenderer.enabled = false;
    }

}
